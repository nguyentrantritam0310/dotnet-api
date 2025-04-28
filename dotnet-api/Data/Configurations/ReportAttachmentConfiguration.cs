using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ReportAttachmentConfiguration : IEntityTypeConfiguration<ReportAttachment>
    {
        public void Configure(EntityTypeBuilder<ReportAttachment> builder)
        {
            builder.ToTable("ReportAttachments");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.FilePath)
                .IsRequired() 
                .HasMaxLength(500); 

            builder.Property(x => x.UploadDate)
                .IsRequired(); 

            builder.HasOne(x => x.Report)
                .WithMany(r => r.ReportAttachments)
                .HasForeignKey(x => x.ReportID)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
