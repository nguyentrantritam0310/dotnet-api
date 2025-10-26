using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dotnet_api.Data.Entities;

namespace dotnet_api.Data.Configurations
{
    public class FaceRegistrationConfiguration : IEntityTypeConfiguration<FaceRegistration>
    {
        public void Configure(EntityTypeBuilder<FaceRegistration> builder)
        {
            builder.HasKey(fr => fr.ID);

            builder.Property(fr => fr.EmployeeId)
                .IsRequired()
                .HasMaxLength(450); // Identity user ID length

            builder.Property(fr => fr.FaceId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(fr => fr.ImagePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(fr => fr.EmbeddingData)
                .IsRequired()
                .HasColumnType("TEXT"); // For large JSON data

            builder.Property(fr => fr.Confidence)
                .IsRequired()
                .HasColumnType("REAL");

            builder.Property(fr => fr.RegisteredDate)
                .IsRequired();

            builder.Property(fr => fr.LastUpdated)
                .IsRequired();

            builder.Property(fr => fr.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(fr => fr.RegisteredBy)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(fr => fr.Notes)
                .HasMaxLength(1000);

            // Configure relationship with ApplicationUser
            builder.HasOne(fr => fr.Employee)
                .WithMany(u => u.FaceRegistrations)
                .HasForeignKey(fr => fr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes for performance
            builder.HasIndex(fr => fr.EmployeeId);
            builder.HasIndex(fr => fr.FaceId).IsUnique();
            builder.HasIndex(fr => fr.IsActive);
            builder.HasIndex(fr => fr.RegisteredDate);
        }
    }
}
