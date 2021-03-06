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
import { Map, List, OrderedSet } from 'immutable'
import { EntitySelectors, EnumsSelectors } from 'selectors'
import {
  getPublishingStatusModifiedId,
  getPublishingStatusPublishedId
} from 'selectors/common'
import { getUserOrganization, getUserRoleName } from 'selectors/userInfo'
import { getParameterFromProps } from 'selectors/base'

const getAreaInformationProperty = createSelector(
  EntitySelectors.getEntity,
  entity => entity.get('areaInformation') || null
)

const getIsNew = createSelector(
  EntitySelectors.getEntity,
  entity => !entity.get('id')
)

const getAreaInformationTypesByCode = createSelector(
  EnumsSelectors.areaInformationTypes.getEntities,
  areaInformationTypes =>
    areaInformationTypes.reduce((result, item) => result.set(item.get('code').toLowerCase(), item), Map())
)

const getDefaultAreaInformationType = createSelector(
  getAreaInformationTypesByCode,
  areaInformationTypes =>
    (areaInformationTypes.get('wholecountry') || Map()).get('id') || null
)

const getUserOrganizationIfNew = createSelector(
  [getUserOrganization, getIsNew],
  (userOrganization, isNew) => isNew && userOrganization
)

const createGetOrganizationAreaInformation =
  (defaultSelector, getOrganization = getUserOrganizationIfNew) => createSelector([
    EntitySelectors.organizationAreaInformations.getEntities,
    getOrganization,
    defaultSelector
  ], (
    areaInformations,
    organization,
    defaultValue
  ) =>
    areaInformations.get(organization) || defaultValue
  )

const getEmptyAreaInformation = () => Map()

const getDefaultAreaInformationInner = createSelector(
  getDefaultAreaInformationType,
  areaInformationType => Map({ areaInformationType })
)

const createGetEntityAreaInformationOrDefault = defaultSelector => createSelector(
  [getAreaInformationProperty, defaultSelector],
  (areaInformation, defaultAreaInformation) =>
   (areaInformation || defaultAreaInformation)
)

const createAreaInformationSelector = (areaInformationSelector) => createSelector(
  areaInformationSelector,
  areaInformation =>
    areaInformation && areaInformation
          .update('businessRegions', OrderedSet)
          .update('hospitalRegions', OrderedSet)
          .update('municipalities', OrderedSet)
          .update('provinces', OrderedSet)
)

const createEntityAreaInformationSelector = defaultSelector =>
  createAreaInformationSelector(createGetEntityAreaInformationOrDefault(defaultSelector))

export const getOrganizationAreaInformation = createAreaInformationSelector(
  createGetOrganizationAreaInformation(
    () => null,
    getParameterFromProps('organization')
  )
)

export const getAreaInformationOrDefaultEmpty = createEntityAreaInformationSelector(
  createGetOrganizationAreaInformation(getEmptyAreaInformation)
)

export const getAreaInformationWithDefault = createEntityAreaInformationSelector(
  createGetOrganizationAreaInformation(getDefaultAreaInformationInner)
)

export const getAreaInformationWithDefaultWithoutOrganization = createEntityAreaInformationSelector(
  createGetOrganizationAreaInformation(getDefaultAreaInformationInner)
)

export const getDefaultAreaInformation = createAreaInformationSelector(getDefaultAreaInformationInner)

export const getEntityLanguageAvailabilities = createSelector([
  EntitySelectors.getEntity,
  getPublishingStatusModifiedId,
  getPublishingStatusPublishedId,
  EntitySelectors.languages.getEntities
], (
  entity,
  modifiedStatusId,
  publishedStatusId,
  languages
) => (entity.get('languagesAvailabilities') || List())
  .map(lA =>
    lA.set('newStatusId', (lA.get('statusId') === modifiedStatusId && lA.get('canBePublished'))
      ? publishedStatusId
      : lA.get('statusId'))
      .set('code', languages.getIn([lA.get('languageId'), 'code']))
  ) || List()
)

export const getOrganization = createSelector(
  [EntitySelectors.getEntity, getUserOrganization],
  (entity, userInfo) => entity.get('organization') || userInfo || null
)
export const getAvailableOrganizations = createSelector([
  EnumsSelectors.userOrganizations.getEntities,
  EnumsSelectors.organizations.getEntities,
  getUserRoleName,
  getParameterFromProps('showAll')
],
  (userOrganizations, organizations, role, showAll) => (role === 'Eeva' || showAll) &&
  organizations ||
  userOrganizations ||
  List()
)

export const getAvailableOrganizationsJS = createSelector(
  getAvailableOrganizations,
  userOrganizations => userOrganizations.toJS()
)
