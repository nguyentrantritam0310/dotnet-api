using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class ConstructionTaskConfiguration : IEntityTypeConfiguration<ConstructionTask>
    {
        public void Configure(EntityTypeBuilder<ConstructionTask> builder)
        {
            builder.ToTable("ConstructionTasks");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Workload)
                   .IsRequired();

            builder.HasOne(x => x.ConstructionPlan)
                   .WithMany(cp => cp.ConstructionTasks)
                   .HasForeignKey(x => x.ConstructionPlanID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ConstructionStatus)
                   .WithMany(cs => cs.ConstructionTasks)
                   .HasForeignKey(x => x.ConstructionStatusID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
