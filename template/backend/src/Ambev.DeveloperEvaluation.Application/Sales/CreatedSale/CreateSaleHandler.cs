using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = request.SaleNumber,
            SaleDate = request.SaleDate,
            CustomerId = request.CustomerId,
            CustomerDescription = request.CustomerDescription,
            BranchId = request.BranchId,
            BranchDescription = request.BranchDescription,
            IsCancelled = false
        };

        foreach (var it in request.Items)
        {
            sale.AddItem(new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = it.ProductId,
                ProductDescription = it.ProductDescription,
                Quantity = it.Quantity,
                UnitPrice = it.UnitPrice
            });
        }

        await _repository.CreateAsync(sale, cancellationToken);

        return _mapper.Map<CreateSaleResult>(sale);
    }
}
