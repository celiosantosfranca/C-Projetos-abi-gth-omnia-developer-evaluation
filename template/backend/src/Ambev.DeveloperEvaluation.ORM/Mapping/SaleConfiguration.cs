using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SaleNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.SaleDate)
            .IsRequired();

        builder.Property(x => x.CustomerDescription)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.BranchDescription)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsCancelled)
            .IsRequired();

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Sale!)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.SaleNumber);
        builder.HasIndex(x => x.SaleDate);
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.BranchId);
    }
}
