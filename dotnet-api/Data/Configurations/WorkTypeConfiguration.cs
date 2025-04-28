using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
    {
        public void Configure(EntityTypeBuilder<WorkType> builder)
        {
            builder.ToTable("WorkTypeItems");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

        builder.Property(x => x.WorkTypeName)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}
