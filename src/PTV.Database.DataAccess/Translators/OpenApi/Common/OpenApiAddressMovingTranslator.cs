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
using PTV.Framework;
using PTV.Framework.Interfaces;
using System;
using PTV.Database.DataAccess.Caches;
using PTV.Domain.Model.Models.OpenApi.V7;
using PTV.Domain.Model.Enums;
using System.Linq;

namespace PTV.Database.DataAccess.Translators.OpenApi.Common
{
    [RegisterService(typeof(ITranslator<Address, V7VmOpenApiAddressWithMoving>), RegisterType.Transient)]
    internal class OpenApiAddressMovingTranslator : OpenApiAddressBaseTranslator<V7VmOpenApiAddressWithMoving>
    {
        private readonly ITypesCache typesCache;

        public OpenApiAddressMovingTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, ITypesCache typesCache) 
            : base(resolveManager, translationPrimitives, typesCache)
        {
            this.typesCache = typesCache;
        }

        public override V7VmOpenApiAddressWithMoving TranslateEntityToVm(Address entity)
        {
            var definition = CreateBaseEntityVmDefinitions(entity);

            var addressType = typesCache.GetByValue<AddressType>(entity.TypeId);
            if (addressType == AddressTypeEnum.Moving.ToString())
            {
                definition.AddCollection(i => i.AddressStreets, o => o.MultipointLocation);
                var vm = definition.GetFinal();
                if (vm.MultipointLocation?.Count > 0)
                {
                    vm.MultipointLocation.FirstOrDefault().OrderNumber = entity.OrderNumber;
                }
                return vm;
            }

            return definition.GetFinal();
        }

        public override Address TranslateVmToEntity(V7VmOpenApiAddressWithMoving vModel)
        {
            throw new NotImplementedException("No translation implemented in OpenApiAddressWithTypeAndCoordinatesTranslator");
        }
    }
}
