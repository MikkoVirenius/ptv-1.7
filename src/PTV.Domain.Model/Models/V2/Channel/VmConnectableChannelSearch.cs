﻿using PTV.Domain.Model.Enums;
using PTV.Domain.Model.Enums.Security;
using PTV.Domain.Model.Models.Interfaces;
using PTV.Domain.Model.Models.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTV.Domain.Model.Models.V2.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class VmConnectableChannelSearch : IVmConnectableChannelSearch, IVmLocalized, IVmSearchParamsBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public LanguageCode Language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? OrganizationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChannelType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DomainEnum Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxPageCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Skip { get; set; }
    }
}
