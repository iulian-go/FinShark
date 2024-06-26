import React, { useCallback, useEffect, useState } from 'react'
import CommentForm from './CommentForm'
import { commentPostAPI, commentsGetAPI } from '../../Services/CommentService';
import { toast } from 'react-toastify';
import { CommentGet } from '../../Models/Comment';
import Spinner from '../Spinner/Spinner';
import StockCommentList from '../CommentList/StockCommentList';

interface Props {
    stockSymbol: string
}

type CommentFormInputs = {
    title: string;
    content: string;
};

const StockComment = (props: Props) => {
    const [comments, setComments] = useState<CommentGet[] | null>(null);
    const [loading, setLoading] = useState<boolean>();

    const handleComment = (e: CommentFormInputs) => {
        commentPostAPI(e.title, e.content, props.stockSymbol)
            .then((res) => {
                if (res) {
                    toast.success("Comment created successfully!");
                    getComments();
                }
            }).catch(e => {
                toast.warning(e);
            })
    };

    const getComments = useCallback(() => {
        setLoading(true);
        commentsGetAPI(props.stockSymbol)
            .then((res) => {
                setLoading(false);
                setComments(res?.data!);
            })
    }, [props.stockSymbol]);

    useEffect(() => {
        getComments();
    }, [getComments]);

    return (
        <div className='flex flex-col w-2/4'>
            {loading ? <Spinner /> : <StockCommentList comments={comments!} />}
            <CommentForm symbol={props.stockSymbol} handleComment={handleComment} />
        </div>
    )
}

export default StockComment