using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class OvertimeFormConfiguration : IEntityTypeConfiguration<OvertimeForm>
    {
        public void Configure(EntityTypeBuilder<OvertimeForm> builder)
        {
            builder.ToTable("OvertimeForms");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

           
        }
    }
}
