using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionTypeConfiguration : IEntityTypeConfiguration<ConstructionType>
    {
        public void Configure(EntityTypeBuilder<ConstructionType> builder)
        {
            builder.ToTable("ConstructionTypes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.ConstructionTypeName)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
