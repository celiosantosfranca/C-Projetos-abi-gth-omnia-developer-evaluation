using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (sale is null) return false;

        sale.IsCancelled = true;
        sale.CancelledAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<(IReadOnlyList<Sale> Items, int TotalCount)> ListAsync(SaleListQuery query, CancellationToken cancellationToken = default)
    {
        var q = _context.Sales.AsNoTracking().AsQueryable();

        if (query.CustomerId.HasValue)
            q = q.Where(x => x.CustomerId == query.CustomerId.Value);

        if (query.BranchId.HasValue)
            q = q.Where(x => x.BranchId == query.BranchId.Value);

        if (query.IsCancelled.HasValue)
            q = q.Where(x => x.IsCancelled == query.IsCancelled.Value);

        if (query.MinDate.HasValue)
            q = q.Where(x => x.SaleDate >= query.MinDate.Value);

        if (query.MaxDate.HasValue)
            q = q.Where(x => x.SaleDate <= query.MaxDate.Value);

        q = ApplyOrderBy(q, query.OrderBy);

        var total = await q.CountAsync(cancellationToken);
        var items = await q
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return (items, total);
    }

    private static IQueryable<Sale> ApplyOrderBy(IQueryable<Sale> q, string? orderBy)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return q.OrderByDescending(x => x.SaleDate);

        var parts = orderBy.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var field = parts[0].ToLowerInvariant();
        var desc = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

        return (field, desc) switch
        {
            ("date", false) => q.OrderBy(x => x.SaleDate),
            ("date", true) => q.OrderByDescending(x => x.SaleDate),
            ("salenumber", false) => q.OrderBy(x => x.SaleNumber),
            ("salenumber", true) => q.OrderByDescending(x => x.SaleNumber),
            ("total", false) => q.OrderBy(x => x.TotalAmount),
            ("total", true) => q.OrderByDescending(x => x.TotalAmount),
            _ => q.OrderByDescending(x => x.SaleDate)
        };
    }
}
