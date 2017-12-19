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
using System.Linq;
using PTV.Database.DataAccess.Caches;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.DataAccess.Translators.Common;
using PTV.Database.Model.Interfaces;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Models;
using PTV.Framework;
using PTV.Framework.Interfaces;

namespace PTV.Database.DataAccess.Translators.Municipalities
{
    [RegisterService(typeof(ITranslator<Area, VmListItemReferences>), RegisterType.Scope)]
    internal class AreaListTranslator : Translator<Area, VmListItemReferences>
    {
        private TranslatedItemDefinitionHelper definitionHelper;
        public AreaListTranslator(IResolveManager resolveManager, ITranslationPrimitives translationPrimitives, TranslatedItemDefinitionHelper definitionHelper) : base(resolveManager, translationPrimitives)
        {
            this.definitionHelper = definitionHelper;
        }

        public override VmListItemReferences TranslateEntityToVm(Area entity)
        {
            var definition = CreateEntityViewModelDefinition(entity)
                .AddNavigation(i => i.Code, o => o.Code)
                .AddSimple(i => i.Id, o => o.Id)
                .AddSimpleList(i => i.AreaMunicipalities.Select(x => x.MunicipalityId), o => o.ReferenceItems);

            definitionHelper.AddTranslations(definition, entity?.Code);
            return definition.GetFinal();
        }

        public override Area TranslateVmToEntity(VmListItemReferences vModel)
        {
            throw new NotSupportedException();
        }
    }
}