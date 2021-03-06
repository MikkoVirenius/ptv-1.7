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
using PTV.Domain.Model.Models.Interfaces.Localization;

namespace PTV.Domain.Model.Models.Localization
{
    /// <summary>
    /// View model of language messages
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.Interfaces.Localization.IVmLanguageMessages" />
    public class VmLanguageMessages : IVmLanguageMessages
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VmLanguageMessages"/> class.
        /// </summary>
        public VmLanguageMessages()
        {
            Texts = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        /// <value>
        /// The language code.
        /// </value>
        public string LanguageCode { get; set; }
        /// <summary>
        /// Gets or sets the texts.
        /// </summary>
        /// <value>
        /// The texts.
        /// </value>
        public IDictionary<string, string> Texts { get; set; }
    }

    /// <summary>
    /// View model of translation item
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.VmEntityBase" />
    /// <seealso cref="PTV.Domain.Model.Models.Interfaces.Localization.IVmTranslationItem" />
    public class VmTranslationItem : VmEntityBase, IVmTranslationItem
    {
        /// <summary>
        /// Gets or sets the texts.
        /// </summary>
        /// <value>
        /// The texts.
        /// </value>
        public IDictionary<string, string> Texts { get; set; }
    }
}
