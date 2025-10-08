using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class kct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdjustmentItemID",
                table: "PayrollAdjustments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5800e502-1d6e-4f0d-94de-ec2c08b4a9f0", "AQAAAAIAAYagAAAAEOJyrUhlGeS8X0abLOOEIHEPRNMB+Shey1UC3q102aPpXNSUu6gPRe+t+VtPdpVqyQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "57283afe-5fde-4375-8a3a-0930689f71d9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "4f83710e-4ec5-4e57-bd93-f8fce47f9df0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "a001f4b2-5967-4878-aea0-b39a3dece87f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "bd5f4d57-3e32-4fb0-a9eb-66fb35fa3b3a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "29453cbc-6a2a-45a7-9ad6-c1ff5fdc7cee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "fd375072-b874-4ab0-89b9-95f3f7320adb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "bee9d078-25fe-431d-9289-b5e6ef0fe9c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "4e5c9fa8-3cc6-41d5-ba27-c1cfea784a9b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "9fd23016-9748-4ec4-886c-ebbca4d16767");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "3a7fb0bc-ba22-4163-afdf-995819b7c2fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "1a1689a7-e398-41c2-90ca-b740243e1a14");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 34, 9, 243, DateTimeKind.Local).AddTicks(6779));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 34, 9, 243, DateTimeKind.Local).AddTicks(6895));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 34, 9, 242, DateTimeKind.Local).AddTicks(8115));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 34, 9, 243, DateTimeKind.Local).AddTicks(6765));

            migrationBuilder.UpdateData(
                table: "PayrollAdjustments",
                keyColumn: "voucherNo",
                keyValue: "PA-001",
                column: "AdjustmentItemID",
                value: null);

            migrationBuilder.UpdateData(
                table: "PayrollAdjustments",
                keyColumn: "voucherNo",
                keyValue: "PA-002",
                column: "AdjustmentItemID",
                value: null);

            migrationBuilder.UpdateData(
                table: "PayrollAdjustments",
                keyColumn: "voucherNo",
                keyValue: "PA-003",
                column: "AdjustmentItemID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdjustments_AdjustmentItemID",
                table: "PayrollAdjustments",
                column: "AdjustmentItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments",
                column: "AdjustmentItemID",
                principalTable: "AdjustmentItems",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_PayrollAdjustments_AdjustmentItemID",
                table: "PayrollAdjustments");

            migrationBuilder.DropColumn(
                name: "AdjustmentItemID",
                table: "PayrollAdjustments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "12371d40-5f1f-4067-b324-602f2614ee91", "AQAAAAIAAYagAAAAEJ1Xyi1etm2oBmOG8nYw3r3HU0AF/kN7PI+VtiBA7/6rl+roiLXBeHYizKrb+9kLdA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "32bbfda2-0f91-41d3-b6d1-f995d34ab87b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "52b296d3-80cc-4f30-afe1-60ffd9a74f39");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "37dc6b69-682c-4486-bf2f-d3a3a85e443a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "161bd947-283c-42ac-872c-01c354671507");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "bf7bf418-f7f2-480e-b470-252347c40bd7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "caa650c6-f93b-447b-884d-8a46431c0cfe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "1106f0ae-1faf-4115-b7f6-728c6499afbd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "d344e423-0a30-4ccc-80da-0c2a60440a1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "de15dc5f-4c68-4245-a72b-999d1943e35b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "f9434ee6-4bd0-4b57-8491-b5204eac5064");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "7dead6a7-e083-4a61-a010-f8c70cb26822");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 12, 48, 20, 544, DateTimeKind.Local).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 12, 48, 20, 544, DateTimeKind.Local).AddTicks(676));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 12, 48, 20, 543, DateTimeKind.Local).AddTicks(3359));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 5, 12, 48, 20, 544, DateTimeKind.Local).AddTicks(550));
        }
    }
}
