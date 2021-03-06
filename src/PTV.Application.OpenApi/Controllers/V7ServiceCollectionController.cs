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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PTV.Application.OpenApi.Models;
using PTV.Database.DataAccess.Interfaces.Services;
using PTV.Domain.Model.Models.Interfaces.OpenApi;
using PTV.Domain.Model.Models.OpenApi.V3;
using PTV.Framework;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net;
using PTV.Framework.Attributes;
using Microsoft.Extensions.Logging;
using PTV.Database.DataAccess.Interfaces.Services.Security;
using PTV.Framework.Enums;
using PTV.Domain.Model.Models.OpenApi.V7;

namespace PTV.Application.OpenApi.Controllers
{
    /// <summary>
    /// PTV Open Api service collections related methods.
    /// </summary>
    [Authorize(ActiveAuthenticationSchemes = "Bearer", Roles = "Eeva,Pete")]
    [Route("api/v7/ServiceCollection")]
    public class V7ServiceCollectionController : ServiceCollectionBaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="V7ServiceCollectionController"/> class.
        /// </summary>
        /// <param name="serviceCollectionService">The service collection service.</param>
        /// <param name="commonService">The common service.</param>
        /// <param name="serviceService">The service service.</param>
        /// <param name="userOrganizationService">The user organization service.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="logger">The logger.</param>
        public V7ServiceCollectionController(
            IServiceCollectionService serviceCollectionService,
            ICommonService commonService,
            IServiceService serviceService,
            IUserOrganizationService userOrganizationService,
            IOptions<AppSettings> settings,
            ILogger<V7ServiceCollectionController> logger)
            : base(serviceCollectionService, commonService, serviceService, userOrganizationService, settings, logger, 7)            
        {
        }

        /// <summary>
        /// Gets all the published service collections within PTV as a list of service collection ids and names.
        /// Service collections created after certain date can be fetched by adding date as query string parameter.
        /// Archived items can be fetched by setting parameter archived to true.
        /// </summary>
        /// <param name="date">Supports only format "yyyy-MM-ddTHH:mm:ss" (UTC).</param>
        /// <param name="page">The page number to be fetched.</param>
        /// <param name="archived">Get archived items by setting archived to true.</param>
        /// <returns>A list of service collection ids and names with paging.</returns>
        /// <remarks>
        /// <para>HTTP status code 400 response model is a dictionary where key is property name and value is a list of error messages.</para>
        /// <code>
        /// {
        ///     "date": [
        ///         "The value 'rfsd' is not valid for Nullable`1.",
        ///         "The date parameter is invalid."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        [AllowAnonymous]
        [HttpGet]
        [ValidateDate]
        [ProducesResponseType(typeof(V3VmOpenApiGuidPage), (int)HttpStatusCode.OK)] // required otherwise swagger generates 204 content response which isn't used
        [SwaggerResponse((int)HttpStatusCode.OK, description: "A list of service collection ids with paging.", type: typeof(V3VmOpenApiGuidPage))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: CoreMessages.OpenApi.BadRequestGeneralMessage, type: typeof(Dictionary<string, IList<string>>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Invalid version number.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: CoreMessages.OpenApi.InternalServerErrorDescripton, type: typeof(IVmError))]
        public IActionResult Get([FromQuery]DateTime? date, [FromQuery]int page = 1, [FromQuery]bool archived = false)
        {
            return base.GetIdAndNameList(date, page, archived);
        }

