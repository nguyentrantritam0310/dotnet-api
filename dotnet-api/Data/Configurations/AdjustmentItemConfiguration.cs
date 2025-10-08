using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class AdjustmentItemConfiguration : IEntityTypeConfiguration<AdjustmentItem>
    {
        public void Configure(EntityTypeBuilder<AdjustmentItem> builder)
        {
            builder.ToTable("AdjustmentItems");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();
            builder.HasOne(x => x.adjustmentType)
       .WithMany(y => y.AdjustmentItems)
       .HasForeignKey(x => x.AdjustmentTypeID)
       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}