using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionPlanConfiguration : IEntityTypeConfiguration<ConstructionPlan>
    {
        public void Configure(EntityTypeBuilder<ConstructionPlan> builder)
        {
            builder.ToTable("ConstructionPlans");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.ExpectedCompletionDate)
                   .IsRequired();

            builder.Property(x => x.ActualCompletionDate);

            builder.HasOne(x => x.ConstructionStatus)
                   .WithMany(ci => ci.ConstructionPlans) 
                   .HasForeignKey(x => x.ConstructionStatusID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Employee)
                   .WithMany(ci => ci.ConstructionPlans) 
                   .HasForeignKey(x => x.EmployeeID)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ConstructionItem)
                   .WithMany(ci => ci.ConstructionPlans)
                   .HasForeignKey(x => x.ConstructionItemID)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
