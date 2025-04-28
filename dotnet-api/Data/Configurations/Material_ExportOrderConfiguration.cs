using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class MaterialExportOrderConfiguration : IEntityTypeConfiguration<Material_ExportOrder>
    {
        public void Configure(EntityTypeBuilder<Material_ExportOrder> builder)
        {
            builder.ToTable("Material_ExportOrders");

            builder.HasKey(x => new { x.ExportOrderID, x.MaterialID });

            builder.Property(x => x.Quantity)
                .IsRequired(); 

            builder.HasOne(x => x.ExportOrder)
                .WithMany(eo => eo.Material_ExportOrder)
                .HasForeignKey(x => x.ExportOrderID)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x => x.Material)
                .WithMany(m => m.Material_ExportOrders)
                .HasForeignKey(x => x.MaterialID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
