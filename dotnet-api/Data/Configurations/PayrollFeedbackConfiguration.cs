using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class PayrollFeedbackConfiguration : IEntityTypeConfiguration<PayrollFeedback>
    {
        public void Configure(EntityTypeBuilder<PayrollFeedback> builder)
        {
            builder.HasKey(x => new { x.PayrollID , x.EmployeeID });

            builder.HasOne(x => x.Payroll)
                .WithMany()
                .HasForeignKey(x => x.PayrollID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Employee)
                .WithMany(u => u.PayrollFeedbacks)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
