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
using PTV.Framework.Converters;

namespace PTV.Domain.Model.Models.Converters
{
    /// <summary>
    /// Converts list item to list
    /// </summary>
    public class ListItemListConverter : VmGuidListConverter<VmListItem>
    {
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override VmListItem GetObject(Guid? id)
        {
            return id.HasValue ? new VmListItem { Id = id.Value } : null;
        }
    }

    /// <summary>
    /// List item converter
    /// </summary>
    /// <seealso cref="PTV.Framework.Converters.VmGuidConverter" />
    public class ListItemConverter : VmGuidConverter
    {
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public override object GetObject(Guid? id)
        {
            return id.HasValue ? new VmListItem { Id = id.Value } : null;
        }

    }
}