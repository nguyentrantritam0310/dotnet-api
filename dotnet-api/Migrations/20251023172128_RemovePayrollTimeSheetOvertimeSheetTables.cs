using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePayrollTimeSheetOvertimeSheetTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OvertimeSheets");

            migrationBuilder.DropTable(
                name: "PayrollFeedbacks");

            migrationBuilder.DropTable(
                name: "TimeSheetFeedbacks");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f56ca123-ac32-4398-b03a-01679159e75f", "AQAAAAIAAYagAAAAEGKvq3UDv7U2mqRG8Xpp9+BLxxGB2q3Ct75+FThQjOpP8d8jrTwA8mhEkOEXbp28Gw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "4dfa62aa-eb26-48a7-b2cd-022ba16ad925");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "be33747c-b5ec-4903-af6c-af12eca02ec8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "9d90bbd0-a209-4189-a6d2-64c482101fd5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "d4062a9a-7c8a-4fff-bc6c-2a1a4ebade10");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "6cd83cb5-ef42-4053-904b-2b9009b34c47");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "4ead78c4-ceaf-41dd-aa09-45af67d2c260");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "a96f2a3c-1887-40ab-a8ba-e124952263bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "6de8adeb-8c34-4cde-bf4c-31afe87ee4f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "2bd84eda-1e94-4702-af8c-cc0ee509a29c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "62ce8f60-10b2-4f9d-8595-54bdd6a2c364");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "3f0cb289-92da-40c2-b5ca-fcf0581d8422");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "1adc39cc-a144-4cf3-b76d-22557001f6ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "3bad9a26-5d03-434e-bdee-4d601f863c57");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "e3d83bca-a937-4888-b449-0d56a0231835");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "2610828f-25d0-4e95-adb4-3f1dd4e83dbe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "68ab4dea-030f-4876-8fe1-9a5e41f334f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "7c397a37-9f22-445f-9c4e-6025153d8708");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "6fab4f52-6362-453e-9599-6069159e6437");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 23, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 23, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 23, 0, 21, 26, 913, DateTimeKind.Local).AddTicks(8965), new DateTime(2025, 10, 23, 0, 21, 26, 913, DateTimeKind.Local).AddTicks(9068) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3393));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8055), new DateTime(2025, 9, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(7948) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 29, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8457), new DateTime(2025, 9, 29, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8456) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 4, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8460), new DateTime(2025, 10, 4, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 9, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8463), new DateTime(2025, 10, 9, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8463) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 14, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8466), new DateTime(2025, 10, 14, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8465) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActualSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DependentDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EatAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HealthInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HealthInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    LeaveSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayrollClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayrollNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonalIncomeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PetrolAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxableIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnemploymentInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnemploymentInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payrolls_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompensatedOvertime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarlyLeaveCount = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    LateArrivalCount = table.Column<int>(type: "int", nullable: false),
                    PayableOvertime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeSheetClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSheetNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalActualWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidLeave = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalStandardWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUnpaidLeave = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnexcusedAbsenceCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeSheets_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayrollID = table.Column<int>(type: "int", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    OvertimeClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvertimeCoefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OvertimeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOvertimeDays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOvertimeHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OvertimeSheets_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeSheets_Payrolls_PayrollID",
                        column: x => x.PayrollID,
                        principalTable: "Payrolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayrollFeedbacks",
                columns: table => new
                {
                    PayrollID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayrollFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollFeedbacks", x => new { x.PayrollID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_PayrollFeedbacks_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollFeedbacks_Payrolls_PayrollID",
                        column: x => x.PayrollID,
                        principalTable: "Payrolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetFeedbacks",
                columns: table => new
                {
                    TimeSheetID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSheetFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheetFeedbacks", x => new { x.TimeSheetID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_TimeSheetFeedbacks_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheetFeedbacks_TimeSheets_TimeSheetID",
                        column: x => x.TimeSheetID,
                        principalTable: "TimeSheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31207bfb-e702-4820-b7b0-7ca238eb991c", "AQAAAAIAAYagAAAAEOO2i5Te3GWEc8fL4dlZQYugszAl6MXiMHGPq6t2l/bq8Rwkd6BdTX0PH7TVhSdiHw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "241f955b-e84b-40ca-8976-decd5c36fcf0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "76e842ec-a41b-417d-8299-ace45900d12f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "78149344-dabb-4d15-a2e0-65a10e8389cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "f850a219-7694-4bc6-b46a-e90f32176c4f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "fc07660b-69f1-45ec-948f-3583616286ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "c7aea5e1-b3d9-4c71-8d81-36622ae4944d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "dac0682f-36ff-4f91-b0d2-e28e9cf4f9e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "dd439793-5b63-4062-8b84-01a83854821b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "e6bb822a-8d81-47dc-be37-1230bc6d881b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "9580771e-7edb-4d94-8f95-59bd509afca8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "340b27dc-b636-4519-9e27-73406fcc6acc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "dc41d7c2-ce98-4093-ac87-ba340f0fce8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "5429aed5-02da-46cc-b407-cbb12ed9046c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "6f88fcd3-97e4-466d-b11c-48cd65eab663");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "cf46e736-f8b5-4293-b2a2-7b4a6ec9bc39");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "9f279da9-bd1c-4d30-92cc-32714c1068af");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "fefda413-77df-47a8-895b-82a0b888b74f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "28d0777b-a6c5-44ba-a8b5-856b9dc7b43d");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 20, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 20, 15, 25, 19, 350, DateTimeKind.Local).AddTicks(3664), new DateTime(2025, 10, 20, 15, 25, 19, 350, DateTimeKind.Local).AddTicks(3801) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(610));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(607));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 21, 15, 25, 19, 352, DateTimeKind.Local).AddTicks(6756), new DateTime(2025, 9, 21, 15, 25, 19, 352, DateTimeKind.Local).AddTicks(6552) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8170), new DateTime(2025, 9, 26, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8150) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8185), new DateTime(2025, 10, 1, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8189), new DateTime(2025, 10, 6, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8189) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8192), new DateTime(2025, 10, 11, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8192) });

            migrationBuilder.InsertData(
                table: "Payrolls",
                columns: new[] { "ID", "ActualSalary", "Bonus", "ClosedAt", "ClosedBy", "ContractSalary", "ContractType", "DailySalary", "DependentDeduction", "EatAllowance", "EmployeeID", "GrossIncome", "HealthInsuranceEmployee", "HealthInsuranceEmployer", "InsuranceSalary", "IsClosed", "LeaveSalary", "MealAllowance", "NetIncome", "NetPay", "OtherIncome", "OvertimeSalary", "PayrollClosingDate", "PayrollNotes", "PersonalDeduction", "PersonalIncomeTax", "PetrolAllowance", "SocialInsuranceEmployee", "SocialInsuranceEmployer", "TaxableIncome", "TotalAllowance", "TotalContractSalary", "TotalDeduction", "UnemploymentInsuranceEmployee", "UnemploymentInsuranceEmployer", "UnionFee" },
                values: new object[] { 1, 20000000m, 2000000m, null, null, 20000000m, "Hợp đồng lao động", 800000m, 4400000m, 500000m, "admin-id", 22000000m, 360000m, 720000m, 18000000m, false, 0m, 200000m, 18800000m, 18800000m, 500000m, 1000000m, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lương tháng 9/2025", 11000000m, 1200000m, 300000m, 1800000m, 3600000m, 20000000m, 1000000m, 20000000m, 3200000m, 180000m, 360000m, 100000m });

            migrationBuilder.InsertData(
                table: "TimeSheets",
                columns: new[] { "ID", "ClosedAt", "ClosedBy", "CompensatedOvertime", "EarlyLeaveCount", "EmployeeID", "EmployeeName", "IsClosed", "LateArrivalCount", "PayableOvertime", "TimeSheetClosingDate", "TimeSheetNotes", "TotalActualWorkdays", "TotalPaidLeave", "TotalStandardWorkdays", "TotalUnpaidLeave", "TotalWorkdays", "UnexcusedAbsenceCount" },
                values: new object[] { 1, null, null, 2m, 0, "admin-id", "Phạm Văn Đốc", false, 0, 2m, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bảng công tháng 9/2025", 24m, 2m, 22m, 0m, 24m, 0 });

            migrationBuilder.InsertData(
                table: "OvertimeSheets",
                columns: new[] { "ID", "ClosedAt", "ClosedBy", "EmployeeID", "EmployeeName", "IsClosed", "OvertimeClosingDate", "OvertimeCoefficient", "OvertimeNotes", "OvertimeSalary", "PayrollID", "TotalOvertimeDays", "TotalOvertimeHours" },
                values: new object[] { 1, null, null, "admin-id", "Phạm Văn Đốc", false, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.5m, "Bảng công tăng ca tháng 9/2025", 1000000m, 1, 3m, 24m });

            migrationBuilder.InsertData(
                table: "PayrollFeedbacks",
                columns: new[] { "EmployeeID", "PayrollID", "Content", "PayrollFeedbackDate", "Title" },
                values: new object[] { "manager1-id", 1, "Lương chưa tính đủ phụ cấp xăng xe.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phản ánh lương tháng 9" });

            migrationBuilder.InsertData(
                table: "TimeSheetFeedbacks",
                columns: new[] { "EmployeeID", "TimeSheetID", "Content", "TimeSheetFeedbackDate", "Title" },
                values: new object[] { "manager1-id", 1, "Thiếu ngày công do lỗi chấm công.", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phản ánh bảng công tháng 9" });

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeSheets_EmployeeID",
                table: "OvertimeSheets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeSheets_PayrollID",
                table: "OvertimeSheets",
                column: "PayrollID");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollFeedbacks_EmployeeID",
                table: "PayrollFeedbacks",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_EmployeeID",
                table: "Payrolls",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetFeedbacks_EmployeeID",
                table: "TimeSheetFeedbacks",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_EmployeeID",
                table: "TimeSheets",
                column: "EmployeeID");
        }
    }
}
