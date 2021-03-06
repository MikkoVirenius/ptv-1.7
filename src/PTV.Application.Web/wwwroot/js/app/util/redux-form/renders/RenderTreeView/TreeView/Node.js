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
import ImmutablePropTypes from 'react-immutable-proptypes'
import cx from 'classnames'
import styles from '../styles.scss'
import ReactList from 'react-list'
import { NodeLabel } from './Labels'
import ChildrenIcon from './ChildrenIcon'
import { composeNode } from './hoc'

export const Nodes = ({
    nodes,
    isRootLevel,
    ...props
  }) => {
  if (nodes.size === 0) {
    return null
  }

  const renderNode = item => (
    <props.NodeComponent
      {...props}
      isRootLevel={isRootLevel}
      key={item}
      id={item}
      className={props.styles.node}
    />
  )

  const renderItem = (index, key) => {
    var item = nodes.get(index)
    return renderNode(item)
  }

  return (
    // TODO: check solution for inner lazy render lists

    <div className={styles.nodeList}>
      {isRootLevel
        ? <div className={styles.reactListWrap}>
          <ReactList
            itemRenderer={renderItem}
            length={nodes.size}
            type={props.listType || 'variable'} /></div>
        : nodes.map(renderNode)
      }
    </div>
  )
}

Nodes.propTypes = {
  nodes: PropTypes.oneOfType([
    PropTypes.array,
    ImmutablePropTypes.list
  ]),
  isRootLevel: PropTypes.bool.isRequired,
  styles: PropTypes.object.isRequired
}

const ChildrenNode = ({
  NodeLabelComponent = NodeLabel,
  Icon = ChildrenIcon,
  isRootLevel,
  isLeaf,
  onToggle,
  isCollapsed,
  ...props
}) => {
  const nodeLabelClass = cx(
    styles.nodeLabelWrap,
    {
      [styles.rootNodeLabel]: isRootLevel
    }
  )
  return (
    <div className={nodeLabelClass}>
      <NodeLabelComponent isRootLevel={isRootLevel} {...props} />
      <Icon
        isRootLevel={isRootLevel}
        isLeaf={isLeaf}
        {...props}
        onClick={onToggle}
        isCollapsed={isCollapsed} />
    </div>
  )
}

export const Node = composeNode(ChildrenNode)
