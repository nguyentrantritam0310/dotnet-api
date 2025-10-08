using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkShiftConfiguration : IEntityTypeConfiguration<WorkShift>
    {
        public void Configure(EntityTypeBuilder<WorkShift> builder)
        {
            builder.ToTable("WorkShifts");

            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID)
       .ValueGeneratedOnAdd();

            builder.Property(x => x.ShiftName)
                   .IsRequired();


            builder.HasMany(x => x.ShiftAssignments)
                           .WithOne(e => e.WorkShift)
                                       .HasForeignKey(x => x.WorkShiftID);

            builder.HasMany(x => x.ShiftDetails)
                    .WithOne(e => e.WorkShift)
                    .HasForeignKey(x => x.WorkShiftID);


        }
    }
}

