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
using Microsoft.Extensions.Logging;
using PTV.Database.DataAccess.Interfaces.DbContext;
using PTV.Database.DataAccess.Interfaces.Repositories;
using PTV.Database.DataAccess.Interfaces.Services;
using PTV.Database.DataAccess.Interfaces.Translators;
using PTV.Database.Model.Models;
using PTV.Domain.Model.Models;
using PTV.Domain.Model.Models.Interfaces;
using PTV.Domain.Model.Models.OpenApi;
using PTV.Framework;
using System.Collections.Generic;
using System.Linq;
using PTV.Framework.Interfaces;
using PTV.Database.DataAccess.Caches;
using PTV.Domain.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace PTV.Database.DataAccess.Services
{
    [RegisterService(typeof(IMunicipalityService), RegisterType.Transient)]
    internal class MunicipalityService : IMunicipalityService
    {
        private readonly IContextManager contextManager;
        private readonly ITranslationEntity translationEntToVm;
        private ILogger logger;
        private ILanguageCache languageCache;

        public MunicipalityService(IContextManager contextManager, ITranslationEntity translationEntToVm, ILogger<MunicipalityService> logger, ICacheManager cacheManager)
        {
            this.contextManager = contextManager;
            this.translationEntToVm = translationEntToVm;
            this.logger = logger;
            this.languageCache = cacheManager.LanguageCache;
        }

        public IVmListItemsData<IVmListItem> GetMunicipalities(string searchedCode, bool onlyValid = true)//TODO ADD LANGUAGE PARAM, MUNICIPALITY NAME
        {
            IReadOnlyList<VmListItem> result = new List<VmListItem>();

            contextManager.ExecuteReader(unitOfWork =>
            {
                var searchCode = searchedCode.ToLower();
                var municipalityRep = unitOfWork.CreateRepository<IMunicipalityRepository>();

                var qry = municipalityRep.All();
                if (onlyValid) qry = qry.Where(m => m.IsValid);

                var resultTemp = unitOfWork.ApplyIncludes(qry, i => i.Include(j => j.MunicipalityNames));

                if (!string.IsNullOrEmpty(searchCode))
                {
                    resultTemp = resultTemp
                                    .Where(x => x.Code.ToLower().Contains(searchedCode) ||
                                                x.MunicipalityNames.Any(y => y.Name.ToLower().Contains(searchedCode)
//                                                && y.LocalizationId == languageCache.Get(LanguageCode.fi.ToString())
                                                ))
                                    .OrderBy(x => x.Code)
                                    .Take(CoreConstants.MaximumNumberOfAllItems);

                    result = translationEntToVm.TranslateAll<Municipality, VmListItem>(resultTemp);
                }
            });

            return new VmListItemsData<IVmListItem>(result);
        }

        public IList<VmOpenApiCodeListItem> GetMunicipalitiesCodeList()
        {
            IReadOnlyList<VmOpenApiCodeListItem> result = new List<VmOpenApiCodeListItem>();

            contextManager.ExecuteReader(unitOfWork =>
            {
                var municipalityRep = unitOfWork.CreateRepository<IMunicipalityRepository>();
                var resultTemp = unitOfWork.ApplyIncludes(municipalityRep.All(), i => i.Include(j => j.MunicipalityNames));

                resultTemp = resultTemp.OrderBy(x => x.Code);

                result = translationEntToVm.TranslateAll<Municipality, VmOpenApiCodeListItem>(resultTemp);
            });

            return new List<VmOpenApiCodeListItem>(result);
        }
    }
}
