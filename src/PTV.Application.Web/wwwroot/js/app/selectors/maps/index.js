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
import { createSelector } from 'reselect'
import { Map } from 'immutable'
import { getMapDNSes } from 'selectors/userInfo'
import { getIntlLocale } from 'Intl/Selectors'

export const getMapDNSForLanguage = createSelector(
    [getIntlLocale, getMapDNSes],
    (lang, dnses) => dnses.get(lang) || ''
)

export const getMapDNSURL = createSelector(
    getMapDNSForLanguage,
    dns => new URL(decodeURI(dns))
)


export const getMaps = state => state.get('maps') || Map()
export const getMapId = (state, props) => props.id

export const getMap = createSelector(
    [getMaps, getMapId],
    (maps, id) => maps.get(id) || Map()
)

export const getMapIsLoading = createSelector(
    getMap,
    map => map.get('isLoading') || false
)

export const getIsEditEnabled = createSelector(
    getMap,
    map => map.get('isEditEnabled') || false
)

