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

            builder.HasKey(x => x.ID);
            
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();


            builder.HasOne(x => x.ShiftAssignment)
                .WithOne(t => t.Attendance)
                     .HasForeignKey<Attendance>(x => x.ShiftAssignmentID);
        }
    }
}
