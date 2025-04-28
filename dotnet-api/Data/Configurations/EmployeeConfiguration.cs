using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasMaxLength(15);

            builder.Property(x => x.Email)
                .HasMaxLength(100);

            builder.Property(x => x.Status)
                .HasMaxLength(50);

            builder.HasOne(x => x.Role)
                .WithMany(ci => ci.Employees) 
                .HasForeignKey(x => x.RoleID)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
