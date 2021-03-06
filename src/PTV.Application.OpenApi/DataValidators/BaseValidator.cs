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
using System;

namespace PTV.Application.OpenApi.DataValidators
{
    /// <summary>
    /// Base class for validating an open api vew model
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class BaseValidator<TModel>
    {
        private TModel model;
        private string propertyName;

        /// <summary>
        /// The model which is validated.
        /// </summary>
        protected TModel Model { get { return model; } private set { } }

        /// <summary>
        /// Name for view model.
        /// </summary>
        protected string PropertyName { get { return propertyName; } set { propertyName = value; } }

        /// <summary>
        /// Ctor - base validator
        /// </summary>
        public BaseValidator(TModel model, string propertyName)
        {
            this.model = model;
            this.propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName), "Property name must be defined."); ;
        }

        /// <summary>
        /// Performs the validation of view model.
        /// </summary>
        public abstract void Validate(ModelStateDictionary modelState);
    }
}
