using Microsoft.EntityFrameworkCore;
using dotnet_api.Data.Entities;
using dotnet_api.Data.Configurations;
using dotnet_api.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace dotnet_api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<AdjustmentItem> AdjustmentItems { get; set; }
        public DbSet<AdjustmentType> AdjustmentTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceMachine> AttendanceMachines { get; set; }
        public DbSet<FaceRegistration> FaceRegistrations { get; set; }
        public DbSet<Construction> Constructions { get; set; }
        public DbSet<ConstructionItem> ConstructionItems { get; set; }
        public DbSet<ConstructionPlan> ConstructionPlans { get; set; }
        public DbSet<ConstructionStatus> ConstructionStatuses { get; set; }
        public DbSet<ConstructionTask> ConstructionTasks { get; set; }
        public DbSet<ConstructionTemplateItem> ConstructionTemplateItems { get; set; }
        public DbSet<ConstructionType> ConstructionTypes { get; set; }
        public DbSet<EmployeeRequests> EmployeeRequests { get; set; }
        public DbSet<ExportOrder> ExportOrders { get; set; }
        public DbSet<ImportOrder> ImportOrders { get; set; }
        public DbSet<ImportOrderEmployee> ImportOrderEmployees { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Material_ExportOrder> Material_ExportOrders { get; set; }
        public DbSet<MaterialNorm> MaterialNorms { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<MaterialPlan> MaterialPlans { get; set; }
        public DbSet<OvertimeForm> OvertimeForms { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<PayrollAdjustment> PayrollAdjustments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportAttachment> ReportAttachments { get; set; }
        public DbSet<ReportStatusLog> ReportStatusLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }
        public DbSet<ShiftDetail> ShiftDetails { get; set; }
        public DbSet<UnitofMeasurement> UnitofMeasuremens { get; set; }
        public DbSet<WorkAttribute> WorkAttributes { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<WorkSubType> WorkSubTypes { get; set; }
        public DbSet<WorkSubTypeVariant_WorkAttribute> WorkSubTypeVariant_WorkAttributes { get; set; }
        public DbSet<WorkSubTypeVariant> WorkSubTypeVariants { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Contract_Allowance> Contract_Allowances { get; set; }
        public DbSet<ApplicationUser_PayrollAdjustment> ApplicationUser_PayrollAdjustments { get; set; }
        public DbSet<ApprovalHistory> ApprovalHistories { get; set; }

    // Family & Payroll
    public DbSet<FamilyRelation> FamilyRelations { get; set; }
    public DbSet<Employee_FamilyRelation> Employee_FamilyRelations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity tables
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "AspNetUsers");
                
                // Configure relationships
                entity.HasOne(e => e.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(e => e.RoleID);

                // Configure required fields
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Phone).IsRequired();
                entity.Property(e => e.Status).IsRequired();
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "AspNetRoles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
            });

            // Apply configurations
            modelBuilder.ApplyConfiguration(new AdjustmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AdjustmentItemConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUser_PayrollAdjustmentConfiguration());
            modelBuilder.ApplyConfiguration(new PayrollAdjustmentConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
            modelBuilder.ApplyConfiguration(new AttendanceMachineConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionItemConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionStatusConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionTaskConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionTemplateItemConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ExportOrderConfiguration());
            modelBuilder.ApplyConfiguration(new ImportOrderConfiguration());
            modelBuilder.ApplyConfiguration(new ImportOrderEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialPlanConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialExportOrderConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialNormConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportAttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new ReportStatusLogConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftDetailConfiguration());
            modelBuilder.ApplyConfiguration(new UnitofMeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new WorkAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkShiftConfiguration());
            modelBuilder.ApplyConfiguration(new WorkSubTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkSubTypeVariant_WorkAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkSubTypeVariantConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new ContractTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AllowanceConfiguration());
            modelBuilder.ApplyConfiguration(new Contract_AllowanceConfiguration());
            modelBuilder.ApplyConfiguration(new FaceRegistrationConfiguration());

            // Family & Payroll configs
            modelBuilder.ApplyConfiguration(new FamilyRelationConfiguration());
            modelBuilder.ApplyConfiguration(new Employee_FamilyRelationConfiguration());


            // Seed data
            modelBuilder.Seed();
        }
    }
}
