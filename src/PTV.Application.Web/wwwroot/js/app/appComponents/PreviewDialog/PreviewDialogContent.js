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
import React from 'react'
import PropTypes from 'prop-types'
import { reduxForm } from 'redux-form/immutable'
import { compose } from 'redux'
import { connect } from 'react-redux'
import { injectIntl, intlShape } from 'react-intl'
import { getElectronicChannel } from 'Routes/Channels/routes/Electronic/selectors'
import { getWebPageChannel } from 'Routes/Channels/routes/WebPage/selectors'
import { getPrintableForm } from 'Routes/Channels/routes/PrintableForm/selectors'
import { getPhoneChannel } from 'Routes/Channels/routes/Phone/selectors'
import { getServiceLocationChannel } from 'Routes/Channels/routes/ServiceLocation/selectors'
import { getService } from 'Routes/Service/selectors'
import { getGeneralDescription } from 'Routes/GeneralDescription/selectors'
import { getOrganizationFormInitialValues } from 'Routes/Organization/selectors'
import ElectronicChannelBasic from 'Routes/Channels/routes/Electronic/components/ElectronicChannelBasic'
import WebPageBasic from 'Routes/Channels/routes/WebPage/components/WebPageBasic'
import PrintableFormBasic from 'Routes/Channels/routes/PrintableForm/components/PrintableFormBasic'
import PhoneChannelBasic from 'Routes/Channels/routes/Phone/components/PhoneChannelBasic'
import ServiceLocationBasic from 'Routes/Channels/routes/ServiceLocation/components/ServiceLocationBasic'
import ServiceBasic from 'Routes/Service/components/ServiceBasic'
import GeneralDescriptionBasic from 'Routes/GeneralDescription/components/GeneralDescriptionBasic'
import OrganizationBasic from 'Routes/Organization/components/OrganizationFormBasic'
import ConnectedEntities from 'appComponents/ConnectedEntities'
import { formTypesEnum, entityConcreteTypesEnum, entityTypesEnum } from 'enums'
import { getAttachedGDId } from './selectors'

const PreviewDialogContent = ({
  concreteType,
  attachedGDId,
  previewLanguageCode,
  intl: { formatMessage },
  ...rest
}) => {
  switch (concreteType) {
    case entityConcreteTypesEnum.ELECTRONICCHANNEL:
      return <div>
        <ElectronicChannelBasic />
        <ConnectedEntities
          type={entityTypesEnum.CHANNELS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.WEBPAGECHANNEL:
      return <div>
        <WebPageBasic />
        <ConnectedEntities
          type={entityTypesEnum.CHANNELS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.PRINTABLEFORMCHANNEL:
      return <div>
        <PrintableFormBasic />
        <ConnectedEntities
          type={entityTypesEnum.CHANNELS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.PHONECHANNEL:
      return <div>
        <PhoneChannelBasic />
        <ConnectedEntities
          type={entityTypesEnum.CHANNELS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.SERVICELOCATIONCHANNEL:
      return <div>
        <ServiceLocationBasic />
        <ConnectedEntities
          type={entityTypesEnum.CHANNELS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.SERVICE:
      return <div>
        <ServiceBasic generalDescriptionId={attachedGDId} previewLanguageCode={previewLanguageCode} />
        <ConnectedEntities
          type={entityTypesEnum.SERVICES}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.GENERALDESCRIPTION:
      return <div>
        <GeneralDescriptionBasic />
        <ConnectedEntities
          type={entityTypesEnum.GENERALDESCRIPTIONS}
          id={rest.id}
        />
      </div>
    case entityConcreteTypesEnum.ORGANIZATION:
      return <OrganizationBasic />
    default:
      return <div>Entity not defined</div>
  }
}
PreviewDialogContent.propTypes = {
  concreteType: PropTypes.string,
  attachedGDId: PropTypes.string,
  previewLanguageCode: PropTypes.string,
  intl: intlShape
}

export default compose(
  injectIntl,
  connect((state, ownProps) => {
    let initialValues = {}
    let attachedGDId = null
    switch (ownProps.concreteType) {
      case entityConcreteTypesEnum.ELECTRONICCHANNEL:
        initialValues = getElectronicChannel(state, ownProps)
        break
      case entityConcreteTypesEnum.WEBPAGECHANNEL:
        initialValues = getWebPageChannel(state, ownProps)
        break
      case entityConcreteTypesEnum.PRINTABLEFORMCHANNEL:
        initialValues = getPrintableForm(state, ownProps)
        break
      case entityConcreteTypesEnum.PHONECHANNEL:
        initialValues = getPhoneChannel(state, ownProps)
        break
      case entityConcreteTypesEnum.SERVICELOCATIONCHANNEL:
        initialValues = getServiceLocationChannel(state, ownProps)
        break
      case entityConcreteTypesEnum.SERVICE:
        initialValues = getService(state, ownProps)
        attachedGDId = getAttachedGDId(state, ownProps)
        break
      case entityConcreteTypesEnum.GENERALDESCRIPTION:
        initialValues = getGeneralDescription(state, ownProps)
        break
      case entityConcreteTypesEnum.ORGANIZATION:
        initialValues = getOrganizationFormInitialValues(state, ownProps)
        break
      default:
        initialValues
        break
    }
    return {
      initialValues,
      attachedGDId
    }
  }),
  reduxForm({
    form: formTypesEnum.PREVIEW,
    enableReinitialize: true
  })
)(PreviewDialogContent)
