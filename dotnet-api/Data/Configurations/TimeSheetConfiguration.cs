using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class TimeSheetConfiguration : IEntityTypeConfiguration<TimeSheet>
    {
        public void Configure(EntityTypeBuilder<TimeSheet> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Employee)
                .WithMany(u => u.TimeSheets)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Payroll)
                .WithOne()
                .HasForeignKey<Payroll>(p => p.ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
