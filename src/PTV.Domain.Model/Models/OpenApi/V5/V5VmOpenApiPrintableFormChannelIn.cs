﻿/**
 * The MIT License
 * Copyright(c) 2016 Population Register Centre(VRK)
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
using PTV.Framework.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTV.Domain.Model.Models.OpenApi.V5
{
    /// <summary>
    /// OPEN API V5 - View Model of printable from channel for IN api
    /// </summary>
    /// <seealso cref="PTV.Domain.Model.Models.OpenApi.V5.V5VmOpenApiPrintableFormChannelInBase" />
    public class V5VmOpenApiPrintableFormChannelIn : V5VmOpenApiPrintableFormChannelInBase
    {
        /// <summary>
        /// List of localized urls.
        /// </summary>
        [ListRequired(AllowEmptyStrings = false)]
        public override IList<VmOpenApiLocalizedListItem> ChannelUrls
        {
            get
            {
                return base.ChannelUrls;
            }

            set
            {
                base.ChannelUrls = value;
            }
        }

        /// <summary>
        /// PTV organization identifier of organization responsible for this channel.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public override string OrganizationId
        {
            get
            {
                return base.OrganizationId;
            }

            set
            {
                base.OrganizationId = value;
            }
        }

        /// <summary>
        /// List of localized service channel names.
        /// </summary>
        [ListRequired(AllowEmptyStrings = false)]
        public override IList<VmOpenApiLanguageItem> ServiceChannelNames
        {
            get
            {
                return base.ServiceChannelNames;
            }

            set
            {
                base.ServiceChannelNames = value;
            }
        }

        /// <summary>
        /// List of localized service channel descriptions.
        /// </summary>
        [ListRequired(AllowEmptyStrings = false)]
        [ListWithPropertyValueRequired("Type", "ShortDescription")]
        [ListWithPropertyValueRequired("Type", "Description")]
        public override IList<VmOpenApiLocalizedListItem> ServiceChannelDescriptions
        {
            get
            {
                return base.ServiceChannelDescriptions;
            }

            set
            {
                base.ServiceChannelDescriptions = value;
            }
        }

        /// <summary>
        /// Service channel publishing status. Values: Draft or Published.
        /// </summary>
        [AllowedValues(propertyName: "PublishingStatus", allowedValues: new[] { "Draft", "Published" })]
        [ValidEnum(null)] // PTV-1792: suppress base ValidEnum validation
        public override string PublishingStatus
        {
            get => base.PublishingStatus;
            set => base.PublishingStatus = value;
        }

        /// <summary>
        /// Set to true to delete all existing delivery address for the service channel. The DeliveryAddress should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteDeliveryAddress
        {
            get
            {
                return base.DeleteDeliveryAddress;
            }

            set
            {
                base.DeleteDeliveryAddress = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing channel urls for the service channel. The ChannelUrls collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllChannelUrls
        {
            get
            {
                return base.DeleteAllChannelUrls;
            }

            set
            {
                base.DeleteAllChannelUrls = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing attachments for the service channel. The Attachments collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllAttachments
        {
            get
            {
                return base.DeleteAllAttachments;
            }

            set
            {
                base.DeleteAllAttachments = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing support email addresses for the service channel. The SupportEmails collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllSupportEmails
        {
            get
            {
                return base.DeleteAllSupportEmails;
            }

            set
            {
                base.DeleteAllSupportEmails = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing support phone numbers for the service channel. The SupportPhones collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllSupportPhones
        {
            get
            {
                return base.DeleteAllSupportPhones;
            }

            set
            {
                base.DeleteAllSupportPhones = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing form identifiers for the service channel. The form identifiers collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllFormIdentifiers
        {
            get
            {
                return base.DeleteAllFormIdentifiers;
            }

            set
            {
                base.DeleteAllFormIdentifiers = value;
            }
        }

        /// <summary>
        /// Set to true to delete all existing form receivers for the service channel. The form receivers collection should be empty when this property is set to true.
        /// </summary>
        [JsonIgnore]
        public override bool DeleteAllFormReceivers
        {
            get
            {
                return base.DeleteAllFormReceivers;
            }

            set
            {
                base.DeleteAllFormReceivers = value;
            }
        }
    }
}
