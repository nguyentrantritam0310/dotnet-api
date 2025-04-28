using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class WorkSubTypeConfiguration : IEntityTypeConfiguration<WorkSubType>
    {
        public void Configure(EntityTypeBuilder<WorkSubType> builder)
        {
            builder.ToTable("WorkSubTypes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.WorkSubTypeName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(x => x.WorkType)
                .WithMany(r => r.WorkSubTypes)
                .HasForeignKey(x => x.WorkTypeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
