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
using Newtonsoft.Json;
using PTV.Domain.Model.Models.Interfaces;

namespace PTV.Domain.Model.Models
{
    /// <summary>
    /// View Model for coordinates
    /// </summary>
    public class VmCoordinate : VmEntityBase, IVmCoordinate
    {
        /// <summary>
        /// Id of owner entity
        /// </summary>
        [JsonIgnore]
        public Guid? OwnerReferenceId { get; set; }
        /// <summary>
        /// Id of coordinate type
        /// </summary>
        [JsonIgnore]
        public Guid? TypeId { get; set; }
        /// <summary>
        /// Indicates, whether cooridnate is main or not
        /// </summary>
        public bool IsMain { get; set; }
        /// <summary>
        /// latitutde of coordinate
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// longitude of coordinate
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// State of the coordinate
        /// </summary>
        public string CoordinateState { get; set; }
    }
}
