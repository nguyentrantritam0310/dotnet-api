using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class AdjustmentTypeConfiguration : IEntityTypeConfiguration<AdjustmentType>
    {
        public void Configure(EntityTypeBuilder<AdjustmentType> builder)
        {
            builder.ToTable("AdjustmentTypes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();


        }
    }
} 