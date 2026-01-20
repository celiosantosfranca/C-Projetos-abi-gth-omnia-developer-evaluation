using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Theory]
    [InlineData(1, 100, 0.00)]
    [InlineData(3, 100, 0.00)]
    [InlineData(4, 100, 0.10)]
    [InlineData(9, 100, 0.10)]
    [InlineData(10, 100, 0.20)]
    [InlineData(20, 100, 0.20)]
    public void Recalculate_ShouldApplyDiscountRules(int quantity, decimal unitPrice, decimal expectedRate)
    {
        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductDescription = "Test",
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        item.Recalculate();

        Assert.Equal(expectedRate, item.DiscountRate);
        var gross = quantity * unitPrice;
        Assert.Equal(decimal.Round(gross * expectedRate, 2, MidpointRounding.AwayFromZero), item.DiscountAmount);
        Assert.Equal(decimal.Round(gross - item.DiscountAmount, 2, MidpointRounding.AwayFromZero), item.TotalAmount);
    }

    [Fact]
    public void Recalculate_QuantityGreaterThan20_ShouldThrow()
    {
        var item = new SaleItem
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            ProductDescription = "Test",
            Quantity = 21,
            UnitPrice = 10
        };

        Assert.Throws<DomainException>(() => item.Recalculate());
    }
}
