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
using System.Text.RegularExpressions;
using PTV.MapServer.ExceptionHandler;

namespace PTV.MapServer.Common
{
    public class RequestValidation
    {
        private const string VersionFormat = @"^(\d{1,2}\.\d{1,2}\.\d{1,2})$";

        public static void ValidateKnownService(string service)
        {
            if (service == null)
            {
                throw new OwsException(
                    OwsExceptionCodeEnum.MissingParameterValue,
                    "service",
                    CoreMessages.NoServiceWasSpecified);
            }

            KnownOcgServiceEnum knownOcgService;
            if (!Enum.TryParse(service, true, out knownOcgService))
            {
                throw new OwsException(
                    OwsExceptionCodeEnum.InvalidParameterValue,
                    "service",
                    string.Format(CoreMessages.SpecifiedServiceIsNotKnownOcgService, service));
            }
        }

        public static SupportedOcgServiceEnum ValidateSupportedService(string service)
        {
            SupportedOcgServiceEnum supportedService;
            if (!Enum.TryParse(service, true, out supportedService))
            {
                throw new OwsException(
                    OwsExceptionCodeEnum.NoApplicableCode,
                    "service",
                    new[]
                    {
                        string.Format(CoreMessages.SpecifiedServiceIsNotSupported, service),
                        $"Supported services: '{string.Join(", ", Enum.GetNames(typeof (SupportedOcgServiceEnum))).ToUpper()}'"
                    });
            }

            return supportedService;
        }

        public static void ValidateVersion(string version, string parameter)
        {
            if (!Regex.IsMatch(version, VersionFormat))
            {
                throw new OwsException(OwsExceptionCodeEnum.InvalidParameterValue, parameter,
                    new[]
                    {
                            $"Version '{version}' is not in correct format.",
                            "Correct format is: 'X.Y.Z"
                    });
            }
        }
    }
}
