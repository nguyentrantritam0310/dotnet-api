using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkSubTypeVariantConfiguration : IEntityTypeConfiguration<WorkSubTypeVariant>
    {
        public void Configure(EntityTypeBuilder<WorkSubTypeVariant> builder)
        {
            builder.ToTable("WorkSubTypeVariants");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

        builder.Property(x => x.Description)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.WorkSubType)
                .WithMany(r => r.WorkSubTypeVariants)
                .HasForeignKey(x => x.WorkSubTypeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