        /// <summary>
        /// Fetches all the information related to a single service collection.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Detailed information about a service collection.</returns>
        /// <remarks>
        /// <para>HTTP status code 400 response model is a dictionary where key is property name and value is a list of error messages.</para>
        /// <code>
        /// {
        ///     "id": [
        ///         "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ValidateId("id")]
        [ProducesResponseType(typeof(V7VmOpenApiServiceCollection), (int)HttpStatusCode.OK)] // required otherwise swagger generates 204 content response which isn't used
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Detailed information about a service collection.", type: typeof(V7VmOpenApiServiceCollection))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: CoreMessages.OpenApi.BadRequestGeneralMessage, type: typeof(Dictionary<string, IList<string>>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: CoreMessages.OpenApi.NotFoundGeneralMessage, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: CoreMessages.OpenApi.InternalServerErrorDescripton, type: typeof(IVmError))]
        public IActionResult Get(string id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// Creates a new service collection with the data provided as input.
        /// </summary>
        /// <param name="request">The service collection data.</param>
        /// <returns>The created service collection.</returns>
        /// <remarks>
        /// <para>HTTP status code 400 response model is a dictionary where key is property name and value is a list of error messages.</para>
        /// <code>
        /// {
        ///     "ServiceCollectionNames": [
        ///         "Type - Required value 'Name' was not found!"
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        // POST api/servicecollection
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [AccessRightRequirement(AccessRightEnum.OpenApiWrite)]
        [HttpPost]
        [ProducesResponseType(typeof(V7VmOpenApiServiceCollection), (int)HttpStatusCode.OK)] // required otherwise swagger generates 204 content response which isn't used
        [SwaggerResponse((int)HttpStatusCode.OK, description: "The created service collection.", type: typeof(V7VmOpenApiServiceCollection))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Invalid version number.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: CoreMessages.OpenApi.BadRequestGeneralMessage, type: typeof(Dictionary<string, IList<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: CoreMessages.OpenApi.InternalServerErrorDescripton, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, description: CoreMessages.OpenApi.UnauthorizedGeneralMessage)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, description: CoreMessages.OpenApi.ForbiddenGeneralMessage)]
        public IActionResult Post([FromBody]V7VmOpenApiServiceCollectionIn request)
        {
            return base.Post(request);
        }

        /// <summary>
        /// Updates the defined service collection with the data provided as input.
        /// </summary>
        /// <param name="id">Service collection identifier</param>
        /// <param name="request">The service collection data</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// <para>HTTP status code 400 response model is a dictionary where key is property name and value is a list of error messages.</para>
        /// <code>
        /// {
        ///     "ServiceCollectionNames[0].Type": [
        ///         "The Type field is required."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        // PUT api/servicecollection/5
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [AccessRightRequirement(AccessRightEnum.OpenApiWrite)]
        [HttpPut("{id}")]
        [ValidateId("id")]
        [ProducesResponseType(typeof(V7VmOpenApiServiceCollection), (int)HttpStatusCode.OK)] // required otherwise swagger generates 204 content response which isn't used
        [SwaggerResponse((int)HttpStatusCode.OK, description: "The updated service collection.", type: typeof(V7VmOpenApiServiceCollection))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: CoreMessages.OpenApi.BadRequestGeneralMessage, type: typeof(Dictionary<string, IList<string>>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: CoreMessages.OpenApi.NotFoundGeneralMessage, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: CoreMessages.OpenApi.InternalServerErrorDescripton, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, description: CoreMessages.OpenApi.UnauthorizedGeneralMessage)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, description: CoreMessages.OpenApi.ForbiddenGeneralMessage)]
        public IActionResult Put(string id, [FromBody]V7VmOpenApiServiceCollectionInBase request)
        {
            return base.Put(request, id);
        }

        /// <summary>
        /// Updates the defined service collection with the data provided as input.
        /// </summary>
        /// <param name="sourceId">External source identifier</param>
        /// <param name="request">The service collection data</param>
        /// <returns>The updated service collection.</returns>
        /// <remarks>
        /// <para>HTTP status code 400 response model is a dictionary where key is property name and value is a list of error messages.</para>
        /// <code>
        /// {
        ///     "ServiceCollectionNames[0].Type": [
        ///         "The Type field is required."
        ///     ]
        /// }
        /// </code>
        /// </remarks>
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        [AccessRightRequirement(AccessRightEnum.OpenApiWrite)]
        [HttpPut("sourceId/{sourceId}")]
        [ValidateRegEx("sourceId", @"^[A-Za-z0-9-.]*$")]
        [ProducesResponseType(typeof(V7VmOpenApiServiceCollection), (int)HttpStatusCode.OK)] // required otherwise swagger generates 204 content response which isn't used
        [SwaggerResponse((int)HttpStatusCode.OK, description: "The updated service collection.", type: typeof(V7VmOpenApiServiceCollection))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: CoreMessages.OpenApi.NotFoundGeneralMessage, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: CoreMessages.OpenApi.BadRequestGeneralMessage, type: typeof(Dictionary<string, IList<string>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, description: CoreMessages.OpenApi.InternalServerErrorDescripton, type: typeof(IVmError))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, description: CoreMessages.OpenApi.UnauthorizedGeneralMessage)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, description: CoreMessages.OpenApi.ForbiddenGeneralMessage)]
        public IActionResult PutBySource(string sourceId, [FromBody]V7VmOpenApiServiceCollectionInBase request)
        {
            return base.Put(request, sourceId: sourceId);
        }
    }
}
