using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class PayrollAdjustmentConfiguration : IEntityTypeConfiguration<PayrollAdjustment>
    {
        public void Configure(EntityTypeBuilder<PayrollAdjustment> builder)
        {
            builder.ToTable("PayrollAdjustments");

            builder.HasKey(x => x.voucherNo);

            // Configure decisionDate to store datetime with default time 00:00:00
            builder.Property(x => x.decisionDate)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.AdjustmentType)
                   .WithMany(y => y.PayrollAdjustments)
                   .HasForeignKey(x => x.AdjustmentTypeID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure optional AdjustmentItem relationship
            builder.HasOne(x => x.AdjustmentItem)
                   .WithMany()
                   .HasForeignKey(x => x.AdjustmentItemID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
        }
    }
} 