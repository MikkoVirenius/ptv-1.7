﻿
using System;
/**
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
namespace PTV.Domain.Model.Models
{
    /// <summary>
    /// View model of form state search
    /// </summary>
    public class VmFormStateSearch
    {
        /// <summary>
        /// Id of saved form state
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// EntityType of saved form state
        /// </summary>
        public string EntityType { get; set; }
        /// <summary>
        /// EntityId of saved form state
        /// </summary>
        public string EntityId { get; set; }
        /// <summary>
        /// LanguageId of saved form state
        /// </summary>
        public Guid LanguageId { get; set; }
        /// <summary>
        /// UserId of saved form state
        /// </summary>
        public string UserName { get; set; }
    }
}
