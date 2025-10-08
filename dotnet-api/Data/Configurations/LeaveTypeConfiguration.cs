using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.ToTable("LeaveTypes");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();  
        }
    }
}
