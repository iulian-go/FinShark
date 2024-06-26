import React from 'react'
import { CommentGet } from '../../Models/Comment'
import StockCommentListItem from './StockCommentListItem';

type Props = {
    comments: CommentGet[];
}

const StockCommentList = (props: Props) => {
  return (
    <>
    {props.comments ? props.comments.map((comment, index) => {
        return <StockCommentListItem key={index} comment={comment} />
    }) : ""}
    </>
  )
}

export default StockCommentList