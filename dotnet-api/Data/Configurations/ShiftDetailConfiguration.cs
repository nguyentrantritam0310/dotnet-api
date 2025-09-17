using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ShiftDetailConfiguration : IEntityTypeConfiguration<ShiftDetail>
    {
        public void Configure(EntityTypeBuilder<ShiftDetail> builder)
        {
            builder.ToTable("ShiftDetails");

            builder.HasKey(x => x.ID);
            
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.DayOfWeek)
                   .IsRequired();

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                   .IsRequired();

            builder.Property(x => x.BreakStart)
                   .IsRequired();

            builder.Property(x => x.BreakEnd)
                   .IsRequired();

            builder.HasOne(x => x.WorkShift)
                   .WithMany(y=>y.ShiftDetails) 
                   .HasForeignKey(x => x.WorkShiftID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
