using dotnet_api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_api.Data.Configurations
{
    public class EmployeeRequestConfiguration : IEntityTypeConfiguration<EmployeeRequests>
    {
        public void Configure(EntityTypeBuilder<EmployeeRequests> builder)
        {
            builder.ToTable("EmployeeRequests");

            builder.HasKey(x => x.VoucherCode);
            
            builder.HasOne(x => x.LeaveType)
                           .WithMany(e => e.EmployeeRequests)
                                       .HasForeignKey(x => x.LeaveTypeID);
            builder.HasOne(x => x.OvertimeForm)
                           .WithMany(e => e.EmployeeRequests)
                                       .HasForeignKey(x => x.OvertimeFormID);
            builder.HasOne(x => x.OvertimeType)
                           .WithMany(e => e.EmployeeRequests)
                                       .HasForeignKey(x => x.OvertimeTypeID);
            builder.HasOne(x => x.Employee)
                           .WithMany(e => e.EmployeeRequests)
                                       .HasForeignKey(x => x.EmployeeID);
        }
    }
}

