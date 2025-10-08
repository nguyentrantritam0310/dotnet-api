using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ApplicationUser_PayrollAdjustmentConfiguration : IEntityTypeConfiguration<ApplicationUser_PayrollAdjustment>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser_PayrollAdjustment> builder)
        {
            builder.ToTable("ApplicationUser_PayrollAdjustments");

            builder.HasKey(x => new { x.PayrollAdjustmentID, x.EmployeeID });

            builder.HasOne(x => x.applicationUser)
                   .WithMany(e => e.applicationUser_PayrollAdjustment)
                   .HasForeignKey(x => x.EmployeeID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.PayrollAdjustment)
                   .WithMany(t => t.applicationUser_PayrollAdjustment)
                   .HasForeignKey(x => x.PayrollAdjustmentID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
