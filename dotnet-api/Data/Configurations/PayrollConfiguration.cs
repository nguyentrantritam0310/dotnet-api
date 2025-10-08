using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.HasKey(x => x.ID);
            
            builder.HasOne(x => x.Employee)
                .WithMany(u => u.Payrolls)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
