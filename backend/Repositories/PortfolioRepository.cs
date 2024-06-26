using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext context;

    public PortfolioRepository(ApplicationDBContext context)
    {
        this.context = context;
    }

    public async Task<Portfolio> CreateAsync(Portfolio portfolio)
    {
        await this.context.Portfolios.AddAsync(portfolio);
        await this.context.SaveChangesAsync();

        return portfolio;
    }

    public async Task<Portfolio?> DeleteAsync(AppUser user, string symbol)
    {
        var portfolio = await this.context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolio == null) return null;

        this.context.Portfolios.Remove(portfolio);
        await this.context.SaveChangesAsync();

        return portfolio;
    }

    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await this.context.Portfolios
            .Where(p => p.AppUserId == user.Id)
            .Select(p => p.Stock).ToListAsync();
    }
}
