using backend.Models;

namespace backend;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync(CommentQuery query);
}
