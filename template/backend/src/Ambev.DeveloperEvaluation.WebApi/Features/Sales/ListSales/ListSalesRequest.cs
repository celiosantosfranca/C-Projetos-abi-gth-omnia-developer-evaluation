namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public Guid? CustomerId { get; set; }
    public Guid? BranchId { get; set; }
    public bool? IsCancelled { get; set; }
    public DateTime? MinDate { get; set; }
    public DateTime? MaxDate { get; set; }

    public string? OrderBy { get; set; }
}
