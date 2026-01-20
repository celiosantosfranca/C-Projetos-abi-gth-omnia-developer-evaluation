using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public record SaleListQuery(
    int Page,
    int PageSize,
    Guid? CustomerId,
    Guid? BranchId,
    bool? IsCancelled,
    DateTime? MinDate,
    DateTime? MaxDate,
    string? OrderBy
);

public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);    
    Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListAsync(SaleListQuery query, CancellationToken cancellationToken = default);
}
