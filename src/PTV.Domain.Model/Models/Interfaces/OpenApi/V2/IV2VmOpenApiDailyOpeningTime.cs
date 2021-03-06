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

namespace PTV.Domain.Model.Models.Interfaces.OpenApi.V2
{
    /// <summary>
    /// OPEN API V2 - View model interface of Daily oepning hours
    /// </summary>
    public interface IV2VmOpenApiDailyOpeningTime
    {
        /// <summary>
        /// Gets or sets the day from.
        /// </summary>
        /// <value>
        /// The day from.
        /// </value>
        string DayFrom { get; set; }
        /// <summary>
        /// Gets or sets the day to.
        /// </summary>
        /// <value>
        /// The day to.
        /// </value>
        string DayTo { get; set; }
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        string From { get; set; }
        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        string To { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this opening hours are extra.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this opening hours are extra; otherwise, <c>false</c>.
        /// </value>
        bool IsExtra { get; set; }
    }
}
