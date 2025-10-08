using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class Employee_FamilyRelationConfiguration : IEntityTypeConfiguration<Employee_FamilyRelation>
    {
        public void Configure(EntityTypeBuilder<Employee_FamilyRelation> builder)
        {
            builder.ToTable("Employee_FamilyRelations");

            builder.HasKey(x => new { x.FamilyRelationID, x.EmployeeID });

            builder.Property(x => x.RelationShipName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.FamilyRelation)
                .WithMany(x => x.EmployeeFamilyRelations)
                .HasForeignKey(x => x.FamilyRelationID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}