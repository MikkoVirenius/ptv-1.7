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

using PTV.Database.DataAccess.Caches;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Models.OpenApi;
using PTV.Framework;
using PTV.Framework.Interfaces;
using System;

namespace PTV.Database.DataAccess.Translators.OpenApi.Services
{
    [RegisterService(typeof(ITranslator<ServiceKeyword, VmOpenApiLanguageItem>), RegisterType.Transient)]
    internal class OpenApiServiceKeywordTranslator : Translator<ServiceKeyword, VmOpenApiLanguageItem>
    {
        private readonly ILanguageCache languageCache;

        public OpenApiServiceKeywordTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            languageCache = cacheManager.LanguageCache;
        }

        public override VmOpenApiLanguageItem TranslateEntityToVm(ServiceKeyword entity)
        {
            if (entity == null || entity.Keyword == null) return null;            
            return CreateEntityViewModelDefinition(entity)
                .AddNavigation(i => string.IsNullOrEmpty(i.Keyword.Name) ? null : i.Keyword.Name, o => o.Value)
                .AddNavigation(i => languageCache.GetByValue(i.Keyword.LocalizationId), o => o.Language)
                .GetFinal();
        }

        public override ServiceKeyword TranslateVmToEntity(VmOpenApiLanguageItem vModel)
        {
            return CreateViewModelEntityDefinition(vModel)
                .UseDataContextUpdate(i => i.Id.IsAssigned() && i.OwnerReferenceId.IsAssigned(),
                i => o => i.Id == o.KeywordId && i.OwnerReferenceId == o.ServiceVersionedId, x => x.UseDataContextCreate(i => true))
                .AddNavigation(i => i, o => o.Keyword)
                .GetFinal();
        }
    }
}
