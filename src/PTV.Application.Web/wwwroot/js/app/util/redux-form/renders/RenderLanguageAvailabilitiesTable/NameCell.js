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
import { connect } from 'react-redux'
import { compose } from 'redux'
import { setContentLanguage } from 'reducers/selections'
import { getContentLanguageId, getContentLanguageCode } from 'selectors/selections'
import { Label, Button } from 'sema-ui-components'
import styles from './styles.scss'
import cx from 'classnames'
import * as commonMessages from 'Routes/messages'
import { injectIntl, intlShape } from 'react-intl'
import { EnumsSelectors } from 'selectors'
import { Map } from 'immutable'
import { getFormValue } from 'selectors/base'

const NameCell = ({
  intl: { formatMessage },
  name,
  languageId,
  languageCode,
  setContentLanguage,
  isActive
}) => {
  const handleOnClick = () => setContentLanguage({
    id: languageId,
    code: languageCode
  })
  return (
    isActive
      ? <div onClick={handleOnClick} className={styles.activeEntityName}>{
        name || formatMessage(commonMessages.messages.languageVersionTitleNew)}</div>
      : <Button link onClick={handleOnClick}>{name || formatMessage(commonMessages.messages.languageVersionTitleNew)}</Button>
  )
}
NameCell.propTypes = {
  name: PropTypes.string.isRequired,
  intl: intlShape,
  languageId: PropTypes.string.isRequired,
  languageCode: PropTypes.string.isRequired,
  setContentLanguage: PropTypes.func.isRequired,
  isActive: PropTypes.bool.isRequired
}

export default compose(
  injectIntl,
  connect((state, { languageId, name, formName, useAlter, alterName }) => {
    const tLanguages = EnumsSelectors.translationLanguages.getEntitiesMap(state)
    const languageCode = (tLanguages.get(languageId) || Map()).get('code')
    const nameFromForm = formName && getFormValue('name')(state, { formName }) || Map()
    const useAlterFromForm = formName && getFormValue('isAlternateNameUsed')(state, { formName }) || Map()
    const alterNameFromForm = formName && getFormValue('alternateName')(state, { formName }) || Map()
    const useAlterName = useAlterFromForm.get(languageCode) || useAlter.get(languageCode)
    return {
      isActive: (languageId === undefined) ||
      getContentLanguageId(state, { formName }) === languageId ||
      getContentLanguageCode(state, { formName }) === languageCode,
      name: useAlterName
      ? alterNameFromForm.get(languageCode) || alterName.get(languageCode)
      : nameFromForm.get(languageCode) || name.get(languageCode),
      languageCode
    }
  }, {
    setContentLanguage
  })
)(NameCell)
