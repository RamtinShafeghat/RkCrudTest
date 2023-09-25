using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Persistence.Configurations;

public class EventDataConfiguration : IEntityTypeConfiguration<EventData>
{
    public void Configure(EntityTypeBuilder<EventData> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Data)
               .IsRequired();
    }
}
