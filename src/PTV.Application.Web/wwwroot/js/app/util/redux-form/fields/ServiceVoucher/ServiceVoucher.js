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
import { RenderCheckBox, RenderCheckBoxDisplay } from 'util/redux-form/renders'
import { Field } from 'redux-form/immutable'
import { injectIntl, intlShape, defineMessages } from 'react-intl'
import { compose } from 'redux'
import { asComparable, asDisableable, withFormStates } from 'util/redux-form/HOC'

export const voucherMessages = defineMessages({
  checkboxTitle:{
    id: 'Containers.Services.AddService.Step1.Voucher.Active.Checkbox',
    defaultMessage: 'Tässä palvelussa on käytössä palveluseteli'
  }
})

const ServiceVoucher = ({
  intl: { formatMessage },
  isReadOnly,
  ...rest
}) => {
  return (
    !isReadOnly && <Field
      name='areServiceVouchers'
      component={RenderCheckBox}
      label={formatMessage(voucherMessages.checkboxTitle)}
      {...rest}
    />
  )
}
ServiceVoucher.propTypes = {
  intl: intlShape,
  isReadOnly: PropTypes.bool.isRequired
}

export default compose(
  injectIntl,
  asComparable({ DisplayRender: RenderCheckBoxDisplay }),
  asDisableable,
  withFormStates
)(ServiceVoucher)
