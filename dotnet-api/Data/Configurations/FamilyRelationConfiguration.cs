using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class FamilyRelationConfiguration : IEntityTypeConfiguration<FamilyRelation>
    {
        public void Configure(EntityTypeBuilder<FamilyRelation> builder)
        {
            builder.ToTable("FamilyRelations");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.RelativeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.HasMany(x => x.EmployeeFamilyRelations)
                .WithOne(x => x.FamilyRelation)
                .HasForeignKey(x => x.FamilyRelationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}