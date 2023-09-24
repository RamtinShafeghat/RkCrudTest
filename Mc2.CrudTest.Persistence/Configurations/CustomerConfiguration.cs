﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.Core.CustomerAggregate;

namespace Mc2.CrudTest.Persistence.Configurations;

public class ExemptVehicleConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
               .IsRequired();


        builder.Property(p => p.LastName)
               .IsRequired();
    }
}