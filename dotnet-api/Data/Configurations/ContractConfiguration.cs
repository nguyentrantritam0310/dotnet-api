using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID)
                               .ValueGeneratedOnAdd();
            builder.HasOne(x => x.Employee)
                .WithMany(u => u.Contracts)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ContractType)
                .WithMany()
                .HasForeignKey(x => x.ContractTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ContractFormEntity)
                .WithMany()
                .HasForeignKey(x => x.ContractFormID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
