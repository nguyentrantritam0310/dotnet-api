using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionTemplateItemConfiguration : IEntityTypeConfiguration<ConstructionTemplateItem>
    {
        public void Configure(EntityTypeBuilder<ConstructionTemplateItem> builder)
        {
            builder.ToTable("ConstructionTemplateItems");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

        builder.Property(x => x.ConstructionTemplateItemName)
                   .IsRequired()
                   .HasMaxLength(200);

            // Relationships

            builder.HasOne(x => x.ConstructionType)
                   .WithMany(c => c.ConstructionTemplateItems)
                   .HasForeignKey(x => x.ConstructionTypeID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.WorkSubTypeVariant)
                   .WithMany(c => c.ConstructionTemplateItems)
                   .HasForeignKey(x => x.WorkSubTypeVarientID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
