using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class Contract_AllowanceConfiguration : IEntityTypeConfiguration<Contract_Allowance>
    {
        public void Configure(EntityTypeBuilder<Contract_Allowance> builder)
        {
            builder.HasKey(x => new { x.AllowanceID , x.ContractID });
            builder.Property(x => x.Value).HasColumnType("decimal(18,2)");
            builder.HasOne(x => x.Contract)
                .WithMany(c => c.ContractAllowances)
                .HasForeignKey(x => x.ContractID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Allowance)
                .WithMany()
                .HasForeignKey(x => x.AllowanceID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
