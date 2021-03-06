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
using Newtonsoft.Json;
using PTV.Domain.Model.Models.OpenApi.V4;
using PTV.Framework;
using PTV.Framework.Attributes;

namespace PTV.Domain.Model.Models.OpenApi.V6
{
    /// <summary>
    /// OPEN API V6 - View Model of service organization base
    /// </summary>
    public class V6VmOpenApiServiceOrganizationBase : VmOpenApiServiceOrganizationVersionBase
    {
        /// <summary>
        /// Provision type, valid values SelfProduced, PurchaseServices or Other. Required if RoleType value is Producer.
        /// </summary>
        [AllowedValues("ProvisionType", new[] {"SelfProduced", "PurchaseServices", "Other"})]
        [RequiredIf("RoleType", "Producer")]
        public override string ProvisionType { get; set; }

        /// <summary>
        /// Organization identifier
        /// </summary>
        [ValidGuid]
        [RequiredIf("RoleType", "Responsible")]
        [RequiredIf("ProvisionType", "SelfProduced")]
        public virtual string OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the service identifier.
        /// </summary>
        /// <value>
        /// The service identifier.
        /// </value>
        [JsonIgnore]
        public string ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the owner reference identifier.
        /// </summary>
        /// <value>
        /// The owner reference identifier.
        /// </value>
        [JsonIgnore]
        public Guid? OwnerReferenceId { get; set; }

        /// <summary>
        /// List of web pages.
        /// </summary>
        [JsonIgnore]
        public override IList<V4VmOpenApiWebPage> WebPages { get; set; }

        /// <summary>
        /// Converts to version 4.
        /// </summary>
        /// <returns>model converted to version 4</returns>
        public V4VmOpenApiServiceOrganization ConvertToVersion4()
        {
            var vm = ConvertToVersionBase<V4VmOpenApiServiceOrganization>();
            vm.OrganizationId = Organization != null && Organization.Id.IsAssigned() ? Organization.Id.ToString() : null;
            return vm;
        }
    }
}
