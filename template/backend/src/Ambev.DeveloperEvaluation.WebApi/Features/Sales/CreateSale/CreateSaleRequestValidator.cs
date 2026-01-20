using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.SaleNumber).NotEmpty().MaximumLength(50);
        RuleFor(x => x.SaleDate).NotEmpty();

        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.CustomerDescription).NotEmpty().MaximumLength(200);

        RuleFor(x => x.BranchId).NotEmpty();
        RuleFor(x => x.BranchDescription).NotEmpty().MaximumLength(200);

        RuleFor(x => x.Items).NotNull().NotEmpty();
        RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
    }
}

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.ProductDescription).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
