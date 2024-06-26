using System.ComponentModel.DataAnnotations;

namespace backend;

public class CreateStockDTO
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 chars")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MaxLength(100, ErrorMessage = "Company name cannot be over 100 chars")]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [Range(1, 1000000000000)]
    public decimal Purchase { get; set; }

    [Required]
    [Range(0.001, 100)]
    public decimal LastDividend { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Industry name cannot be over 100 chars")]
    public string Industry { get; set; } = string.Empty;

    [Required]
    [Range(1, 5000000000000)]
    public long MarketCap { get; set; }
}
