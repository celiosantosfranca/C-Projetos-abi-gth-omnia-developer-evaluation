namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
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

    public List<CreateSaleItemResponse> Items { get; set; } = new();
}

public class CreateSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
