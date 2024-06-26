using backend.Models;

namespace backend;

public interface IFMPService
{
    Task<Stock> FindStockBySymbolAsync(string symbol);
}
