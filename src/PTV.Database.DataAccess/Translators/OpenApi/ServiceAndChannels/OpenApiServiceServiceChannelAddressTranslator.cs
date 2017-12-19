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
using PTV.Domain.Model.Models.OpenApi.V7;
using PTV.Framework;
using PTV.Framework.Interfaces;
using System;

namespace PTV.Database.DataAccess.Translators.OpenApi.ServiceAndChannels
{
    [RegisterService(typeof(ITranslator<ServiceServiceChannelAddress, V7VmOpenApiAddressWithForeignIn>), RegisterType.Transient)]
    internal class OpenApiServiceServiceChannelAddressTranslator : Translator<ServiceServiceChannelAddress, V7VmOpenApiAddressWithForeignIn>
    {
        private readonly ITypesCache typesCache;

        public OpenApiServiceServiceChannelAddressTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ICacheManager cacheManager) : base(resolveManager, translationPrimitives)
        {
            typesCache = cacheManager.TypesCache;
        }

        public override V7VmOpenApiAddressWithForeignIn TranslateEntityToVm(ServiceServiceChannelAddress entity)
        {
            throw new NotImplementedException("No translation implemented in OpenApiServiceLocationChannelAddressTranslator");
        }

        public override ServiceServiceChannelAddress TranslateVmToEntity(V7VmOpenApiAddressWithForeignIn vModel)
        {
            return CreateViewModelEntityDefinition(vModel)
                .AddSimple(i => typesCache.Get<AddressCharacter>(i.Type), o => o.CharacterId)
                .AddNavigation(i => i, o => o.Address)
                .GetFinal();
        }
    }
}