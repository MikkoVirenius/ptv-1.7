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
import { createSelector } from 'reselect'
import entitySelector from 'selectors/entities'
import styles from './styles.scss'
import cx from 'classnames'
import { getPublishingStatusDraftCode } from 'selectors/common'

const getPublishingStatuses = createSelector(
  [
    entitySelector.languages.getEntities,
    entitySelector.publishingStatuses.getEntities,
    (_, { languageId }) => languageId || null,
    (_, { statusId }) => statusId || null,
    getPublishingStatusDraftCode
  ], (
    languages,
    statuses,
    languageId,
    statusId,
    pSdraftCode
  ) => ({
    code: languages.getIn([languageId, 'code']) || '?',
    status: statuses.getIn([statusId, 'code']) || pSdraftCode
  })
)

const LanguageStatusSquare = ({
  code,
  status,
  languageId,
  isArchived,
  onClick,
  size
}) => {
  const languageStatusClass = cx(
    styles.languageStatus,
    styles[size],
    styles[isArchived ? 'deleted' : status.toLowerCase()]
  )
  return (
    <div
      onClick={() => onClick && onClick({ languageId, code })}
      className={languageStatusClass}
    >
      {code.toUpperCase()}
    </div>
  )
}

LanguageStatusSquare.propTypes = {
  code: PropTypes.string,
  status: PropTypes.string,
  isArchived: PropTypes.bool.isRequired,
  languageId: PropTypes.string,
  onClick: PropTypes.func,
  size: PropTypes.string
}

LanguageStatusSquare.defaultProps = {
  size: 's20'
}

export default compose(
  connect(
    (state, ownProps) => ({
      ...getPublishingStatuses(state, ownProps)
    })
  )
)(LanguageStatusSquare)
