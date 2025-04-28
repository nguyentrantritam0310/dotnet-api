using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionStatusConfiguration : IEntityTypeConfiguration<ConstructionStatus>
    {
        public void Configure(EntityTypeBuilder<ConstructionStatus> builder)
        {
            builder.ToTable("ConstructionStatuses");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
