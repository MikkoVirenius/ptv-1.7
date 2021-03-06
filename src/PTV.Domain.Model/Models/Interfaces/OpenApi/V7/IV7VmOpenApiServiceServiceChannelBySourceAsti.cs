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
using System.Collections.Generic;

namespace PTV.Domain.Model.Models.Interfaces.OpenApi.V7
{
    /// <summary>
    /// OPEN API  - View model interface of service service channel.
    /// </summary>
    public interface IV7VmOpenApiServiceServiceChannelBySourceAsti : IVmOpenApiServiceServiceChannelBySourceBase
    {
        /// <summary>
        /// The extra types related to service and service channel connection.
        /// </summary>
        IList<VmOpenApiExtraType> ExtraTypes { get; set; }
        /// <summary>
        /// Indicates if connection between service and service channel is ASTI related.
        /// </summary>
        /// <value>
        /// The channel unique identifier.
        /// </value>
        bool IsASTIConnection { get; set; }
        /// <summary>
        /// Indicates if all extra types should be deleted.
        /// </summary>
        bool DeleteAllExtraTypes { get; set; }
    }
}
