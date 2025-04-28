using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.MaterialName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Specification)
                .HasMaxLength(255);

            builder.Property(x => x.Note)
                .HasMaxLength(255);

            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
                .HasMaxLength(50);

            builder.Property(x => x.Specification)
                .HasMaxLength(200);

            builder.Property(x => x.Note)
                .HasMaxLength(50);

            builder.HasOne(x => x.UnitOfMeasurement)
                .WithMany(u => u.Materials) 
                .HasForeignKey(x => x.UnitOfMeasurementID)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(x => x.MaterialType)
                .WithMany(mt => mt.Materials) 
                .HasForeignKey(x => x.MaterialTypeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
