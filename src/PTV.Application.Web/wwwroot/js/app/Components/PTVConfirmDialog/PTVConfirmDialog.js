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
/*import React, { Component } from 'react';
import { injectIntl } from 'react-intl';
*/import { ButtonCancel, ButtonContinue } from '../../Containers/Common/Buttons';
import React, {Component} from 'react';
import { defineMessages, injectIntl, intlShape, FormattedMessage } from 'react-intl';
//import PTVButton from '../PTVButton';
import PTVIcon from '../PTVIcon';

import styles from  './styles.scss';

export class PTVConfirmDialog extends Component {
    
   constructor(props) {
       super(props);
    }

    render() {
      const {formatMessage} = this.props.intl;
  
      return (
        <div className={"ptv-dialog" + (this.props.show?'':' closed')}>
          <div className="row">
            <div className="col-xs-1 centered">
              <PTVIcon 
                className="dialog-type"
                name="icon-info"
                width={50}
                height={50}
              />
            </div>
            <div className="col-xs-11">
              <p className='text'>{this.props.text}</p>
            </div>
          </div>
          <div className='button-group centered'>
            { this.props.acceptCallback ? <ButtonCancel secondary onClick={ this.props.acceptCallback }> { this.props.acceptButton } </ButtonCancel> : null }
            { this.props.cancelCallback ? <ButtonContinue onClick={ this.props.cancelCallback }> { this.props.cancelButton } </ButtonContinue> : null }               
          </div>
          <div className="clearfix"></div>
        </div>
      )
    }
}
export default injectIntl(PTVConfirmDialog)
