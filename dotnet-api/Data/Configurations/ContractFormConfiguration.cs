using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ContractFormConfiguration : IEntityTypeConfiguration<ContractForm>
    {
        public void Configure(EntityTypeBuilder<ContractForm> builder)
        {
            builder.HasKey(x => x.ID);

        }
    }
}
