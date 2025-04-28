using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class MaterialPlanConfiguration : IEntityTypeConfiguration<MaterialPlan>
    {
        public void Configure(EntityTypeBuilder<MaterialPlan> builder)
        {
            builder.ToTable("MaterialPlans");

            builder.HasKey(x => new { x.ImportOrderID, x.MaterialID, x.ConstructionPlanID });

            builder.Property(x => x.ImportQuantity)
                .IsRequired(); 

            builder.Property(x => x.Status)
                .IsRequired(); 

            builder.HasOne(x => x.Material)
                .WithMany(m => m.MaterialPlans)
                .HasForeignKey(x => x.MaterialID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ImportOrder)
                .WithMany(io => io.MaterialPlans)
                .HasForeignKey(x => x.ImportOrderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ConstructionPlan)
                .WithMany(cp => cp.MaterialPlans)
                .HasForeignKey(x => x.ConstructionPlanID)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
