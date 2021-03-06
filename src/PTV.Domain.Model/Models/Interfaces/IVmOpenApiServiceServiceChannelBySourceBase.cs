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

using PTV.Domain.Model.Models.OpenApi;
using PTV.Domain.Model.Models.OpenApi.V4;
using System.Collections.Generic;

namespace PTV.Domain.Model.Models.Interfaces
{
    /// <summary>
    /// OPEN API  - View model interface of service service channel.
    /// </summary>
    public interface IVmOpenApiServiceServiceChannelBySourceBase
    {
        /// <summary>
        /// The external source id for service channel.
        /// </summary>
        string ServiceChannelSourceId { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        IList<VmOpenApiLocalizedListItem> Description { get; set; }
        /// <summary>
        /// Gets or sets the type of the service charge.
        /// </summary>
        /// <value>
        /// The type of the service charge.
        /// </value>
        string ServiceChargeType { get; set; }
        /// <summary>
        /// List of connection related service hours.
        /// </summary>
        IList<V4VmOpenApiServiceHour> ServiceHours { get; set; }
        /// <summary>
        /// List of connection related service hours.
        /// </summary>
        VmOpenApiContactDetailsInBase ContactDetails { get; set; }
        /// <summary>
        /// Indicates if value for property ServiceChargeType should be deleted.
        /// </summary>
        bool DeleteServiceChargeType { get; set; }
        /// <summary>
        /// Indicates if all descriptions should be deleted.
        /// </summary>
        bool DeleteAllDescriptions { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether all emails should be delted.
        /// </summary>
        /// <value>
        /// <c>true</c> if all email should be deleted; otherwise, <c>false</c>.
        /// </value>
        bool DeleteAllServiceHours { get; set; }
    }
}
