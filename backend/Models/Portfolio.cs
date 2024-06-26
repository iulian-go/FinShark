using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace backend;

[Table("Portfolios")]
public class Portfolio
{
    public string AppUserId { get; set; } = string.Empty;

    public int StockId { get; set; }

    public AppUser AppUser { get; set; } = null!;

    public Stock Stock { get; set; } = null!;
}
