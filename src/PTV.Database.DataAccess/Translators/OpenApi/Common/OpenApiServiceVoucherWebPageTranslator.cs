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
using PTV.Domain.Model.Models.OpenApi;
using PTV.Framework;
using PTV.Framework.Interfaces;

namespace PTV.Database.DataAccess.Translators.OpenApi.Common
{
    [RegisterService(typeof(ITranslator<WebPage, VmOpenApiServiceVoucher>), RegisterType.Transient)]
    internal class OpenApiServiceVoucherWebPageTranslator : Translator<WebPage, VmOpenApiServiceVoucher>
    {

        public OpenApiServiceVoucherWebPageTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives) : base(resolveManager, translationPrimitives)
        {}

        public override VmOpenApiServiceVoucher TranslateEntityToVm(WebPage entity)
        {
            throw new NotImplementedException("WebPage -> VmOpenApiServiceVoucher is not implemented.");
//            return CreateEntityViewModelDefinition(entity)
//                .AddSimple(i => i.Id, o => o.Id)
//                .AddSimple(i => i.OrderNumber ?? 0, o => o.OrderNumber)
//                .AddNavigation(i => i.Name, o => o.Value)
//                .AddNavigation(i => i.Url, o => o.Url)
//                .AddNavigation(i => i.Description, o => o.AdditionalInformation)
//                .GetFinal();
        }

        public override WebPage TranslateVmToEntity(VmOpenApiServiceVoucher vModel)
        {
            var id = vModel.Id ?? Guid.Empty;
            var exists = vModel.Id.IsAssigned();

            var definition = CreateViewModelEntityDefinition(vModel)
                    .UseDataContextCreate(i => !exists, o => o.Id, i => Guid.NewGuid())
                    .UseDataContextUpdate(i => exists, i => o => id == o.Id, e => e.UseDataContextCreate(x => true))
                    .AddSimple(i => i.OrderNumber, o => o.OrderNumber)
                    .AddNavigation(i => i.Value, o => o.Name)
                    .AddNavigation(i => i.Url, o => o.Url)
                    .AddNavigation(i => i.AdditionalInformation, o => o.Description)
                    .AddNavigation(i => i.Language, o => o.Localization)
                ;
            return definition.GetFinal();
        }
    }
}