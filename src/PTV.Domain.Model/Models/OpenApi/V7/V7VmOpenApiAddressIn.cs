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

using System.Collections.Generic;
using PTV.Framework.Attributes;
using Newtonsoft.Json;
using PTV.Domain.Model.Models.OpenApi.Extensions;

namespace PTV.Domain.Model.Models.OpenApi.V7
{
    /// <summary>
    /// OPEN API V7 - View Model of address
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.OpenApi.V7.V7VmOpenApiAddressInVersionBase" />
    public class V7VmOpenApiAddressIn : V7VmOpenApiAddressInVersionBase
    {
        /// <summary>
        /// Address type, Visiting or Postal.
        /// </summary>
        [AllowedValues("Type", new[] { AddressConsts.VISITING, AddressConsts.POSTAL })]
        public override string Type { get => base.Type; set => base.Type = value; }

        /// <summary>
        /// Address sub type, Street or PostOfficeBox.
        /// </summary>
        [AllowedValues("SubType", new[] { AddressConsts.STREET, AddressConsts.POSTOFFICEBOX })]
        public override string SubType { get => base.SubType; set => base.SubType = value; }

        /// <summary>
        /// Localized list of foreign address information.
        /// </summary>
        [JsonIgnore]
        public override IList<VmOpenApiLanguageItem> ForeignAddress { get => base.ForeignAddress; set => base.ForeignAddress = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public V7VmOpenApiAddressWithForeignIn ConvertToForeignAddress()
        {
            return base.GetBaseMode<V7VmOpenApiAddressWithForeignIn>();
        }
    }
}
