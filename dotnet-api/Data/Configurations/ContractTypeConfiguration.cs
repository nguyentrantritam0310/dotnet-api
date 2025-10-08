using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ContractTypeConfiguration : IEntityTypeConfiguration<ContractType>
    {
        public void Configure(EntityTypeBuilder<ContractType> builder)
        {
            builder.HasKey(x => x.ID);
        }
    }
}
