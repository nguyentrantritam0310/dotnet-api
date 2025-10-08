using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class AllowanceConfiguration : IEntityTypeConfiguration<Allowance>
    {
        public void Configure(EntityTypeBuilder<Allowance> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();
        }
    }
}
