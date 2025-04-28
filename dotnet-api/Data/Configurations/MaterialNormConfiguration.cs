using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class MaterialNormConfiguration : IEntityTypeConfiguration<MaterialNorm>
    {
        public void Configure(EntityTypeBuilder<MaterialNorm> builder)
        {
            builder.ToTable("MaterialNorms");

            builder.HasKey(x => new { x.MaterialID, x.WorkSubTypeVariantID });

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne(x => x.Material)
                .WithMany(m => m.MaterialNorms)
                .HasForeignKey(x => x.MaterialID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WorkSubTypeVariant)
                .WithMany(ci => ci.MaterialNorms)
                .HasForeignKey(x => x.WorkSubTypeVariantID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
