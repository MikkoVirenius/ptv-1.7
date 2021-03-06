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

using Microsoft.AspNetCore.Mvc.ModelBinding;
using PTV.Database.DataAccess.Interfaces.Services;
using PTV.Domain.Model.Enums;
using PTV.Domain.Model.Models.Interfaces.OpenApi;
using PTV.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PTV.Application.OpenApi.DataValidators
{
    /// <summary>
    /// Validator for general description.
    /// </summary>
    public class GeneralDescriptionValidator : BaseValidator<IVmOpenApiGeneralDescriptionInVersionBase>
    {
        private LocalizedListValidator names;
        private LocalizedListValidator descriptions;
        private LanguageListValidator languages;
        private ServiceClassListValidator serviceClasses;
        private OntologyTermListValidator ontologyTerms;
        private TargetGroupListValidator targetGroups;
        private LifeEventListValidator lifeEvents;
        private IndustrialClassListValidator industrialClasses;
        private PublishingStatusValidator status;

        private bool createOperation;
        private IList<string> newLanguages;

        /// <summary>
        /// Ctor - general description validator
        /// </summary>
        /// <param name="model">General description model</param>
        /// <param name="codeService">Code service</param>
        /// <param name="fintoService">Finto item service</param>
        /// <param name="newLanguages">Languages that should be validated within lists</param>
        /// <param name="createOperation">Indicates if general description is beeing created or updated.</param>
        public GeneralDescriptionValidator(IVmOpenApiGeneralDescriptionInVersionBase model, ICodeService codeService, IFintoService fintoService, IList<string> newLanguages, bool createOperation)
            : base(model, "GeneralDescription")
        {
            if (model == null)
            {
                throw new ArgumentNullException(PropertyName, $"{PropertyName} must be defined.");
            }

            names = new LocalizedListValidator(model.Names, "Names", newLanguages, new List<string>() { NameTypeEnum.Name.ToString() });
            languages = new LanguageListValidator(model.Languages, codeService);
            serviceClasses = new ServiceClassListValidator(model.ServiceClasses, fintoService);
            ontologyTerms = new OntologyTermListValidator(model.OntologyTerms, fintoService);
            targetGroups = new TargetGroupListValidator(model.TargetGroups, fintoService);
            lifeEvents = new LifeEventListValidator(model.LifeEvents, fintoService);
            industrialClasses = new IndustrialClassListValidator(model.IndustrialClasses, fintoService);
            status = new PublishingStatusValidator(model.PublishingStatus, model.CurrentPublishingStatus);

            this.createOperation = createOperation;
            this.newLanguages = newLanguages;
        }

        /// <summary>
        /// Validates general description model.
        /// </summary>
        public override void Validate(ModelStateDictionary modelState)
        {
            names.Validate(modelState);
            if (Model.Descriptions?.Where(d => d.Type == DescriptionTypeEnum.Description.ToString()).FirstOrDefault() == null && 
                    Model.Descriptions?.Where(d => d.Type == DescriptionTypeEnum.BackgroundDescription.ToString()).FirstOrDefault() == null && newLanguages?.Count > 0)
            {
                modelState.AddModelError("Descriptions", string.Format(CoreMessages.OpenApi.RequiredValueWithLanguageAndTypeNotFound, 
                    $"{ DescriptionTypeEnum.Description.ToString()} or { DescriptionTypeEnum.BackgroundDescription.ToString()}", newLanguages.First()));

                descriptions = new LocalizedListValidator(Model.Descriptions, "Descriptions", newLanguages,
                new List<string>() { DescriptionTypeEnum.ShortDescription.ToString() });
            }
            else
            {
                var descriptionType = Model.Descriptions?.Where(d => d.Type != DescriptionTypeEnum.ShortDescription.ToString()).Select(d => d.Type).FirstOrDefault();
                descriptions = new LocalizedListValidator(Model.Descriptions, "Descriptions", newLanguages,
                new List<string>() { DescriptionTypeEnum.ShortDescription.ToString(), descriptionType });
            }
            descriptions.Validate(modelState);
            languages.Validate(modelState);
            serviceClasses.Validate(modelState);
            ontologyTerms.Validate(modelState);
            targetGroups.Validate(modelState);
            lifeEvents.Validate(modelState);
            industrialClasses.Validate(modelState);
            status.Validate(modelState);
        }
    }
}
