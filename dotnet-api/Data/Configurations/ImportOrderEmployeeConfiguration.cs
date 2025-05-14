using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ImportOrderEmployeeConfiguration : IEntityTypeConfiguration<ImportOrderEmployee>
    {
        public void Configure(EntityTypeBuilder<ImportOrderEmployee> builder)
        {
            builder.ToTable("ImportOrderEmployees");
            // Composite primary key
            builder.HasKey(ioe => new { ioe.ImportOrderId, ioe.EmployeeID });

            // Relationship: ImportOrderEmployee -> ImportOrder (many-to-one)
            builder.HasOne(ioe => ioe.ImportOrder)
                .WithMany(io => io.ImportOrderEmployees)
                .HasForeignKey(ioe => ioe.ImportOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: ImportOrderEmployee -> ApplicationUser (many-to-one)
            builder.HasOne(ioe => ioe.Employee)
                .WithMany(e => e.ImportOrderEmployees)
                .HasForeignKey(ioe => ioe.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            // Enum property mapping (optional, handled by EF Core by default)
            builder.Property(ioe => ioe.Role)
                .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
