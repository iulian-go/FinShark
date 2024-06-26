using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class StockRepository : IRepository<Stock>, IStockRepository
{
    private readonly ApplicationDBContext context;

    public StockRepository(ApplicationDBContext context)
    {
        this.context = context;
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await this.context.Stocks.AddAsync(stock);
        await this.context.SaveChangesAsync();

        return stock;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await this.GetByIdAsync(id);

        if (stock == null) return null;

        this.context.Remove(stock);
        await this.context.SaveChangesAsync();

        return stock;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await this.context.Stocks.Include(s => s.Comments).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await this.context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, Stock model)
    {
        var existingStock = await this.GetByIdAsync(id);

        if (existingStock == null) return null;

        existingStock.Symbol = model.Symbol;
        existingStock.CompanyName = model.CompanyName;
        existingStock.Industry = model.Industry;
        existingStock.Purchase = model.Purchase;
        existingStock.LastDividend = model.LastDividend;
        existingStock.MarketCap = model.MarketCap;

        await this.context.SaveChangesAsync();

        return existingStock;
    }

    public async Task<List<Stock>> GetAllAsync(StockQuery query)
    {
        var stocks = this.context.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Symbol))
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

        if (!string.IsNullOrWhiteSpace(query.SortBy))
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);

        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        stocks = stocks.Skip(skipNumber).Take(query.PageSize);

        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
        return await this.context.Stocks.FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
    }
}
