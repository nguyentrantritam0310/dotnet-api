using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.Property(x => x.EmployeeCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasMaxLength(15);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RoleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 