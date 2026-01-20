namespace Ambev.DeveloperEvaluation.Application.Sales.Models;

public class SaleDto
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }

    public Guid CustomerId { get; set; }
    public string CustomerDescription { get; set; } = string.Empty;

    public Guid BranchId { get; set; }
    public string BranchDescription { get; set; } = string.Empty;

    public bool IsCancelled { get; set; }
    public DateTime? CancelledAt { get; set; }

    public decimal TotalAmount { get; set; }

    public List<SaleItemDto> Items { get; set; } = new();
}
