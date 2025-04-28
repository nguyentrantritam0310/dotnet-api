using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ImportOrderConfiguration : IEntityTypeConfiguration<ImportOrder>
    {
        public void Configure(EntityTypeBuilder<ImportOrder> builder)
        {
            builder.ToTable("ImportOrders");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ImportDate)
                .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithMany(e => e.ImportOrders) 
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
