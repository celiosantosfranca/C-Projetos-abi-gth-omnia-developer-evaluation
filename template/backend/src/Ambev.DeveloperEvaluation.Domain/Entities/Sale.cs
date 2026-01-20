using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }

    public Guid CustomerId { get; set; }
    public string CustomerDescription { get; set; } = string.Empty;

    public Guid BranchId { get; set; }
    public string BranchDescription { get; set; } = string.Empty;

    public bool IsCancelled { get; set; }
    public DateTime? CancelledAt { get; set; }

    public decimal TotalAmount { get; set; }

    public List<SaleItem> Items { get; set; } = new();

    public Sale()
    {
        SaleDate = DateTime.UtcNow;
    }

    public void SetItems(IEnumerable<SaleItem> items)
    {
        Items = items?.ToList() ?? new List<SaleItem>();
        foreach (var it in Items)
        {
            it.SaleId = Id;
            it.Recalculate();
        }
        RecalculateTotals();
    }

    public void AddItem(SaleItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        item.SaleId = Id;
        item.Recalculate();
        Items.Add(item);
        RecalculateTotals();
    }

    public void RecalculateTotals()
    {
        TotalAmount = Items.Sum(i => i.TotalAmount);
    }

    public void Cancel()
    {
        IsCancelled = true;
        CancelledAt = DateTime.UtcNow;
    }
}
