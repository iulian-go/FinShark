using System.Runtime.CompilerServices;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IRepository<Comment> repository;
    private readonly IRepository<Stock> stockRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IStockRepository customStockRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IFMPService fmpService;

    public CommentController(
        IRepository<Comment> repository,
        IRepository<Stock> stockRepository,
        ICommentRepository commentRepository,
        IStockRepository customStockRepository,
        UserManager<AppUser> userManager,
        IFMPService fmpService)
    {
        this.repository = repository;
        this.stockRepository = stockRepository;
        this.commentRepository = commentRepository;
        this.customStockRepository = customStockRepository;
        this.userManager = userManager;
        this.fmpService = fmpService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] CommentQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var comments = await this.commentRepository.GetAllAsync(query);
        var commentDTOs = comments.Select(c => c.ToCommentDTO());

        return Ok(commentDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var comment = await this.repository.GetByIdAsync(id);

        if (comment == null) return NotFound();

        return Ok(comment.ToCommentDTO());
    }

    [HttpPost("{symbol}")]
    public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDTO commentDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var stock = await this.customStockRepository.GetBySymbolAsync(symbol);
        if (stock == null)
        {
            stock = await this.fmpService.FindStockBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock does not exist");

            await this.stockRepository.CreateAsync(stock);
        }

        var username = this.User.GetUsername();
        var appUser = await this.userManager.FindByNameAsync(username);

        var comment = commentDTO.ToComment(stock.Id);
        comment.AppUserId = appUser.Id;
        await this.repository.CreateAsync(comment);

        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDTO commentDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var comment = commentDTO.ToComment();
        comment = await this.repository.UpdateAsync(id, comment);

        if (comment == null) return NotFound("Comment not found");

        return Ok(comment.ToCommentDTO());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var comment = await this.repository.DeleteAsync(id);

        if (comment == null) return NotFound();

        return NoContent();
    }
}
