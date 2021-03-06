﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PTV.Domain.Model.Models.V2.Channel.PrintableForm
{
    /// <summary>
    /// 
    /// </summary>
    public class VmPrintableFormOutput : VmServiceChannel, Interfaces.V2.IAttachments
    {
        /// <summary>
        /// Gets or sets the form identifier.
        /// </summary>
        /// <value>
        /// The form identifier.
        /// </value>
        public Dictionary<string, string> FormIdentifier { get; set; }
        /// <summary>
        /// Gets or sets the form receiver.
        /// </summary>
        /// <value>
        /// The form receiver.
        /// </value>
        public Dictionary<string, string> FormReceiver { get; set; }
        /// <summary>
        /// Gets or sets the delivery address.
        /// </summary>
        /// <value>
        /// The delivery address.
        /// </value>
        public VmAddressSimple DeliveryAddress { get; set; }
        /// <summary>
        /// Gets or sets the URL attachments.
        /// </summary>
        /// <value>
        /// The URL attachments.
        /// </value>
        public Dictionary<string, List<VmChannelAttachment>> Attachments { get; set; }
        /// <summary>
        /// Gets or sets the form files.
        /// </summary>
        /// <value>
        /// The web form files.
        /// </value>
        public Dictionary<string, List<VmWebPage>> FormFiles { get; set; }
    }
}
