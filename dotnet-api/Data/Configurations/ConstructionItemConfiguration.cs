using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionItemConfiguration : IEntityTypeConfiguration<ConstructionItem>
    {
        public void Configure(EntityTypeBuilder<ConstructionItem> builder)
        {
            builder.ToTable("ConstructionItems");

            builder.HasKey(x => x.ID);
           
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ConstructionItemName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.ExpectedCompletionDate)
                   .IsRequired();

            builder.Property(x => x.ActualCompletionDate);

            builder.Property(x => x.TotalVolume)
                   .IsRequired();

            // Relationships

            builder.HasOne(x => x.Construction)
                   .WithMany(c => c.ConstructionItems)
                   .HasForeignKey(x => x.ConstructionID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UnitOfMeasurement)
                   .WithMany(ci => ci.ConstructionItems) 
                   .HasForeignKey(x => x.UnitOfMeasurementID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ConstructionStatus)
                   .WithMany(ci => ci.ConstructionItems) 
                   .HasForeignKey(x => x.ConstructionStatusID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.WorkSubTypeVariant)
                    .WithMany(ci => ci.ConstructionItems)
                    .HasForeignKey(x => x.WorkSubTypeVariantID)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
