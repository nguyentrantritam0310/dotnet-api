using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class TimeSheetFeedbackConfiguration : IEntityTypeConfiguration<TimeSheetFeedback>
    {
        public void Configure(EntityTypeBuilder<TimeSheetFeedback> builder)
        {
            builder.HasKey(x => new { x.TimeSheetID , x.EmployeeID });
            builder.HasOne(x => x.TimeSheet)
                .WithMany()
                .HasForeignKey(x => x.TimeSheetID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Employee)
                .WithMany(u => u.TimeSheetFeedbacks)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
