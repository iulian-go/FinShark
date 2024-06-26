using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace backend;

public class CommentRepository : IRepository<Comment>, ICommentRepository
{
    private readonly ApplicationDBContext context;

    public CommentRepository(ApplicationDBContext context)
    {
        this.context = context;
    }

    public async Task<Comment> CreateAsync(Comment model)
    {
        await this.context.Comments.AddAsync(model);
        await this.context.SaveChangesAsync();

        return model;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await this.GetByIdAsync(id);

        if (comment == null) return null;

        this.context.Remove(comment);
        await this.context.SaveChangesAsync();

        return comment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await this.context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public async Task<List<Comment>> GetAllAsync(CommentQuery query)
    {
        var comments = this.context.Comments.Include(a => a.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Symbol))
            comments = comments.Where(c => c.Stock.Symbol == query.Symbol);

        if (query.IsDescending)
            comments = comments.OrderByDescending(c => c.CreateOn);

        return await comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await this.context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Comment?> UpdateAsync(int id, Comment model)
    {
        var existingComment = await this.GetByIdAsync(id);

        if (existingComment == null) return null;

        existingComment.Title = model.Title;
        existingComment.Content = model.Content;

        await this.context.SaveChangesAsync();

        return existingComment;
    }
}
