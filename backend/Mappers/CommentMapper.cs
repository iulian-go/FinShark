using backend.Models;

namespace backend;

public static class CommentMapper
{
    public static CommentDTO ToCommentDTO(this Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreateOn = comment.CreateOn,
            CreatedBy = comment.AppUser.UserName,
            StockId = comment.StockId
        };
    }

    public static Comment ToComment(this CreateCommentDTO commentDTO, int stockId)
    {
        return new Comment
        {
            Title = commentDTO.Title,
            Content = commentDTO.Content,
            StockId = stockId
        };
    }

    public static Comment ToComment(this UpdateCommentDTO commentDTO)
    {
        return new Comment
        {
            Title = commentDTO.Title,
            Content = commentDTO.Content
        };
    }
}
