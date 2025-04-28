using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ExportOrderConfiguration : IEntityTypeConfiguration<ExportOrder>
    {
        public void Configure(EntityTypeBuilder<ExportOrder> builder)
        {
            builder.ToTable("ExportOrders");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ExportDate)
                .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithMany(e => e.ExportOrders) 
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(x => x.ConstructionPlan)
                .WithMany(c => c.ExportOrders) 
                .HasForeignKey(x => x.ConstructionPlanID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
