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
using PTV.Domain.Model.Models.Interfaces.OpenApi.V5;
using PTV.Domain.Model.Models.OpenApi.V7;
using System;
using System.Collections.Generic;

namespace PTV.Domain.Model.Models.OpenApi.V5
{
    /// <summary>
    /// OPEN API V5 - View Model of service and channel relation IN (PUT).
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.Interfaces.OpenApi.V5.IV5VmOpenApiServiceAndChannelRelationInBase" />
    public class V5VmOpenApiServiceAndChannelRelationInBase : IV5VmOpenApiServiceAndChannelRelationInBase
    {
        /// <summary>
        /// Identifier for service.
        /// </summary>
        [JsonIgnore]
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Set to true to delete all existing relations between defined service and service channels (the ChannelRelations collection for this object should be empty collection when this option is used).
        /// </summary>
        public bool DeleteAllChannelRelations { get; set; }

        /// <summary>
        /// Gets or sets the channel relations.
        /// </summary>
        /// <value>
        /// The channel relations.
        /// </value>
        public List<V5VmOpenApiServiceServiceChannelInBase> ChannelRelations { get; set; } = new List<V5VmOpenApiServiceServiceChannelInBase>();        

        /// <summary>
        /// Converts model into version 7.
        /// </summary>
        /// <returns></returns>
        public V7VmOpenApiServiceAndChannelRelationAstiInBase ConvertToLatestVersion()
        {
            var vm = new V7VmOpenApiServiceAndChannelRelationAstiInBase()
            {
                ServiceId = this.ServiceId,
                DeleteAllChannelRelations = this.DeleteAllChannelRelations,
                IsASTI = false
            };
            this.ChannelRelations.ForEach(relation => vm.ChannelRelations.Add(relation.ConvertToLatestVersion()));

            return vm;
        }
    }
}
