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
using VmDigitalAuthorization = PTV.Domain.Model.Models.V2.Common.VmDigitalAuthorization;
using PTV.Domain.Model.Enums;
using System.Linq;
using PTV.Database.Model.Interfaces;
using PTV.Domain.Model.Models;
using System.Collections.Generic;
using PTV.Domain.Model.Models.V2.Common;

namespace PTV.Database.DataAccess.Translators.GeneralDescription.V2
{
    [RegisterService(typeof(ITranslator<StatutoryServiceGeneralDescription, VmConnectionsInput>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionsInputTranslator : Translator<StatutoryServiceGeneralDescription, VmConnectionsInput>
    {
        public GeneralDescriptionConnectionsInputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        { }

        public override VmConnectionsInput TranslateEntityToVm(StatutoryServiceGeneralDescription entity)
        {
            throw new NotImplementedException();
        }

        public override StatutoryServiceGeneralDescription TranslateVmToEntity(VmConnectionsInput vModel)
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
                .AddCollectionWithRemove(i => i.SelectedConnections, o => o.StatutoryServiceGeneralDescriptionServiceChannels, r => true)
                .GetFinal();
        }
    }

    [RegisterService(typeof(ITranslator<StatutoryServiceGeneralDescription, VmConnectionsOutput>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionsOutputTranslator : Translator<StatutoryServiceGeneralDescription, VmConnectionsOutput>
    {
        public GeneralDescriptionConnectionsOutputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        { }

        public override VmConnectionsOutput TranslateEntityToVm(StatutoryServiceGeneralDescription entity)
        {
            return CreateEntityViewModelDefinition(entity)               
               .AddCollection(input => input.StatutoryServiceGeneralDescriptionServiceChannels.OrderBy(x=> x.OrderNumber).ThenBy(x=> x.Created), output => output.Connections)
               .GetFinal();
        }

        public override StatutoryServiceGeneralDescription TranslateVmToEntity(VmConnectionsOutput vModel)
        {
            throw new NotImplementedException();
        }
    }

    [RegisterService(typeof(ITranslator<GeneralDescriptionServiceChannel, VmConnectionInput>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionInputTranslator : Translator<GeneralDescriptionServiceChannel, VmConnectionInput>
    {
        private ITypesCache typesCache;
        private ILanguageCache languageCache;
        public GeneralDescriptionConnectionInputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            typesCache = cacheManager.TypesCache;
            languageCache = cacheManager.LanguageCache;
        }

        public override VmConnectionInput TranslateEntityToVm(GeneralDescriptionServiceChannel entity)
        {
            throw new NotImplementedException();
        }

        public override GeneralDescriptionServiceChannel TranslateVmToEntity(VmConnectionInput vModel)
        {
            var descriptions = new List<VmDescription>();
            descriptions.AddNullRange(vModel.BasicInformation?.Description?.Select(pair => CreateDescription(pair.Key, pair.Value, vModel, DescriptionTypeEnum.Description)));
            descriptions.AddNullRange(vModel.BasicInformation?.AdditionalInformation?.Select(pair => CreateDescription(pair.Key, pair.Value, vModel, DescriptionTypeEnum.ChargeTypeAdditionalInfo)));

            return CreateViewModelEntityDefinition(vModel)
                .UseDataContextUpdate(input => true, input => output => input.MainEntityId == output.StatutoryServiceGeneralDescriptionId && input.ConnectedEntityId == output.ServiceChannelId,
                def => def.UseDataContextCreate(input => true).AddSimple(input => input.MainEntityId, output => output.StatutoryServiceGeneralDescriptionId)
                )                
                .AddSimple(i => i.ConnectedEntityId, o => o.ServiceChannelId)
                .AddSimple(i => i.BasicInformation?.ChargeType, o => o.ChargeTypeId)
                .AddSimple(i => i.OrderNumber, o => o.OrderNumber)
                .AddCollection(i => descriptions, o => o.GeneralDescriptionServiceChannelDescriptions, true)
                .AddCollectionWithRemove(i => i.DigitalAuthorization?.DigitalAuthorizations?.Select(x => new VmDigitalAuthorization() { Id = x, OwnerReferenceId = i.MainEntityId, OwnerReferenceId2 = i.ConnectedEntityId }), o => o.GeneralDescriptionServiceChannelDigitalAuthorizations, x => true)
                .GetFinal();
        }
        private VmDescription CreateDescription(string language, string value, VmConnectionInput vModel, DescriptionTypeEnum typeEnum, bool isEmpty = false)
        {
            return new VmDescription
            {
                Description = isEmpty ? null : value,
                TypeId = typesCache.Get<DescriptionType>(typeEnum.ToString()),
                OwnerReferenceId = vModel.MainEntityId,
                OwnerReferenceId2 = vModel.ConnectedEntityId,
                LocalizationId = languageCache.Get(language)
            };
        }        
    }

    [RegisterService(typeof(ITranslator<GeneralDescriptionServiceChannel, VmConnectionOutput>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionOutputTranslator : Translator<GeneralDescriptionServiceChannel, VmConnectionOutput>
    {
        private ITypesCache typesCache;
        private ILanguageCache languageCache;
        public GeneralDescriptionConnectionOutputTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            typesCache = cacheManager.TypesCache;
            languageCache = cacheManager.LanguageCache;
        }

        public override VmConnectionOutput TranslateEntityToVm(GeneralDescriptionServiceChannel entity)
        {
            var serviceChannel = VersioningManager.ApplyPublishingStatusFilterFallback(entity.ServiceChannel.Versions);
            var connectionId = entity.StatutoryServiceGeneralDescriptionId.ToString() + serviceChannel.UnificRootId.ToString();
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
               .GetFinal();
        }

        public override GeneralDescriptionServiceChannel TranslateVmToEntity(VmConnectionOutput vModel)
        {
            throw new NotImplementedException();
        }
    }

    [RegisterService(typeof(ITranslator<GeneralDescriptionServiceChannel, VmConnectionBasicInformation>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionBasicInfoTranslator : Translator<GeneralDescriptionServiceChannel, VmConnectionBasicInformation>
    {
        private ITypesCache typesCache;
        private ILanguageCache languageCache;
        public GeneralDescriptionConnectionBasicInfoTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            typesCache = cacheManager.TypesCache;
            languageCache = cacheManager.LanguageCache;
        }

        public override VmConnectionBasicInformation TranslateEntityToVm(GeneralDescriptionServiceChannel entity)
        {          
            return CreateEntityViewModelDefinition(entity)
               .AddSimple(i=>i.ChargeTypeId, o=>o.ChargeType)
               .AddDictionary(i => GetDescription(i, DescriptionTypeEnum.Description), output => output.Description, k => languageCache.GetByValue(k.LocalizationId))
               .AddDictionary(i => GetDescription(i, DescriptionTypeEnum.ChargeTypeAdditionalInfo), output => output.AdditionalInformation, k => languageCache.GetByValue(k.LocalizationId))
               .GetFinal();
        }

        public override GeneralDescriptionServiceChannel TranslateVmToEntity(VmConnectionBasicInformation vModel)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<IDescription> GetDescription(GeneralDescriptionServiceChannel generalDescriptionServiceChannel, DescriptionTypeEnum type)
        {
            return generalDescriptionServiceChannel.GeneralDescriptionServiceChannelDescriptions.Where(x => typesCache.Compare<DescriptionType>(x.TypeId, type.ToString()));
        }
    }

    [RegisterService(typeof(ITranslator<GeneralDescriptionServiceChannel, VmConnectionDigitalAuthorizationOutput>), RegisterType.Transient)]
    internal class GeneralDescriptionConnectionDigitalAuthorizationTranslator : Translator<GeneralDescriptionServiceChannel, VmConnectionDigitalAuthorizationOutput>
    {
        public GeneralDescriptionConnectionDigitalAuthorizationTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {}

        public override VmConnectionDigitalAuthorizationOutput TranslateEntityToVm(GeneralDescriptionServiceChannel entity)
        {
            return CreateEntityViewModelDefinition(entity)
               .AddCollection(input => input.GeneralDescriptionServiceChannelDigitalAuthorizations.Select(x => x.DigitalAuthorization as IFintoItemBase).OrderBy(x => x.Uri), output => output.DigitalAuthorizations)
               .GetFinal();
        }

        public override GeneralDescriptionServiceChannel TranslateVmToEntity(VmConnectionDigitalAuthorizationOutput vModel)
        {
            throw new NotImplementedException();
        }
    }
}
