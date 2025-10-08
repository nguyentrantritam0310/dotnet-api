using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class Contract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowanceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractForms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractFormName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractForms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FamilyRelations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyRelations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalStandardWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalUnpaidLeave = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidLeave = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompensatedOvertime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayableOvertime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalActualWorkdays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LateArrivalCount = table.Column<int>(type: "int", nullable: false),
                    EarlyLeaveCount = table.Column<int>(type: "int", nullable: false),
                    UnexcusedAbsenceCount = table.Column<int>(type: "int", nullable: false),
                    TimeSheetClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSheetNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractTypeID = table.Column<int>(type: "int", nullable: false),
                    ContractFormID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApproveStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractForms_ContractFormID",
                        column: x => x.ContractFormID,
                        principalTable: "ContractForms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTypes_ContractTypeID",
                        column: x => x.ContractTypeID,
                        principalTable: "ContractTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee_FamilyRelations",
                columns: table => new
                {
                    FamilyRelationID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RelationShipName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_FamilyRelations", x => new { x.EmployeeID, x.FamilyRelationID });
                    table.ForeignKey(
                        name: "FK_Employee_FamilyRelations_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_FamilyRelations_FamilyRelations_FamilyRelationID",
                        column: x => x.FamilyRelationID,
                        principalTable: "FamilyRelations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DailySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeaveSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EatAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PetrolAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HealthInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnemploymentInsuranceEmployee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SocialInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HealthInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnemploymentInsuranceEmployer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxableIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DependentDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonalIncomeTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayrollClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayrollNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Payrolls_TimeSheets_ID",
                        column: x => x.ID,
                        principalTable: "TimeSheets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetFeedbacks",
                columns: table => new
                {
                    TimeSheetID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSheetFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Contract_Allowances",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false),
                    AllowanceID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_Allowances", x => new { x.AllowanceID, x.ContractID });
                    table.ForeignKey(
                        name: "FK_Contract_Allowances_Allowances_AllowanceID",
                        column: x => x.AllowanceID,
                        principalTable: "Allowances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Allowances_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayrollFeedbacks",
                columns: table => new
                {
                    PayrollID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayrollFeedbackDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Allowances",
                columns: new[] { "ID", "AllowanceName" },
                values: new object[,]
                {
                    { 1, "Phụ cấp ăn trưa" },
                    { 2, "Phụ cấp xăng xe" },
                    { 3, "Phụ cấp điện thoại" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5474319c-9fd0-4bcf-818d-77d0eb0349a4", "AQAAAAIAAYagAAAAEDY/Yli7qqUS5sgCcME7S5fxrz0g1oXFy4+9/L/ljB1C2dm/faSCcQDjFpFRxbmRvw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "b851455e-7c98-43a2-a311-2ceee33d9951");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "04924f0b-f0f6-46c0-8d53-57b4f42e3e2e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "85a63d10-a9dd-435f-9ff8-fb71eded1c88");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "4268edaa-cd2e-4182-b0a0-2605e775e552");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "ed9b6db9-bcba-4df4-921a-26aa56da2e0a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "7736ec6f-9ed2-4446-9cb4-43691b1e4ab2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "7c33cdf7-91e8-41f2-ac03-3dae08aea1e2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "1a1d475b-2855-4de2-8995-d3a8763f4bdc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "7d4c3a8f-d893-48a0-9407-ff04d8f8d030");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "a740f22c-74c8-422a-8379-07d8d53a31e2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "ab3bdd95-43ff-4aee-a444-216273b8b896");

            migrationBuilder.InsertData(
                table: "ContractForms",
                columns: new[] { "ID", "ContractFormName" },
                values: new object[,]
                {
                    { 1, "Chính thức" },
                    { 2, "Thử việc" },
                    { 3, "Khoán việc" }
                });

            migrationBuilder.InsertData(
                table: "ContractTypes",
                columns: new[] { "ID", "ContractTypeName" },
                values: new object[,]
                {
                    { 1, "Hợp đồng lao động" },
                    { 2, "Hợp đồng thử việc" },
                    { 3, "Hợp đồng khoán việc" }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 2, 23, 32, 0, 393, DateTimeKind.Local).AddTicks(7818));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 2, 23, 32, 0, 393, DateTimeKind.Local).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 2, 23, 32, 0, 393, DateTimeKind.Local).AddTicks(297));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 2, 23, 32, 0, 393, DateTimeKind.Local).AddTicks(7805));

            migrationBuilder.InsertData(
                table: "FamilyRelations",
                columns: new[] { "ID", "EndDate", "RelativeName", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Văn A", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trần Thị B", new DateTime(2005, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TimeSheets",
                columns: new[] { "ID", "CompensatedOvertime", "EarlyLeaveCount", "EmployeeID", "EmployeeName", "LateArrivalCount", "PayableOvertime", "TimeSheetClosingDate", "TimeSheetNotes", "TotalActualWorkdays", "TotalPaidLeave", "TotalStandardWorkdays", "TotalUnpaidLeave", "TotalWorkdays", "UnexcusedAbsenceCount" },
                values: new object[] { 1, 2m, 0, "admin-id", "Phạm Văn Đốc", 0, 2m, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bảng công tháng 9/2025", 24m, 2m, 22m, 0m, 24m, 0 });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ID", "ApproveStatus", "ContractFormID", "ContractNumber", "ContractSalary", "ContractTypeID", "EmployeeID", "EndDate", "InsuranceSalary", "StartDate", "Status" },
                values: new object[] { 1, "Đã duyệt", 1, "HD001", 20000000m, 1, "admin-id", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đã ký" });

            migrationBuilder.InsertData(
                table: "Employee_FamilyRelations",
                columns: new[] { "EmployeeID", "FamilyRelationID", "RelationShipName" },
                values: new object[,]
                {
                    { "admin-id", 1, "Cha" },
                    { "admin-id", 2, "Mẹ" }
                });

            migrationBuilder.InsertData(
                table: "Payrolls",
                columns: new[] { "ID", "ActualSalary", "Bonus", "ContractSalary", "ContractType", "DailySalary", "DependentDeduction", "EatAllowance", "EmployeeID", "GrossIncome", "HealthInsuranceEmployee", "HealthInsuranceEmployer", "InsuranceSalary", "LeaveSalary", "MealAllowance", "NetIncome", "NetPay", "OtherIncome", "OvertimeSalary", "PayrollClosingDate", "PayrollNotes", "PersonalDeduction", "PersonalIncomeTax", "PetrolAllowance", "SocialInsuranceEmployee", "SocialInsuranceEmployer", "TaxableIncome", "TotalAllowance", "TotalContractSalary", "TotalDeduction", "UnemploymentInsuranceEmployee", "UnemploymentInsuranceEmployer", "UnionFee" },
                values: new object[] { 1, 20000000m, 2000000m, 20000000m, "Hợp đồng lao động", 800000m, 4400000m, 500000m, "admin-id", 22000000m, 360000m, 720000m, 18000000m, 0m, 200000m, 18800000m, 18800000m, 500000m, 1000000m, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lương tháng 9/2025", 11000000m, 1200000m, 300000m, 1800000m, 3600000m, 20000000m, 1000000m, 20000000m, 3200000m, 180000m, 360000m, 100000m });

            migrationBuilder.InsertData(
                table: "TimeSheetFeedbacks",
                columns: new[] { "EmployeeID", "TimeSheetID", "Content", "TimeSheetFeedbackDate", "Title" },
                values: new object[] { "manager1-id", 1, "Thiếu ngày công do lỗi chấm công.", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phản ánh bảng công tháng 9" });

            migrationBuilder.InsertData(
                table: "Contract_Allowances",
                columns: new[] { "AllowanceID", "ContractID", "Value" },
                values: new object[,]
                {
                    { 1, 1, 500000m },
                    { 2, 1, 300000m }
                });

            migrationBuilder.InsertData(
                table: "PayrollFeedbacks",
                columns: new[] { "EmployeeID", "PayrollID", "Content", "PayrollFeedbackDate", "Title" },
                values: new object[] { "manager1-id", 1, "Lương chưa tính đủ phụ cấp xăng xe.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phản ánh lương tháng 9" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Allowances_ContractID",
                table: "Contract_Allowances",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractFormID",
                table: "Contracts",
                column: "ContractFormID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeID",
                table: "Contracts",
                column: "ContractTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeID",
                table: "Contracts",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FamilyRelations_FamilyRelationID",
                table: "Employee_FamilyRelations",
                column: "FamilyRelationID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract_Allowances");

            migrationBuilder.DropTable(
                name: "Employee_FamilyRelations");

            migrationBuilder.DropTable(
                name: "PayrollFeedbacks");

            migrationBuilder.DropTable(
                name: "TimeSheetFeedbacks");

            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "FamilyRelations");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "ContractForms");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c34bea63-63c2-476c-8f86-cb52a7cb2545", "AQAAAAIAAYagAAAAEPNcoCDFlJpd/gYGHx/ZO2g9gQpxoYbvRUY84cw/BcElYS2MoZ4Pqi74tSLpng1vmQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "e2c7625d-f3b2-4847-9cde-3dd20e81c954");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "11e1e43e-ba97-49e7-a2d0-a9b150d9e322");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "1201da24-582c-4d45-a0f7-bf12d365fd54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "c33ec118-33ff-4ba1-ab6f-686bda760fe3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "22c52298-6b59-456c-b9d3-70eebc8902dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "da6f50d6-d98e-47e6-a7f8-f5a76fb8b3e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "8cc40ff0-292b-4bdc-9b9a-5618047e60c0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "effb3abe-93dd-42f3-b06c-a4805db251ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "3aa261eb-3fa2-459f-a91c-283a3c4e6926");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "8d88d93a-41d5-4727-8ad2-68d40491f299");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "b82cd0a7-d649-482c-a828-27ecce7b6119");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 19, 54, 28, 872, DateTimeKind.Local).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 19, 54, 28, 872, DateTimeKind.Local).AddTicks(2140));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 19, 54, 28, 871, DateTimeKind.Local).AddTicks(4402));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 19, 54, 28, 872, DateTimeKind.Local).AddTicks(2120));
        }
    }
}
