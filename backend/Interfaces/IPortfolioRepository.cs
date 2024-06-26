﻿using backend.Models;

namespace backend;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);

    Task<Portfolio> CreateAsync(Portfolio portfolio);

    Task<Portfolio?> DeleteAsync(AppUser user, string symbol);
}
