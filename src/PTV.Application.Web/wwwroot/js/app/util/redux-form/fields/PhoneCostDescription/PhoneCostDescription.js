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
import { RenderTextField, RenderTextFieldDisplay } from 'util/redux-form/renders'
import { Field } from 'redux-form/immutable'
import { injectIntl, intlShape, defineMessages } from 'react-intl'
import { compose } from 'redux'
import { asDisableable, asComparable } from 'util/redux-form/HOC'

const messages = defineMessages({
  label: {
    id: 'Containers.Channels.AddElectronicChannel.Step1.PhoneCostOtherDescription.Title',
    description: 'ReduxForm.Renders.RenderPhoneCostDescription.Label',
    defaultMessage: 'Puhelun hintatiedot'
  },
  placeholder: {
    id: 'Containers.Channels.AddElectronicChannel.Step1.PhoneCostOtherDescription.Placeholder',
    description: 'ReduxForm.Renders.RenderPhoneCostDescription.Placeholder',
    defaultMessage: 'esim. Pvm:n lisäksi jonotuksesta veloitetaan...'
  },
  tooltip: {
    id: 'Containers.Channels.AddElectronicChannel.Step1.PhoneCostOtherDescription.Tooltip',
    kokot: 'ReduxForm.Renders.RenderPhoneCostDescription.Tooltip',
    description: 'ReduxForm.Renders.RenderPhoneCostDescription.Tooltip',
    defaultMessage: 'Kerro sanallisesti, mitä puhelinnumeroon soittaminen maksaa.'
  }
})

const PhoneCostDescription = ({
  intl: { formatMessage },
  ...rest
}) => (
  <Field
    name='chargeDescription'
    component={RenderTextField}
    label={formatMessage(messages.label)}
    placeholder={formatMessage(messages.placeholder)}
    tooltip={formatMessage(messages.tooltip)}
    multiline
    rows={3}
    {...rest}
  />
)
PhoneCostDescription.propTypes = {
  intl: intlShape
}

export default compose(
  injectIntl,
  asComparable({ DisplayRender: RenderTextFieldDisplay }),
  asDisableable
)(PhoneCostDescription)
