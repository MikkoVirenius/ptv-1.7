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

using Newtonsoft.Json;

namespace PTV.Domain.Model.Models.OpenApi
{
    /// <summary>
    /// OPEN API - View Model of service location channel
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.OpenApi.VmOpenApiContactDetailsInBase" />
    public class VmOpenApiContactDetailsIn : VmOpenApiContactDetailsInBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether all addresses should be delted.
        /// </summary>
        /// <value>
        /// <c>true</c> if all addresses should be deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public override bool DeleteAllAddresses { get => base.DeleteAllAddresses; set => base.DeleteAllAddresses = value; }

        /// <summary>
        /// Gets or sets a value indicating whether all emails should be delted.
        /// </summary>
        /// <value>
        /// <c>true</c> if all email should be deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public override bool DeleteAllEmails { get => base.DeleteAllEmails; set => base.DeleteAllEmails = value; }

        /// <summary>
        /// Gets or sets a value indicating whether all phones should be delted.
        /// </summary>
        /// <value>
        /// <c>true</c> if all phones should be deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public override bool DeleteAllPhones { get => base.DeleteAllPhones; set => base.DeleteAllPhones = value; }

        /// <summary>
        /// Gets or sets a value indicating whether all web pages should be delted.
        /// </summary>
        /// <value>
        /// <c>true</c> if all web pages should be deleted; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public override bool DeleteAllWebPages { get => base.DeleteAllWebPages; set => base.DeleteAllWebPages = value; }
    }
}
