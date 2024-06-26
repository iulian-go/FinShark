using backend.Models;

namespace backend;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(StockQuery query);

    Task<Stock?> GetBySymbolAsync(string symbol);
}
