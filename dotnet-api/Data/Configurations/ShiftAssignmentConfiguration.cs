using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ShiftAssignmentConfiguration : IEntityTypeConfiguration<ShiftAssignment>
    {
        public void Configure(EntityTypeBuilder<ShiftAssignment> builder)
        {
            builder.ToTable("ShiftAssignments");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkDate)
                   .IsRequired();
        
            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.ShiftAssignments)
                   .HasForeignKey(x => x.EmployeeID);
            
            builder.HasOne(x => x.WorkShift)
                   .WithMany(t => t.ShiftAssignments)
                   .HasForeignKey(x => x.WorkShiftID);

            builder.HasOne(x => x.ConstructionTask)
                   .WithMany(ct => ct.Attendances)
                   .HasForeignKey(x => x.ConstructionTaskID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

