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
using PTV.Database.DataAccess.Interfaces.DbContext;
using PTV.Database.DataAccess.Interfaces.Repositories;
using PTV.Database.Model.Models;
using PTV.Database.Model.Models.Base;
using PTV.ExternalSources;

namespace PTV.Database.DataAccess.Interfaces.Services
{
    internal interface ISeedingService
    {
        void SeedDatabaseEnums();

        void SeedTypes();
        
        /// <summary>
        /// Creates or updates finto data (<see cref="ServiceClass" />, <see cref="OntologyTerm"/>, <see cref="LifeEvent"/>, <see cref="TargetGroup" />, <see cref="IndustrialClass" />)
        /// </summary>
        void SeedFintoItems();

        /// <summary>
        /// Creates or updates finto data (<see cref="ServiceClass" />, <see cref="OntologyTerm"/>, <see cref="LifeEvent"/>, <see cref="TargetGroup" />, <see cref="IndustrialClass" />)
        /// </summary>
        void SeedFintoItems<TEntity>(string content, string userName = null) where TEntity : FintoItemBase<TEntity>, new();

        /// <summary>
        /// Creates or updates finto data (<see cref="OntologyTerm" />)
        /// </summary>
        void SeedOntologyTerms(string content, string userName = null);

        /// <summary>
        /// 
        /// </summary>
        void FixDuplicatedFintoItems();

        /// <summary>
        /// Creates or updates digital authorization data (<see cref="DigitalAuthorization" />)
        /// </summary>
        void SeedDigitalAuthorizations(string jsonData = null);

        void FixMultiplePublishedEntities(IUnitOfWorkWritable unitOfWork);
        void PrintGeneralDescriptionsWithoutLaw(IUnitOfWorkWritable unitOfWork);
        void CopyLawsFromPreviousToPublishedGeneralDesc(IUnitOfWorkWritable unitOfWork);
        void PrintMissingSweLaws(IUnitOfWorkWritable unitOfWork);
    }
}
