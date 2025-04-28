using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ReportStatusLogConfiguration : IEntityTypeConfiguration<ReportStatusLog>
    {
        public void Configure(EntityTypeBuilder<ReportStatusLog> builder)
        {
            builder.ToTable("ReportStatusLogs");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Status)
                .IsRequired() 
                .HasMaxLength(100); 

            builder.Property(x => x.Note)
                .HasMaxLength(500); 

            builder.Property(x => x.ReportDate)
                .IsRequired(); 

            builder.HasOne(x => x.Report)
                .WithMany(r => r.ReportStatusLogs)
                .HasForeignKey(x => x.ReportID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
