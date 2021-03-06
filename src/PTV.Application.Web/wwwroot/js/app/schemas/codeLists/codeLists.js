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
import { schema } from 'normalizr'
import { IntlSchemas } from 'Intl/Schemas'
const { translation } = IntlSchemas

const languageSchema = new schema.Entity('languages', { translation })
const translationLanguageSchema = new schema.Entity('translationLanguages', { translation })
const municipalitySchema = new schema.Entity('municipalities', { translation })
const postalCodeSchema = new schema.Entity('postalCodes', { translation })
const dialCodeSchema = new schema.Entity('dialCodes', { translation })
const provinceSchema = new schema.Entity('provinces', { translation })
const hospitalRegionSchema = new schema.Entity('hospitalRegions', { translation })
const businessRegionSchema = new schema.Entity('businessRegions', { translation })
const countryRegionSchema = new schema.Entity('countries', { translation })

export const CodeListSchemas = {
  LANGUAGE: languageSchema,
  LANGUAGE_ARRAY: new schema.Array(languageSchema),
  TRANSLATED_LANGUAGE: translationLanguageSchema,
  TRANSLATED_LANGUAGE_ARRAY: new schema.Array(translationLanguageSchema),
  MUNICIPALITY: municipalitySchema,
  MUNICIPALITY_ARRAY: new schema.Array(municipalitySchema),
  POSTAL_CODE: postalCodeSchema,
  POSTAL_CODE_ARRAY: new schema.Array(postalCodeSchema),
  PROVINCE: provinceSchema,
  PROVINCE_ARRAY: new schema.Array(provinceSchema),
  HOSPITAL_REGION: hospitalRegionSchema,
  HOSPITAL_REGION_ARRAY: new schema.Array(hospitalRegionSchema),
  BUSINESS_REGION: businessRegionSchema,
  BUSINESS_REGION_ARRAY: new schema.Array(businessRegionSchema),
  DIAL_CODE: dialCodeSchema,
  DIAL_CODE_ARRAY: new schema.Array(dialCodeSchema),
  COUNTRY: countryRegionSchema,
  COUNTRY_ARRAY: new schema.Array(countryRegionSchema)
}
