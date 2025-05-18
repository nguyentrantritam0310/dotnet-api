using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ReportDate)
                .IsRequired(); 

            builder.Property(x => x.ReportType)
                .HasMaxLength(100);

            builder.Property(x => x.Content)
                .HasMaxLength(1000); 

            builder.Property(x => x.Level)
                .HasMaxLength(50); 

            builder.HasOne(x => x.Employee)
                .WithMany(e => e.Reports)
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(x => x.Construction)
                .WithMany(c => c.Reports)
                .HasForeignKey(x => x.ConstructionID)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
