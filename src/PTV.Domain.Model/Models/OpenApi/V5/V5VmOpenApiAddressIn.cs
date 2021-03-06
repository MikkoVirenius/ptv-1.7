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

using PTV.Framework.Attributes;
using PTV.Domain.Model.Models.Interfaces.OpenApi.V5;
using System.Collections.Generic;
using PTV.Domain.Model.Models.OpenApi.V7;
using PTV.Domain.Model.Models.OpenApi.Extensions;

namespace PTV.Domain.Model.Models.OpenApi.V5
{
    /// <summary>
    /// OPEN API V5 - View Model of Address
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.OpenApi.VmOpenApiAddressInVersionBase" />
    /// <seealso cref="PTV.Domain.Model.Models.Interfaces.OpenApi.V5.IV5VmOpenApiAddressIn" />
    public class V5VmOpenApiAddressIn : VmOpenApiAddressInVersionBase, IV5VmOpenApiAddressIn
    {
        /// <summary>
        /// Post office box like PL 310
        /// </summary>
        [ListRequiredIf("StreetAddress", null)]
        public override IList<VmOpenApiLanguageItem> PostOfficeBox
        {
            get
            {
                return base.PostOfficeBox;
            }

            set
            {
                base.PostOfficeBox = value;
            }
        }

        /// <summary>
        /// List of localized street addresses.
        /// </summary>
        [ListRequiredIf(AddressConsts.POSTOFFICEBOX, "")]
        [ValidStreetAddress]
        public override IList<VmOpenApiLanguageItem> StreetAddress
        {
            get
            {
                return base.StreetAddress;
            }

            set
            {
                base.StreetAddress = value;
            }
        }

        /// <summary>
        /// Converts model to latest version.
        /// </summary>
        /// <returns></returns>
        public V7VmOpenApiAddressDeliveryIn LatestVersionDeliveryAddress()
        {
            return base.ConvertToDeliveryAddressVersionIn<V7VmOpenApiAddressDeliveryIn>();
        }
    }
}
