using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IRepository<Stock> repository;
        private readonly IStockRepository stockRepository;

        public StockController(IRepository<Stock> repository, IStockRepository stockRepository)
        {
            this.repository = repository;
            this.stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] StockQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stocks = await this.stockRepository.GetAllAsync(query);
            var stockDTOs = stocks.Select(s => s.ToStockDTO());

            return Ok(stockDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await this.repository.GetByIdAsync(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO stockDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = stockDTO.ToStock();

            await this.repository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDTO stockDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = stockDTO.ToStock();
            stockModel = await this.repository.UpdateAsync(id, stockModel);

            if (stockModel == null) return NotFound();

            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = await this.repository.DeleteAsync(id);

            if (stockModel == null) return NotFound();

            return NoContent();
        }
    }
}