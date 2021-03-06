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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using PTV.Database.DataAccess.Migrations.Base;

namespace PTV.Database.DataAccess.Migrations
{
    public partial class OperationIdInfo : IPartialMigration
    {
        public void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "WebPageTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "WebPageType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "WebpageChannelUrl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "WebpageChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "WebPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Versioning",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "UserOrganization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "TargetGroupName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "TargetGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StreetName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceTargetGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceServiceClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceRequirement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceOntologyTerm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLifeEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLaw",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLanguage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceIndustrialClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceGeneralDescriptionVersioned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceDescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceWebPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceVersioned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceTargetGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannelDigitalAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannelDescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceRequirement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceOntologyTerm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLocationChannelAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLocationChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLifeEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLaw",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLanguageAvailability",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLanguage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceKeyword",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceIndustrialClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChargeTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChargeType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelWebPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelVersioned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelTargetGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelServiceHours",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelServiceClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelOntologyTerm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelLanguageAvailability",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelLanguage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelKeyword",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelDescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelConnectionTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelConnectionType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelAttachment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelAreaMunicipality",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelArea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHourTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHourType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHoursAdditionalInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceFundingTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceFundingType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceElectronicNotificationChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceElectronicCommunicationChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceDescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceClassName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceBlockedAccessRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceAreaMunicipality",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceArea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "RoleTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "RoleType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PublishingStatusTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PublishingStatusType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ProvisionTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ProvisionType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "UserAccessRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrlTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrlType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelReceiver",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelIdentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PostOfficeBoxName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PostalCodeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PostalCode",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PhoneNumberTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "PhoneNumberType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Phone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationWebPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationVersioned",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationServiceAdditionalInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationService",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationLanguageAvailability",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationEmail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationDisplayNameType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationDescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationBlockedAccessRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationAreaMunicipality",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationArea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermParent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermExactMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTerm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "NameTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "NameType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "MunicipalityName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Municipality",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "LifeEventName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "LifeEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "LawWebPage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "LawName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Law",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "LanguageName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Language",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Keyword",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "IndustrialClassName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "IndustrialClass",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ChannelBlockedAccessRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "GeneralDescriptionLanguageAvailability",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "GeneralDescriptionBlockedAccessRight",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "FormTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "FormType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Form",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ExternalSource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ExceptionHoursStatusTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ExceptionHoursStatusType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ExactMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Email",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ElectronicChannelUrl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "ElectronicChannel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DigitalAuthorizationName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DigitalAuthorization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DialCode",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DescriptionTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DescriptionType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "DailyOpeningTime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "CountryName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "CoordinateTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "CoordinateType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Coordinate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Business",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "BugReport",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AttachmentTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AttachmentType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Attachment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaMunicipality",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaInformationTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AreaInformationType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Area",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AddressTypeName",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AddressType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AddressAdditionalInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AccessRightType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastOperationId",
                schema: "public",
                table: "AccessRightName",
                nullable: true);
        }

        public void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "WebPageTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "WebPageType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "WebpageChannelUrl");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "WebpageChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "WebPage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Versioning");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "UserOrganization");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "TargetGroupName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "TargetGroup");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StreetName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceTargetGroup");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceServiceClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceRequirement");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceOntologyTerm");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLifeEvent");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLaw");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceLanguage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceIndustrialClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceGeneralDescriptionVersioned");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "StatutoryServiceDescription");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceWebPage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceVersioned");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceTargetGroup");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannelDigitalAuthorization");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannelDescription");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceServiceClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceRequirement");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceOntologyTerm");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLocationChannelAddress");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLocationChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLifeEvent");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLaw");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLanguageAvailability");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceLanguage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceKeyword");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceIndustrialClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChargeTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChargeType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelWebPage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelVersioned");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelTargetGroup");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelServiceHours");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelServiceClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelPhone");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelOntologyTerm");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelLanguageAvailability");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelLanguage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelKeyword");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelEmail");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelDescription");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelConnectionTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelConnectionType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelAttachment");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelAreaMunicipality");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceChannelArea");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHourTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHourType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceHoursAdditionalInformation");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceFundingTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceFundingType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceElectronicNotificationChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceElectronicCommunicationChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceDescription");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceClassName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceBlockedAccessRight");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceAreaMunicipality");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ServiceArea");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "RoleTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "RoleType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PublishingStatusTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PublishingStatusType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ProvisionTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ProvisionType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "UserAccessRight");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrlTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrlType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelUrl");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelReceiver");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannelIdentifier");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PrintableFormChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PostOfficeBoxName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PostalCodeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PostalCode");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PhoneNumberTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "PhoneNumberType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Phone");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationWebPage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationVersioned");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationServiceAdditionalInformation");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationService");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationPhone");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationLanguageAvailability");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationEmail");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationDisplayNameType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationDescription");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationBlockedAccessRight");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationAreaMunicipality");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationArea");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OrganizationAddress");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermParent");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTermExactMatch");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "OntologyTerm");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "NameTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "NameType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "MunicipalityName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Municipality");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "LifeEventName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "LifeEvent");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "LawWebPage");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "LawName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Law");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "LanguageName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "IndustrialClassName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "IndustrialClass");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ChannelBlockedAccessRight");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "GeneralDescriptionLanguageAvailability");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "GeneralDescriptionBlockedAccessRight");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "FormTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "FormType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ExternalSource");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ExceptionHoursStatusTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ExceptionHoursStatusType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ExactMatch");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ElectronicChannelUrl");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "ElectronicChannel");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DigitalAuthorizationName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DigitalAuthorization");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DialCode");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DescriptionTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DescriptionType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "DailyOpeningTime");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "CountryName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "CoordinateTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "CoordinateType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Coordinate");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "BugReport");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AttachmentTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AttachmentType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaMunicipality");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaInformationTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AreaInformationType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AddressTypeName");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AddressType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AddressAdditionalInformation");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AccessRightType");

            migrationBuilder.DropColumn(
                name: "LastOperationId",
                schema: "public",
                table: "AccessRightName");
        }
    }
}
