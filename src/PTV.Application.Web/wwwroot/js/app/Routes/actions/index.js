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
import { apiCall3 } from 'actions'
import { EntitySchemas } from 'schemas'
import {
  formGetActionTypes,
  formEntityTypes,
  formApiPaths,
  formTypesEnum,
  formActionsTypesEnum,
  formActions
} from 'enums'
import { change } from 'redux-form/immutable'
import { getOrganizationAreaInformation, getDefaultAreaInformation } from 'Routes/selectors'
import { Map } from 'immutable'
// why it cannot be in enums???? it throws weird exception
const formSchemas = {
  [formTypesEnum.SERVICEFORM]: EntitySchemas.SERVICE_FORM,
  [formTypesEnum.GENERALDESCRIPTIONFORM]: EntitySchemas.GENERAL_DESCRIPTION,
  [formTypesEnum.ORGANIZATIONFORM]: EntitySchemas.ORGANIZATION,
  [formTypesEnum.ELECTRONICCHANNELFORM]: EntitySchemas.CHANNEL,
  [formTypesEnum.PHONECHANNELFORM]: EntitySchemas.CHANNEL,
  [formTypesEnum.PRINTABLEFORM]: EntitySchemas.CHANNEL,
  [formTypesEnum.SERVICELOCATIONFORM]: EntitySchemas.CHANNEL,
  [formTypesEnum.WEBPAGEFORM]: EntitySchemas.CHANNEL,
  [formTypesEnum.GENERALDESCRIPTIONSEARCHFORM]: '/noschema'
}

export const unLockEntity = (entityId, formName) =>
      apiCall3({
        keys: [formEntityTypes[formName], 'unLock'],
        payload: {
          endpoint: formActions[formActionsTypesEnum.UNLOCKENTITY][formName],
          data: { id: entityId }
        },
        schemas: formSchemas[formName],
        formName
      })

export const createGetEntityAction = (id, formName) => {
  return (
    apiCall3({
      keys: [formEntityTypes[formName], 'load'],
      payload: {
        endpoint: formApiPaths[formName] + '/' + formGetActionTypes[formName],
        data: { id }
      },
      schemas: formSchemas[formName],
      formName
    })
  )
}

const setAreaInformation = ({ formName, areaInformation, dispatch }) => {
  dispatch(
    change(
      formName,
      'areaInformation',
      areaInformation
    )
  )
}

export const loadDefaultAreaInformationForOrganization = (organization, formName, useDefaultArea = false) =>
  ({ getState, dispatch }) => {
    if (!organization) {
      setAreaInformation({
        formName,
        areaInformation: useDefaultArea ? getDefaultAreaInformation(getState()) : Map(),
        dispatch
      })
      return
    }

    // commented out, to get refreshed organization data,
    // if this will cause performance issues, need to find other way how to refresh data
    // const areaInformation = getOrganizationAreaInformation(getState(), { organization })
    // if (areaInformation && areaInformation.size) {
    //   setAreaInformation({
    //     formName,
    //     areaInformation,
    //     dispatch
    //   })
    // } else {
    dispatch(apiCall3({
      keys: [formName, 'areaType'],
      payload: {
        endpoint: 'common/GetDefaultAreaInformation',
        data: { organizationId: organization }
      },
      schemas: EntitySchemas.ORGANIZATION_AREA_INFORMATION,
      formName,
      successNextAction: response =>
          setAreaInformation({
            formName,
            // getState is called in success callback to get fresh data from api call
            areaInformation: getOrganizationAreaInformation(getState(), { organization }),
            dispatch
          })
    }))
    // }
  }
