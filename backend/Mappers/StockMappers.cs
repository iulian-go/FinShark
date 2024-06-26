using backend.Models;

namespace backend;

public static class StockMappers
{
    public static StockDTO ToStockDTO(this Stock stock)
    {
        return new StockDTO
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDividend = stock.LastDividend,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Comments = stock.Comments.Select(c => c.ToCommentDTO()).ToList()
        };
    }

    public static Stock ToStock(this CreateStockDTO stockDTO)
    {
        return new Stock
        {
            Symbol = stockDTO.Symbol,
            CompanyName = stockDTO.CompanyName,
            Industry = stockDTO.Industry,
            Purchase = stockDTO.Purchase,
            LastDividend = stockDTO.LastDividend,
            MarketCap = stockDTO.MarketCap
        };
    }

    public static Stock ToStock(this UpdateStockDTO stockDTO)
    {
        return new Stock
        {
            Symbol = stockDTO.Symbol,
            CompanyName = stockDTO.CompanyName,
            Industry = stockDTO.Industry,
            Purchase = stockDTO.Purchase,
            LastDividend = stockDTO.LastDividend,
            MarketCap = stockDTO.MarketCap
        };
    }

    public static Stock ToStock(this FMPStock fmpStock)
    {
        return new Stock
        {
            Symbol = fmpStock.symbol,
            CompanyName = fmpStock.companyName,
            Industry = fmpStock.industry,
            Purchase = fmpStock.price,
            LastDividend = fmpStock.lastDiv,
            MarketCap = fmpStock.mktCap
        };
    }
}
