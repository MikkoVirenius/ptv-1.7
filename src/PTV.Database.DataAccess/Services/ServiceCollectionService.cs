﻿/**
 * The MIT License
 * Copyright (c) 2016 Population Register Centre (VRK)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using PTV.Database.DataAccess.Interfaces.Services;
using PTV.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using PTV.Database.DataAccess.Interfaces.DbContext;
using PTV.Database.DataAccess.Interfaces.Repositories;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Models;
using PTV.Domain.Model.Models.Interfaces;
using PTV.Domain.Model.Enums;

using Microsoft.Extensions.Logging;
using PTV.Database.Model.Models.Base;
using PTV.Database.Model.Interfaces;
using PTV.Domain.Logic;
using PTV.Domain.Model.Models.OpenApi;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;
using PTV.Database.DataAccess.Caches;
using PTV.Database.DataAccess.Utils;
using PTV.Framework.Exceptions;
using PTV.Database.DataAccess.Interfaces;
using PTV.Database.DataAccess.Interfaces.Services.Security;
using PTV.Database.DataAccess.Translators.Channels;
using PTV.Domain.Model.Models.OpenApi.V3;
using PTV.Domain.Model.Models.Interfaces.OpenApi;
using PTV.Framework.Interfaces;
using PTV.Framework.ServiceManager;
using PTV.Domain.Model.Models.OpenApi.V4;
using Remotion.Linq.Clauses;

namespace PTV.Database.DataAccess.Services
{
    [RegisterService(typeof (IServiceCollectionService), RegisterType.Transient)]
    internal class ServiceCollectionService : ServiceBase, IServiceCollectionService
    {
        private IContextManager contextManager;

        private ILogger logger;
        private ServiceUtilities utilities;
        private DataUtils dataUtils;
        private ICommonServiceInternal commonService;
        private VmOwnerReferenceLogic ownerReferenceLogic;
        private ITypesCache typesCache;
        private ILanguageCache languageCache;
        private IVersioningManager versioningManager;

        public ServiceCollectionService(
            IContextManager contextManager,
            ITranslationEntity translationManagerToVm,
            ITranslationViewModel translationManagerToEntity,
            ILogger<ServiceService> logger,
            ServiceUtilities utilities,
            DataUtils dataUtils,
            ICommonServiceInternal commonService,
            VmOwnerReferenceLogic ownerReferenceLogic,
            ITypesCache typesCache,
            ILanguageCache languageCache,
            IPublishingStatusCache publishingStatusCache,
            IVersioningManager versioningManager,
            IGeneralDescriptionService generalDescriptionService,
            IUserOrganizationChecker userOrganizationChecker)
            : base(translationManagerToVm, translationManagerToEntity, publishingStatusCache, userOrganizationChecker)
        {
            this.contextManager = contextManager;
            this.logger = logger;
            this.utilities = utilities;
            this.dataUtils = dataUtils;
            this.commonService = commonService;
            this.ownerReferenceLogic = ownerReferenceLogic;
            this.typesCache = typesCache;
            this.languageCache = languageCache;
            this.versioningManager = versioningManager;
        }

        public IVmEntityBase DeleteServiceCollection(Guid serviceCollectionId)
        {
            ServiceCollectionVersioned result = null;
            contextManager.ExecuteWriter(unitOfWork =>
            {
                result = DeleteServiceCollection(unitOfWork, serviceCollectionId);
                unitOfWork.Save();

            });
            return new VmEntityStatusBase { Id = result.Id, PublishingStatusId = result.PublishingStatusId };
        }

        private ServiceCollectionVersioned DeleteServiceCollection(IUnitOfWorkWritable unitOfWork, Guid? serviceCollectionId)
        {
            var publishStatus = TranslationManagerToEntity.Translate<String, PublishingStatusType>(PublishingStatus.Deleted.ToString(), unitOfWork);

            var serviceCollectionRep = unitOfWork.CreateRepository<IServiceCollectionVersionedRepository>();
            var serviceCollection = serviceCollectionRep.All().Single(x => x.Id == serviceCollectionId.Value);
            serviceCollection.PublishingStatus = publishStatus;

            return serviceCollection;
        }

        #region Open Api

        public IVmOpenApiGuidPageVersionBase GetServiceCollections(DateTime? date, int pageNumber, int pageSize, bool archived)
        {
            var vm = new V3VmOpenApiGuidPage(pageNumber, pageSize);

            if (pageNumber <= 0) return vm;

            List<ServiceCollectionVersioned> collections = null;
            contextManager.ExecuteReader(unitOfWork =>
            {
                if (archived)
                {
                    collections = GetArchivedEntities<ServiceCollectionVersioned, ServiceCollection, ServiceCollectionLanguageAvailability>(vm, date, unitOfWork, q => q.Include(i => i.ServiceCollectionNames));
                }
                else
                {
                    collections = GetPublishedEntities<ServiceCollectionVersioned, ServiceCollection, ServiceCollectionLanguageAvailability>(vm, date, unitOfWork, q => q.Include(i => i.ServiceCollectionNames));
                }
            });

            if (collections?.Count > 0)
            {
                vm.ItemList = TranslationManagerToVm.TranslateAll<ServiceCollectionVersioned, VmOpenApiItem>(collections).ToList();
            }

            return vm;
        }

        public IVmOpenApiServiceCollectionVersionBase GetServiceCollectionById(Guid id, int openApiVersion, bool getOnlyPublished = true)
        {
            IVmOpenApiServiceCollectionVersionBase result = null;
            contextManager.ExecuteReader(unitOfWork =>
            {
                result = GetServiceCollectionById(unitOfWork, id, openApiVersion, getOnlyPublished);
            });

            return result;
        }

        private IVmOpenApiServiceCollectionVersionBase GetServiceCollectionById(IUnitOfWork unitOfWork, Guid id, int openApiVersion, bool getOnlyPublished)
        {
            try
            {
                // Get the right version id
                Guid? entityId = null;
                if (getOnlyPublished)
                {
                    entityId = versioningManager.GetVersionId<ServiceCollectionVersioned>(unitOfWork, id, PublishingStatus.Published);
                }
                else
                {
                    entityId = versioningManager.GetVersionId<ServiceCollectionVersioned>(unitOfWork, id);
                }
                if (entityId.IsAssigned())
                {
                    return GetServiceCollectionWithDetails(unitOfWork, entityId.Value, openApiVersion, getOnlyPublished);
                }
            }
            catch (Exception ex)
            {
                var errorMsg = string.Format("Error occured while getting a service collection with id {0}. {1}", id, ex.Message);
                logger.LogError(errorMsg + " " + ex.StackTrace);
                throw new Exception(errorMsg);
            }
            return null;
        }

        public IVmOpenApiServiceCollectionVersionBase GetServiceCollectionBySource(string sourceId, int openApiVersion, bool getOnlyPublished = true, string userName = null)
        {
            IVmOpenApiServiceCollectionVersionBase result = new VmOpenApiServiceCollectionVersionBase();
            var userId = userName ?? utilities.GetRelationIdForExternalSource();
            try
            {
                contextManager.ExecuteReader(unitOfWork =>
                {
                    var rootId = GetPTVId<ServiceCollection>(sourceId, userId, unitOfWork);
                    result = GetServiceCollectionById(unitOfWork, rootId, openApiVersion, getOnlyPublished);

                });
            }
            catch (Exception ex)
            {
                var errorMsg = string.Format("Error occured while getting service collection by source id {0}. {1}", sourceId, ex.Message);
                logger.LogError(errorMsg);
                throw new Exception(errorMsg);
            }
            return result;
        }

        public IVmOpenApiServiceCollectionBase AddServiceCollection(IVmOpenApiServiceCollectionInVersionBase vm, bool allowAnonymous, int openApiVersion, string userName = null)
        {
            var serviceCollection = new ServiceCollectionVersioned();
            var saveMode = allowAnonymous ? SaveMode.AllowAnonymous : SaveMode.Normal;
            var userId = userName ?? utilities.GetRelationIdForExternalSource();
            var useOtherEndPoint = false;

            contextManager.ExecuteWriter(unitOfWork =>
            {
                // Check if the external source already exists. Let's not throw the excpetion here to avoid contextManager to catch the exception.
                useOtherEndPoint = ExternalSourceExists<ServiceCollection>(vm.SourceId, userId, unitOfWork);
                if (!useOtherEndPoint)
                {
                    serviceCollection = TranslationManagerToEntity.Translate<IVmOpenApiServiceCollectionInVersionBase, ServiceCollectionVersioned>(vm, unitOfWork);

                    var serviceCollectionRep = unitOfWork.CreateRepository<IServiceCollectionVersionedRepository>();
                    serviceCollectionRep.Add(serviceCollection);

                    // Create the mapping between external source id and PTV id
                    if (!string.IsNullOrEmpty(vm.SourceId))
                    {
                        SetExternalSource(serviceCollection.UnificRoot, vm.SourceId, userId, unitOfWork);
                    }

                    unitOfWork.Save(saveMode, userName: userName);
                }
            });

            if (useOtherEndPoint)
            {
                throw new ExternalSourceExistsException(string.Format(CoreMessages.OpenApi.ExternalSourceExists, vm.SourceId));
            }

            // Publish all language versions
            if (vm.PublishingStatus == PublishingStatus.Published.ToString())
            {
                var publishingResult = commonService.PublishAllAvailableLanguageVersions<ServiceCollectionVersioned, ServiceCollectionLanguageAvailability>(serviceCollection.Id, i => i.ServiceCollectionVersionedId == serviceCollection.Id);
            }

            return GetServiceCollectionWithDetails(serviceCollection.Id, openApiVersion, false);
        }

        public IVmOpenApiServiceCollectionBase SaveServiceCollection(IVmOpenApiServiceCollectionInVersionBase vm, bool allowAnonymous, int openApiVersion, string sourceId = null, string userName = null)
        {
            var saveMode = allowAnonymous ? SaveMode.AllowAnonymous : SaveMode.Normal;
            var userId = userName ?? utilities.GetRelationIdForExternalSource();
            IVmOpenApiServiceCollectionBase result = new VmOpenApiServiceCollectionBase();
            ServiceCollectionVersioned serviceCollection = null;

            contextManager.ExecuteWriter(unitOfWork =>
            {
                // Get the root id according to source id (if defined)
                var rootId = vm.Id ?? GetPTVId<ServiceCollection>(sourceId, userId, unitOfWork);

                // Get right version id
                vm.Id = versioningManager.GetVersionId<ServiceCollectionVersioned>(unitOfWork, rootId);

                if (vm.PublishingStatus == PublishingStatus.Deleted.ToString())
                {
                    serviceCollection = DeleteServiceCollection(unitOfWork, vm.Id);
                }
                else
                {
                    // Entity needs to be restored?
                    if (vm.CurrentPublishingStatus == PublishingStatus.Deleted.ToString())
                    {
                        if (vm.PublishingStatus == PublishingStatus.Modified.ToString() || vm.PublishingStatus == PublishingStatus.Published.ToString())
                        {
                            // We need to restore already archived item
                            var publishingResult = commonService.RestoreArchivedEntity<ServiceCollectionVersioned>(unitOfWork, vm.Id.Value);
                        }
                    }

                    serviceCollection = TranslationManagerToEntity.Translate<IVmOpenApiServiceCollectionInVersionBase, ServiceCollectionVersioned>(vm, unitOfWork);

                    if (vm.CurrentPublishingStatus == PublishingStatus.Draft.ToString())
                    {
                        // We need to manually remove items from collections!
                        if (vm.ServiceCollectionNames?.Count > 0)
                        {
                            var updatedEntities = serviceCollection.ServiceCollectionNames;
                            var rep = unitOfWork.CreateRepository<IServiceCollectionNameRepository>();
                            var currentItems = rep.All().Where(s => s.ServiceCollectionVersionedId == serviceCollection.Id).ToList();
                            var toRemove = currentItems.Where(i => !updatedEntities.Any(s => s.TypeId == i.TypeId && s.LocalizationId == i.LocalizationId));
                            toRemove.ForEach(i => rep.Remove(i));
                        }
                        if (vm.ServiceCollectionDescriptions?.Count > 0)
                        {
                            var updatedEntities = serviceCollection.ServiceCollectionDescriptions;
                            var rep = unitOfWork.CreateRepository<IServiceCollectionDescriptionRepository>();
                            var currentItems = rep.All().Where(s => s.ServiceCollectionVersionedId == serviceCollection.Id).ToList();
                            var toRemove = currentItems.Where(i => !updatedEntities.Any(s => s.TypeId == i.TypeId && s.LocalizationId == i.LocalizationId));
                            toRemove.ForEach(i => rep.Remove(i));
                        }
                        if (vm.DeleteAllServices || vm.ServiceCollectionServices?.Count > 0)
                        {
                            serviceCollection.ServiceCollectionServices = dataUtils.UpdateCollectionForReferenceTable(unitOfWork, 
                                serviceCollection.ServiceCollectionServices,
                                query => query.ServiceCollectionVersionedId == serviceCollection.Id, 
                                service => service.Service != null ? service.Service.Id : service.ServiceId
                                );
                        }
                    }

                    // Update the mapping between external source id and PTV id
                    if (!string.IsNullOrEmpty(vm.SourceId))
                    {
                        UpdateExternalSource<ServiceCollection>(serviceCollection.UnificRootId, vm.SourceId, userId, unitOfWork);
                    }
                }

                unitOfWork.Save(saveMode, serviceCollection, userName);
            });

            // Publish all language versions
            if (vm.PublishingStatus == PublishingStatus.Published.ToString())
            {
                var publishingResult = commonService.PublishAllAvailableLanguageVersions<ServiceCollectionVersioned, ServiceCollectionLanguageAvailability>(serviceCollection.Id, i => i.ServiceCollectionVersionedId == serviceCollection.Id);
            }

            return GetServiceCollectionWithDetails(serviceCollection.Id, openApiVersion, false);
        }

        public bool ServiceCollectionExists(Guid serviceCollectionId)
        {
            bool srvCollectionExists = false;

            if (Guid.Empty == serviceCollectionId)
            {
                return srvCollectionExists;
            }

            contextManager.ExecuteReader(unitOfWork =>
            {
                var serviceCollectionRepo = unitOfWork.CreateRepository<IServiceCollectionRepository>().All();

                if (serviceCollectionRepo.FirstOrDefault(s => s.Id.Equals(serviceCollectionId)) != null)
                {
                    srvCollectionExists = true;
                }
            });

            return srvCollectionExists;
        }

        private IVmOpenApiServiceCollectionVersionBase GetServiceCollectionWithDetails(IUnitOfWork unitOfWork, Guid versionId, int openApiVersion, bool getOnlyPublished = true)
        {
            return GetServiceCollectionsWithDetails(unitOfWork, new List<Guid> { versionId }, openApiVersion, getOnlyPublished).FirstOrDefault();
        }

        private IVmOpenApiServiceCollectionVersionBase GetServiceCollectionWithDetails(Guid versionId, int openApiVersion, bool getOnlyPublished = true)
        {
            IVmOpenApiServiceCollectionVersionBase result = null;
            contextManager.ExecuteReader(unitOfWork =>
            {
                result = GetServiceCollectionsWithDetails(unitOfWork, new List<Guid> { versionId }, openApiVersion, getOnlyPublished).FirstOrDefault();
            });
            return result;
        }

        private IList<IVmOpenApiServiceCollectionVersionBase> GetServiceCollectionsWithDetails(IUnitOfWork unitOfWork, List<Guid> versionIdList, int openApiVersion, bool getOnlyPublished = true)
        {
            if (versionIdList.Count == 0) return new List<IVmOpenApiServiceCollectionVersionBase>();

            var serviceCollectionRep = unitOfWork.CreateRepository<IServiceCollectionVersionedRepository>();
            var publishedId = PublishingStatusCache.Get(PublishingStatus.Published);

            var resultTemp = unitOfWork.ApplyIncludes(serviceCollectionRep.All().Where(s => versionIdList.Contains(s.Id)), q =>
                //q.Include(i => i.ServiceCollectionLanguages).ThenInclude(i => i.Language)
                q.Include(i => i.ServiceCollectionNames)
                .Include(i => i.ServiceCollectionDescriptions)
                .Include(i => i.ServiceCollectionServices).ThenInclude(i => i.Service).ThenInclude(i => i.Versions)
                .Include(i => i.LanguageAvailabilities)
                .Include(i => i.Organization).ThenInclude(i => i.Versions)
                .OrderByDescending(i => i.Modified));

            // Filter out items that do not have language versions published!
            var serviceCollections = getOnlyPublished ? resultTemp.Where(c => c.LanguageAvailabilities.Any(l => l.StatusId == publishedId)).ToList() : resultTemp.ToList();

            // Find only published services for service collections
            var publishedServices = serviceCollections.SelectMany(i => i.ServiceCollectionServices).Select(i => i.Service).SelectMany(i => i.Versions)
                .Where(i => i.PublishingStatusId == publishedId).ToList();
            var publishedServiceIds = publishedServices.Select(i => i.Id).ToList();
            var publishedServiceRootIds = new List<Guid>();
            if (publishedServiceIds.Count > 0)
            {
                var servicenRep = unitOfWork.CreateRepository<IServiceVersionedRepository>();
                publishedServiceRootIds = servicenRep.All().Where(c => publishedServiceIds.Contains(c.Id))
                        .Where(s => s.LanguageAvailabilities.Any(l => l.StatusId == publishedId)) // Filter out services with no language versions published
                        .Select(c => c.UnificRootId).ToList();
            }

            serviceCollections.ForEach(serviceCollection =>
            {
                // Filter out not published services
                serviceCollection.ServiceCollectionServices = serviceCollection.ServiceCollectionServices.Where(c => publishedServiceRootIds.Contains(c.ServiceId)).ToList();

                // Filter out not published language versions
                if (getOnlyPublished)
                {
                    var notPublishedLanguageVersions = serviceCollection.LanguageAvailabilities.Where(l => l.StatusId != publishedId).Select(l => l.LanguageId).ToList();
                    if (notPublishedLanguageVersions.Count > 0)
                    {
                        serviceCollection.ServiceCollectionNames = serviceCollection.ServiceCollectionNames.Where(i => !notPublishedLanguageVersions.Contains(i.LocalizationId)).ToList();
                        serviceCollection.ServiceCollectionDescriptions = serviceCollection.ServiceCollectionDescriptions.Where(i => !notPublishedLanguageVersions.Contains(i.LocalizationId)).ToList();
                    }
                }
            });

            // Fill with service names (only type: Name)
            var serviceNames = unitOfWork.CreateRepository<IServiceNameRepository>().All()
                .Where(i => publishedServiceIds.Contains(i.ServiceVersionedId) && i.TypeId == typesCache.Get<NameType>(NameTypeEnum.Name.ToString()))
                .GroupBy(i => i.ServiceVersionedId).ToDictionary(i => i.Key, i => i.ToList());

            // Fill with service descriptions (only type: Description)
            var serviceDescriptions = unitOfWork.CreateRepository<IServiceDescriptionRepository>().All()
                .Where(i => publishedServiceIds.Contains(i.ServiceVersionedId) && i.TypeId == typesCache.Get<DescriptionType>(DescriptionTypeEnum.Description.ToString()))
                .GroupBy(i => i.ServiceVersionedId).ToDictionary(i => i.Key, i => i.ToList());

            // Get language availabilities for published services
            var serviceLanguageAvailabilities = unitOfWork.CreateRepository<IServiceLanguageAvailabilityRepository>().All()
                .Where(i => publishedServiceIds.Contains(i.ServiceVersionedId) && i.StatusId == publishedId)
                .GroupBy(i => i.ServiceVersionedId)
                .ToDictionary(i => i.Key, i => i.Select(l => l.LanguageId).ToList());

            // Filter out name and description language versions that are not available
            publishedServices.ForEach(s =>
            {
                var availableLanguages = serviceLanguageAvailabilities.TryGet(s.Id);

                if (availableLanguages?.Count > 0)
                {
                    s.ServiceNames = serviceNames.TryGet(s.Id)?.Where(n => availableLanguages.Contains(n.LocalizationId)).ToList();
                    s.ServiceDescriptions = serviceDescriptions?.TryGet(s.Id).Where(n => availableLanguages.Contains(n.LocalizationId)).ToList();
                }
            });

            var result = TranslationManagerToVm.TranslateAll<ServiceCollectionVersioned, VmOpenApiServiceCollectionVersionBase>(serviceCollections).ToList();
            if (result == null)
            {
                throw new Exception(CoreMessages.OpenApi.RecordNotFound);
            }

            // Get the right open api view model version
            var versionList = new List<IVmOpenApiServiceCollectionVersionBase>();
            result.ForEach(serviceCollection =>
            {
                // Get the sourceId if user is logged in
                var userId = utilities.GetRelationIdForExternalSource(false);
                if (!string.IsNullOrEmpty(userId))
                {
                    serviceCollection.SourceId = GetSourceId<ServiceCollection>(serviceCollection.Id.Value, userId, unitOfWork);
                }
                versionList.Add(GetEntityByOpenApiVersion(serviceCollection as IVmOpenApiServiceCollectionVersionBase, openApiVersion));
            });

            return versionList;
        }

        #endregion
    }
}