using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class AttendanceMachineConfiguration : IEntityTypeConfiguration<AttendanceMachine>
    {
        public void Configure(EntityTypeBuilder<AttendanceMachine> builder)
        {
            builder.ToTable("AttendanceMachines");

            builder.HasKey(x => x.ID);
            
            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();



        }
    }
}
