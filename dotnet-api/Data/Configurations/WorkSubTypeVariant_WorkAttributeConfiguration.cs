using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkSubTypeVariant_WorkAttributeConfiguration : IEntityTypeConfiguration<WorkSubTypeVariant_WorkAttribute>
    {
        public void Configure(EntityTypeBuilder<WorkSubTypeVariant_WorkAttribute> builder)
        {
            builder.ToTable("WorkSubTypeVariant_WorkAttributes");

            builder.HasKey(x => new { x.WorkSubTypeVariantID, x.WorkAttributeID });

            builder.Property(x => x.Value)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.WorkSubTypeVariant)
                   .WithMany(e => e.WorkSubTypeVariant_WorkAttributes)
                   .HasForeignKey(x => x.WorkSubTypeVariantID);

            builder.HasOne(x => x.WorkAttribute)
                   .WithMany(t => t.WorkSubTypeVariant_WorkAttributes)
                   .HasForeignKey(x => x.WorkAttributeID);
        }
    }
}
