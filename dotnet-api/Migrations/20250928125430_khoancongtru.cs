using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class khoancongtru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_LeaveType_LeaveTypeID",
                table: "EmployeeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_OvertimeForm_OvertimeFormID",
                table: "EmployeeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_OvertimeType_OvertimeTypeID",
                table: "EmployeeRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeType",
                table: "OvertimeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeForm",
                table: "OvertimeForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveType",
                table: "LeaveType");

            migrationBuilder.RenameTable(
                name: "OvertimeType",
                newName: "OvertimeTypes");

            migrationBuilder.RenameTable(
                name: "OvertimeForm",
                newName: "OvertimeForms");

            migrationBuilder.RenameTable(
                name: "LeaveType",
                newName: "LeaveTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeForms",
                table: "OvertimeForms",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "AdjustmentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustmentItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdjustmentTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdjustmentItems_AdjustmentTypes_AdjustmentTypeID",
                        column: x => x.AdjustmentTypeID,
                        principalTable: "AdjustmentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollAdjustments",
                columns: table => new
                {
                    voucherNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    decisionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdjustmentTypeID = table.Column<int>(type: "int", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollAdjustments", x => x.voucherNo);
                    table.ForeignKey(
                        name: "FK_PayrollAdjustments_AdjustmentTypes_AdjustmentTypeID",
                        column: x => x.AdjustmentTypeID,
                        principalTable: "AdjustmentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser_PayrollAdjustments",
                columns: table => new
                {
                    PayrollAdjustmentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    PayrollAdjustmentvoucherNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser_PayrollAdjustments", x => new { x.PayrollAdjustmentID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_ApplicationUser_PayrollAdjustments_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_PayrollAdjustments_PayrollAdjustments_PayrollAdjustmentID",
                        column: x => x.PayrollAdjustmentID,
                        principalTable: "PayrollAdjustments",
                        principalColumn: "voucherNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_PayrollAdjustments_PayrollAdjustments_PayrollAdjustmentvoucherNo",
                        column: x => x.PayrollAdjustmentvoucherNo,
                        principalTable: "PayrollAdjustments",
                        principalColumn: "voucherNo");
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "ID", "AdjustmentTypeName" },
                values: new object[,]
                {
                    { 1, "Khen thưởng" },
                    { 2, "Tạm ứng" },
                    { 3, "Kỷ luật" },
                    { 4, "Truy thu" },
                    { 5, "Truy lãnh" }
                });

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

            migrationBuilder.InsertData(
                table: "AdjustmentItems",
                columns: new[] { "ID", "AdjustmentItemName", "AdjustmentTypeID" },
                values: new object[,]
                {
                    { 1, "Thưởng năng suất", 1 },
                    { 2, "Thưởng dự án", 1 },
                    { 3, "Tạm ứng lương", 2 },
                    { 4, "Tạm ứng công tác phí", 2 },
                    { 5, "Đi trễ", 3 },
                    { 6, "Nghỉ không phép", 3 },
                    { 7, "Truy thu bảo hiểm", 4 },
                    { 8, "Truy lãnh tăng lương", 5 }
                });

            migrationBuilder.InsertData(
                table: "PayrollAdjustments",
                columns: new[] { "voucherNo", "AdjustmentTypeID", "ApproveStatus", "Month", "Reason", "Year", "decisionDate" },
                values: new object[,]
                {
                    { "PA-001", 1, 2, 9, "Thưởng dự án A", 2025, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "PA-002", 2, 2, 9, "Tạm ứng lương tháng 9", 2025, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "PA-003", 3, 2, 9, "Kỷ luật đi trễ nhiều lần", 2025, new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUser_PayrollAdjustments",
                columns: new[] { "EmployeeID", "PayrollAdjustmentID", "PayrollAdjustmentvoucherNo", "Value" },
                values: new object[,]
                {
                    { "tech1-id", "PA-001", null, 2000000f },
                    { "tech1-id", "PA-002", null, 3000000f },
                    { "tech1-id", "PA-003", null, 500000f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdjustmentItems_AdjustmentTypeID",
                table: "AdjustmentItems",
                column: "AdjustmentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_PayrollAdjustments_EmployeeID",
                table: "ApplicationUser_PayrollAdjustments",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_PayrollAdjustments_PayrollAdjustmentvoucherNo",
                table: "ApplicationUser_PayrollAdjustments",
                column: "PayrollAdjustmentvoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdjustments_AdjustmentTypeID",
                table: "PayrollAdjustments",
                column: "AdjustmentTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_LeaveTypes_LeaveTypeID",
                table: "EmployeeRequests",
                column: "LeaveTypeID",
                principalTable: "LeaveTypes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_OvertimeForms_OvertimeFormID",
                table: "EmployeeRequests",
                column: "OvertimeFormID",
                principalTable: "OvertimeForms",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_OvertimeTypes_OvertimeTypeID",
                table: "EmployeeRequests",
                column: "OvertimeTypeID",
                principalTable: "OvertimeTypes",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_LeaveTypes_LeaveTypeID",
                table: "EmployeeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_OvertimeForms_OvertimeFormID",
                table: "EmployeeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_OvertimeTypes_OvertimeTypeID",
                table: "EmployeeRequests");

            migrationBuilder.DropTable(
                name: "AdjustmentItems");

            migrationBuilder.DropTable(
                name: "ApplicationUser_PayrollAdjustments");

            migrationBuilder.DropTable(
                name: "PayrollAdjustments");

            migrationBuilder.DropTable(
                name: "AdjustmentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeForms",
                table: "OvertimeForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes");

            migrationBuilder.RenameTable(
                name: "OvertimeTypes",
                newName: "OvertimeType");

            migrationBuilder.RenameTable(
                name: "OvertimeForms",
                newName: "OvertimeForm");

            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                newName: "LeaveType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeType",
                table: "OvertimeType",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeForm",
                table: "OvertimeForm",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveType",
                table: "LeaveType",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4861b1e5-99bf-4e1a-a767-0d88b1facfbe", "AQAAAAIAAYagAAAAEMcV5mYNIXFdjdQ8NAIvfDWoQNJFA9cA5U8bdyMkxN6e3IBiHY1KeH5hlcPkTl3VeA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "84ad64ba-d1e0-4ee0-9e07-7b67a24055eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "571ca0ac-762e-4e3e-b54c-590272c84881");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "149bf284-0032-4690-88fc-d7302294cbfb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "b4423058-6698-46fb-9ce3-dad11e717321");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "a58b412a-62f1-4ae7-b22b-36b00ad1edab");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "a090f057-ff6a-4a91-937a-c8367e6178cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "a3b2e296-a5ec-4d5f-941a-a3b924f2978b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "b605266b-c4ec-4311-be4d-7218b8c2014a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "a25ed9fa-a070-4a59-b238-65c2a6a47448");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "27fa7c70-fed5-4995-94f3-ab371db1dd92");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "518adc5f-dc90-49b0-8c46-0c193f7916c9");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 352, DateTimeKind.Local).AddTicks(9774));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8050));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_LeaveType_LeaveTypeID",
                table: "EmployeeRequests",
                column: "LeaveTypeID",
                principalTable: "LeaveType",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_OvertimeForm_OvertimeFormID",
                table: "EmployeeRequests",
                column: "OvertimeFormID",
                principalTable: "OvertimeForm",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_OvertimeType_OvertimeTypeID",
                table: "EmployeeRequests",
                column: "OvertimeTypeID",
                principalTable: "OvertimeType",
                principalColumn: "ID");
        }
    }
}
