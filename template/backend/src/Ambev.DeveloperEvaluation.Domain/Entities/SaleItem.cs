using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid SaleId { get; set; }
    public Sale? Sale { get; set; }

    public Guid ProductId { get; set; }
    public string ProductDescription { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal DiscountAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public bool IsCancelled { get; set; }

    public void Recalculate()
    {
        if (Quantity <= 0) throw new DomainException("Quantity must be greater than zero");
        if (Quantity > 20) throw new DomainException("It is not possible to sell more than 20 identical items");
        if (UnitPrice < 0) throw new DomainException("UnitPrice cannot be negative");

        DiscountRate = GetDiscountRate(Quantity);

        var gross = Quantity * UnitPrice;
        DiscountAmount = decimal.Round(gross * DiscountRate, 2, MidpointRounding.AwayFromZero);
        TotalAmount = decimal.Round(gross - DiscountAmount, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal GetDiscountRate(int quantity)
    {
        if (quantity < 4) return 0m;
        if (quantity <= 9) return 0.10m;
        return 0.20m; // 10..20
    }
}
