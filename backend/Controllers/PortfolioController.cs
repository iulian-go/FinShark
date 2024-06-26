using System.Xml.Schema;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly IRepository<Stock> repository;
    private readonly IStockRepository stockRepository;
    private readonly IPortfolioRepository portfolioRepository;
    private readonly IFMPService fmpService;

    public PortfolioController(
        UserManager<AppUser> userManager,
        IRepository<Stock> repository,
        IStockRepository stockRepository,
        IPortfolioRepository portfolioRepository,
        IFMPService fmpService)
    {
        this.userManager = userManager;
        this.repository = repository;
        this.stockRepository = stockRepository;
        this.portfolioRepository = portfolioRepository;
        this.fmpService = fmpService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = this.User.GetUsername();
        var appUser = await this.userManager.FindByNameAsync(username);
        var userPortfolio = await this.portfolioRepository.GetUserPortfolio(appUser);

        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var username = this.User.GetUsername();
        var appUser = await this.userManager.FindByNameAsync(username);

        var stock = await this.stockRepository.GetBySymbolAsync(symbol);
        if (stock == null)
        {
            stock = await this.fmpService.FindStockBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock does not exist");

            await this.repository.CreateAsync(stock);
        }

        var userPortfolio = await this.portfolioRepository.GetUserPortfolio(appUser);
        if (userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower())) return StatusCode(500, "Cannot add same stock twice");

        var portfolio = new Portfolio { AppUserId = appUser.Id, StockId = stock.Id };
        portfolio = await this.portfolioRepository.CreateAsync(portfolio);

        if (portfolio == null) return StatusCode(500, "Could not create portfolio");

        return Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = this.User.GetUsername();
        var appUser = await this.userManager.FindByNameAsync(username);

        var userPortfolio = await this.portfolioRepository.GetUserPortfolio(appUser);
        var filteredStock = userPortfolio.Find(s => s.Symbol.ToLower() == symbol.ToLower());

        if(filteredStock == null) return StatusCode(500, "Could not find portfolio");

        await this.portfolioRepository.DeleteAsync(appUser, symbol);

        return Ok();
    }
}
