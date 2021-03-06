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
using System.Collections.Generic;
using System.Linq;
using PTV.Database.DataAccess.Caches;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Enums;
using PTV.Domain.Model.Models;
using PTV.Framework;
using PTV.Framework.Interfaces;
using PTV.Database.Model.Models.Base;
using PTV.Database.Model.Interfaces;

namespace PTV.Database.DataAccess.Translators.Services
{
    [RegisterService(typeof(ITranslator<ServiceVersioned, VmServiceRelationListItem>), RegisterType.Transient)]
    internal class ServiceRelationListItemTranslator : Translator<ServiceVersioned, VmServiceRelationListItem>
    {
        private ITypesCache typesCache;
        public ServiceRelationListItemTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ITypesCache typesCache) : base(resolveManager, translationPrimitives)
        {
            this.typesCache = typesCache;
        }

        public override VmServiceRelationListItem TranslateEntityToVm(ServiceVersioned entity)
        {
            var langAvailabilities = entity.LanguageAvailabilities ?? new List<ServiceLanguageAvailability>();
            langAvailabilities = langAvailabilities.Where(i => i.Language != null).ToList();
            return CreateEntityViewModelDefinition<VmServiceRelationListItem>(entity)
                .AddNavigation(i => VersioningManager.ApplyLanguageFilterFallback(i.ServiceNames.Where(k => k.TypeId == typesCache.Get<NameType>(NameTypeEnum.Name.ToString())), RequestLanguageId)?.Name ?? "N/A", o => o.Name)
                .AddSimple(i => i.PublishingStatusId, o => o.PublishingStatusId)
                .AddSimple(i => i.UnificRootId, o => o.UnificRootId)
                .AddCollection(i => i.UnificRoot.ServiceServiceChannels.Where(x => VersioningManager.ApplyPublishingStatusFilterFallback(x.ServiceChannel.Versions) != null), o => o.ChannelRelations)
                .AddCollection<ILanguageAvailability, VmLanguageAvailabilityInfo>(i => langAvailabilities.OrderBy(x => x.Language.OrderNumber), o => o.LanguagesAvailabilities)
                .GetFinal();
        }

        public override ServiceVersioned TranslateVmToEntity(VmServiceRelationListItem vModel)
        {
            throw new NotImplementedException();
        }
    }
}
