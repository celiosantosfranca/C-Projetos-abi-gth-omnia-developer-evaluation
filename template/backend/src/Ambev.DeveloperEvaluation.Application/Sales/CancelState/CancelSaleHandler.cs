using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand>
{
    private readonly ISaleRepository _repository;

    public CancelSaleHandler(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var ok = await _repository.CancelAsync(request.Id, cancellationToken);
        if (!ok)
            throw new KeyNotFoundException($"Sale {request.Id} not found");
    }
}
