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

using PTV.Domain.Model.Models.Interfaces.OpenApi.V5;
using PTV.Domain.Model.Models.OpenApi.Extensions;
using PTV.Domain.Model.Models.OpenApi.V4;

namespace PTV.Domain.Model.Models.OpenApi.V5
{
    /// <summary>
    /// OPEN API V5 - View Model of address with type and coordinates
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.OpenApi.V5.V5VmOpenApiAddressWithType" />
    /// <seealso cref="PTV.Domain.Model.Models.Interfaces.OpenApi.V5.IV5VmOpenApiAddressWithTypeAndCoordinates" />
    public class V5VmOpenApiAddressWithTypeAndCoordinates : V5VmOpenApiAddressWithType, IV5VmOpenApiAddressWithTypeAndCoordinates
    {
        /// <summary>
        /// Location latitude coordinate.
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Location longitude coordinate.
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// State of coordinates. Coordinates are fetched from a service provided by Maanmittauslaitos (WFS).
        /// Possible values are: Loading, Ok, Failed, NotReceived, EmptyInputReceived, MultipleResultsReceived, WrongFormatReceived or EnteredByUser.
        /// </summary>
        public string CoordinateState { get; set; }

        /// <summary>
        /// Converts to version 4.
        /// </summary>
        /// <returns>converted model to version 4</returns>
        public V4VmOpenApiAddressWithTypeAndCoordinates ConvertToVersion4()
        {
            var vm = ConvertToVersion<V4VmOpenApiAddressWithTypeAndCoordinates>();
            vm.Type = Type;
            vm.OwnerReferenceId = OwnerReferenceId;
            vm.Latitude = Latitude;
            vm.Longitude = Longitude;
            vm.CoordinateState = CoordinateState;

            vm.PostOfficeBox = PostOfficeBox.ValueString();

            return vm;
        }
    }
}
