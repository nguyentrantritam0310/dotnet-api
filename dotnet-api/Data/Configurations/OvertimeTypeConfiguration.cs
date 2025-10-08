using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class OvertimeTypeConfiguration : IEntityTypeConfiguration<OvertimeType>
    {
        public void Configure(EntityTypeBuilder<OvertimeType> builder)
        {
            builder.ToTable("OvertimeTypes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

           
        }
    }
}
