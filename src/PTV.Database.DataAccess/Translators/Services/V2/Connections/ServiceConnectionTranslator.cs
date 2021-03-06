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
using System;
using PTV.Database.DataAccess.Caches;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Framework;
using PTV.Framework.Interfaces;
using PTV.Domain.Model.Enums;
using System.Linq;
using PTV.Database.Model.Interfaces;
using PTV.Domain.Model.Models;
using System.Collections.Generic;
using PTV.Domain.Model.Models.V2.Common;

namespace PTV.Database.DataAccess.Translators.Services.V2
{
    [RegisterService(typeof(ITranslator<Service, VmConnectionsInput>), RegisterType.Transient)]
    internal class ServiceConnectionsInputTranslator : Translator<Service, VmConnectionsInput>
    {
        public ServiceConnectionsInputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        { }

        public override VmConnectionsInput TranslateEntityToVm(Service entity)
        {
            throw new NotImplementedException();
        }

        public override Service TranslateVmToEntity(VmConnectionsInput vModel)
        {
            var order = 1;
            vModel.SelectedConnections.ForEach(connection =>
            {
                connection.MainEntityId = vModel.UnificRootId;
                connection.OrderNumber = order++;
            });
            return CreateViewModelEntityDefinition(vModel)
                .DisableAutoTranslation()
                .AddSimple(i => i.UnificRootId, o => o.Id)
                .AddCollectionWithRemove(i => i.SelectedConnections, o => o.ServiceServiceChannels, r => true)
                .GetFinal();
        }
    }

    [RegisterService(typeof(ITranslator<Service, VmConnectionsOutput>), RegisterType.Transient)]
    internal class ServiceConnectionsOutputTranslator : Translator<Service, VmConnectionsOutput>
    {
        public ServiceConnectionsOutputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        { }

        public override VmConnectionsOutput TranslateEntityToVm(Service entity)
        {
            return CreateEntityViewModelDefinition(entity)               
               .AddCollection(input => input.ServiceServiceChannels.OrderBy(x=>x.OrderNumber).ThenBy(x=>x.Created), output => output.Connections)
               .GetFinal();
        }

        public override Service TranslateVmToEntity(VmConnectionsOutput vModel)
        {
            throw new NotImplementedException();
        }
    }

    

    [RegisterService(typeof(ITranslator<ServiceServiceChannel, VmConnectionOutput>), RegisterType.Transient)]
    internal class ServieConnectionOutputTranslator : Translator<ServiceServiceChannel, VmConnectionOutput>
    {
        private ITypesCache typesCache;
        private ILanguageCache languageCache;
        public ServieConnectionOutputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            typesCache = cacheManager.TypesCache;
            languageCache = cacheManager.LanguageCache;
        }

        public override VmConnectionOutput TranslateEntityToVm(ServiceServiceChannel entity)
        {
            var serviceChannel = VersioningManager.ApplyPublishingStatusFilterFallback(entity.ServiceChannel.Versions);
            var connectionId = entity.ServiceId.ToString() + serviceChannel.UnificRootId.ToString();
            return CreateEntityViewModelDefinition(entity)
               .AddNavigation(i=>connectionId, o=>o.ConnectionId)
               .AddSimple(i=>serviceChannel.Id, o=>o.Id)
               .AddSimple(i=>serviceChannel.UnificRootId, o=>o.UnificRootId)
               .AddCollection<ILanguageAvailability, VmLanguageAvailabilityInfo>(i => serviceChannel.LanguageAvailabilities, o => o.LanguagesAvailabilities)               
               .AddSimple(i=> serviceChannel.TypeId, o=>o.ChannelTypeId)
               .AddNavigation(i=> typesCache.GetByValue<ServiceChannelType>(serviceChannel.TypeId), o => o.ChannelType)
               .AddSimple(i=> serviceChannel.OrganizationId, o=>o.OrganizationId)
               .AddSimple(i=>i.Modified, o=>o.Modified)
               .AddNavigation(i => i.ModifiedBy, o => o.ModifiedBy)
               .AddDictionary(i => serviceChannel.ServiceChannelNames.Where(j => j.TypeId == typesCache.Get<NameType>(NameTypeEnum.Name.ToString())), o => o.Name, i => languageCache.GetByValueEnum(i.LocalizationId).ToString())
               .AddNavigation(i => i, o => o.BasicInformation)
               .AddNavigation(i => i, o => o.DigitalAuthorization)
               .AddNavigation(i => i, o => o.AstiDetails)
               .GetFinal();
        }

        public override ServiceServiceChannel TranslateVmToEntity(VmConnectionOutput vModel)
        {
            throw new NotImplementedException();
        }
    }
}
