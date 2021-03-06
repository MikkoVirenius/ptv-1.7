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
import { Field } from 'redux-form/immutable'
import { RenderSelect, RenderSelectDisplay, RenderTextFieldDisplay } from 'util/redux-form/renders'
import { injectIntl, intlShape, defineMessages } from 'react-intl'
import { compose } from 'redux'
import { connect } from 'react-redux'
import injectSelectPlaceholder from 'appComponents/SelectPlaceholderInjector'
import { getMunicipalitiesJS } from './selectors'
import { translateValidation } from 'util/redux-form/validators'
import { localizeList } from 'appComponents/Localize'
import { asDisableable, asComparable } from 'util/redux-form/HOC'

const messages = defineMessages({
  municipalityNameTitle: {
    id: 'Containers.Manage.Organizations.Manage.Step1.Municpality.Name.Title',
    defaultMessage: 'Kuntanimi'
  },
  municipalityNamePlaceholder: {
    id: 'Containers.Manage.Organizations.Manage.Step1.Municpality.Name.Placeholder',
    defaultMessage: 'Kirjoita kunnan nimi'
  },
  municipalityNameTooltip: {
    id: 'Containers.Manage.Organizations.Manage.Step1.Municpality.Name.Tooltip',
    defaultMessage: 'Kirjoita kunnan nimi.'
  },
  municipalityNumberTitle: {
    id: 'Containers.Manage.Organizations.Manage.Step1.Municpality.Number.Title',
    defaultMessage: 'Kuntanumero'
  }
})

const onRenderMunicipalityOption = (options) => {
  return `${options.code} ${options.label}`
}

const getCustomValue = (options, id) => {
  return id && options.find(x => x.value === id).code
}

const Municipality = ({
  intl: { formatMessage },
  validate,
  ...rest
}) => (
  <div className='row'>
    <div className='col-lg-12'>
      <Field
        name='municipality'
        component={RenderSelect}
        label={formatMessage(messages.municipalityNameTitle)}
        tooltip={formatMessage(messages.municipalityNameTooltip)}
        placeholder={formatMessage(messages.municipalityNamePlaceholder)}
        //validate={translateValidation(validate, formatMessage, messages.municipalityNameTitle)}
        optionRenderer={onRenderMunicipalityOption}
        searchable
        {...rest}
  />
    </div>
    <div className='col-lg-12'>
      <Field
        name='municipality'
        label={formatMessage(messages.municipalityNumberTitle)}
        getCustomValue={(id) => getCustomValue(rest.options, id)}
        {...rest}
        component={RenderTextFieldDisplay}
  />
    </div>
  </div>
)
Municipality.propTypes = {
  intl: intlShape,
  validate: PropTypes.func
}

export default compose(
  injectIntl,
  asDisableable,
  connect(state => ({
    options: getMunicipalitiesJS(state)
  })),
  localizeList({
    input: 'options',
    idAttribute: 'value',
    nameAttribute: 'label',
    isSorted: true
  }),
  asComparable({ DisplayRender: RenderSelectDisplay }),
  injectSelectPlaceholder()
)(Municipality)
