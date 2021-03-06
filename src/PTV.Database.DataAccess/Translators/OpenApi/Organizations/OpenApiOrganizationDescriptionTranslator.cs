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
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Models.OpenApi;
using PTV.Framework;
using PTV.Framework.Interfaces;
using System;
using PTV.Domain.Model.Models.Interfaces;
using PTV.Database.DataAccess.Caches;
using PTV.Domain.Model.Enums;
using PTV.Database.DataAccess.Translators.OpenApi.Common;

namespace PTV.Database.DataAccess.Translators.OpenApi.Organizations
{
    [RegisterService(typeof(ITranslator<OrganizationDescription, VmOpenApiLocalizedListItem>), RegisterType.Transient)]
    internal class OpenApiOrganizationDescriptionTranslator : OpenApiDescriptionWithTypeBaseTranslator<OrganizationDescription>
    {
        private readonly ILanguageCache languageCache;
        private readonly ITypesCache typesCache;

        public OpenApiOrganizationDescriptionTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives, cacheManager)
        {
            languageCache = cacheManager.LanguageCache;
            typesCache = cacheManager.TypesCache;
        }

        public override VmOpenApiLocalizedListItem TranslateEntityToVm(OrganizationDescription entity)
        {
             return base.TranslateEntityToVm(entity);
        }

        public override OrganizationDescription TranslateVmToEntity(VmOpenApiLocalizedListItem vModel)
        {
            var isMarkDescription = vModel.Type == DescriptionTypeEnum.Description.ToString();
            return CreateViewModelEntityDefinition(vModel)
                .UseDataContextCreate(i => !i.OwnerReferenceId.IsAssigned())
                .UseDataContextUpdate(i => i.OwnerReferenceId.IsAssigned(), i => o => typesCache.Get<DescriptionType>(i.Type) == o.TypeId && languageCache.Get(i.Language) == o.LocalizationId && i.OwnerReferenceId == o.OrganizationVersionedId, def => def.UseDataContextCreate(i => true))
                .AddNavigation(i => IsTextEditorField(typesCache.Get<DescriptionType>(vModel.Type)) ? textManager.ConvertMarkdownToJson(i.Value) : i.Value, o => o.Description)
                .AddNavigation(i => i.Language, o => o.Localization)
                .AddNavigation(i => i.Type, o => o.Type)
                .GetFinal();
        }
    }
}
