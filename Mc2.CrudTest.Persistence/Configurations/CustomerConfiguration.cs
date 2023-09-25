using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(50).IsRequired();
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(50).IsRequired();
        builder.Property(p => p.PhoneNumber).HasMaxLength(11).IsRequired();
        builder.Property(p => p.BankAccountNumber).HasMaxLength(20).IsRequired();
        builder.Property(p => p.IsDeleted).IsRequired();

        builder.HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth }).IsUnique(true);
        builder.HasIndex(c => c.Email).IsUnique(true);
    }
}
