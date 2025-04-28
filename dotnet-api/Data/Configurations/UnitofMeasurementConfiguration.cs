using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class UnitofMeasurementConfiguration : IEntityTypeConfiguration<UnitofMeasurement>
    {
        public void Configure(EntityTypeBuilder<UnitofMeasurement> builder)
        {
            builder.ToTable("UnitofMeasurements");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.UnitName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ShortName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Category)
                .HasMaxLength(50);
        }
    }
}
