using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendances");

            builder.HasKey(x => new { x.EmployeeID, x.WorkShiftID, x.AttendanceDate });

            builder.Property(x => x.AttendanceDate)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .HasMaxLength(100); 
        
            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.Attendances)
                   .HasForeignKey(x => x.EmployeeID);
            
            builder.HasOne(x => x.WorkShift)
                   .WithMany(t => t.Attendances)
                   .HasForeignKey(x => x.WorkShiftID);
        }
    }
}

