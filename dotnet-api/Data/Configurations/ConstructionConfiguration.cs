using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionConfiguration : IEntityTypeConfiguration<Construction>
    {
        public void Configure(EntityTypeBuilder<Construction> builder)
        {
            builder.ToTable("Constructions");

            builder.HasKey(x => x.ID);
            
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ConstructionName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Location)
                   .HasMaxLength(300);

            builder.Property(x => x.TotalArea)
                   .IsRequired();

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.ExpectedCompletionDate)
                   .IsRequired();

            builder.Property(x => x.ActualCompletionDate);

            builder.Property(x => x.DesignBlueprint)
                   .HasMaxLength(500);

            builder.HasOne(x => x.ConstructionType)
                   .WithMany(y=>y.Constructions) 
                   .HasForeignKey(x => x.ConstructionTypeID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ConstructionStatus)
                   .WithMany(y=>y.Constructions) 
                   .HasForeignKey(x => x.ConstructionStatusID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
