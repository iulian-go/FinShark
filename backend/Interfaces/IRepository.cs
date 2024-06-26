namespace backend;

public interface IRepository<TModel>
{
    Task<List<TModel>> GetAllAsync();
    
    Task<TModel?> GetByIdAsync(int id);

    Task<TModel> CreateAsync(TModel model);

    Task<TModel?> UpdateAsync(int id, TModel model);

    Task<TModel?> DeleteAsync(int id);
}
