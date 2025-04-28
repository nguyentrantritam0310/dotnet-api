using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkAttributeConfiguration : IEntityTypeConfiguration<WorkAttribute>
    {
        public void Configure(EntityTypeBuilder<WorkAttribute> builder)
        {
            builder.ToTable("WorkAttributes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

        builder.Property(x => x.WorkAttributeName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.UnitOfMeasurement)
                .WithMany(r => r.WorkAttributes)
                .HasForeignKey(x => x.UnitOfMeasurementID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
