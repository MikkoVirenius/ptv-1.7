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
import { compose } from 'redux'
import { EditorState } from 'draft-js'
import { Label } from 'sema-ui-components'
import { injectIntl, intlShape } from 'react-intl'
import { messages } from '../messages'
import styles from './styles.scss'

const RenderTextEditorDisplay = ({
  input,
  label,
  getCustomValue,
  compare,
  intl: { formatMessage }
}) => {
  const emptyEditorState = EditorState.createEmpty()
  const editorState = getCustomValue
    ? getCustomValue(input.value, emptyEditorState, compare)
    : input.value || emptyEditorState
  const editorStatePlainText = editorState && editorState.getCurrentContent &&
    editorState.getCurrentContent().getPlainText() || ''
  return (
    <div>
      <Label labelText={label} />
      <div className={styles.overflow}>{editorStatePlainText.trim() || <Label infoLabel labelText={formatMessage(messages.emptyMessage)} />}</div>
    </div>
  )
}
RenderTextEditorDisplay.propTypes = {
  input: PropTypes.object,
  getCustomValue: PropTypes.func,
  label: PropTypes.string,
  compare: PropTypes.bool,
  intl: intlShape
}

export default compose(
  injectIntl
)(RenderTextEditorDisplay)

