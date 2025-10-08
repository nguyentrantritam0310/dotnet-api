using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class ok1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "decisionDate",
                table: "PayrollAdjustments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5fe7eb8b-8aae-4236-b601-6dfc32b5d978", "AQAAAAIAAYagAAAAENo1kQwNbR/QbV0sxZxCjRv2TwKh9OfJ9x16YZQVsK19Xk8YaYSJxPs7zs671ALZTQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "ffb81fbc-4b27-4fe7-9a86-260793463c6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "74b2bd2e-4775-4b8d-897b-9e8445cc8e0d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "5c36c31d-8f15-443d-9e6a-e3aff3becbef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "4de24351-2b9f-4323-a8e0-d9fd2ac4c04f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "549d30af-28a2-4901-a449-eaaa9f788437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "105c372d-5ded-4c48-9c5c-0f78f8387ce6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "ee5c595d-a767-4d01-b2e2-6c1ad90fee55");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "68c70ab6-1a01-4933-92e1-bfbda4faa65c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "4359d3c4-872a-4d3d-b5bf-7a4e9af6c6f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "3a98611d-b378-4dba-9489-e0ad68cc4e1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "ecac7964-2015-412a-bcc5-8984faaac0da");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 42, 13, 384, DateTimeKind.Local).AddTicks(8265));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 42, 13, 384, DateTimeKind.Local).AddTicks(8419));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 42, 13, 384, DateTimeKind.Local).AddTicks(646));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 42, 13, 384, DateTimeKind.Local).AddTicks(8251));

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments",
                column: "AdjustmentItemID",
                principalTable: "AdjustmentItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "decisionDate",
                table: "PayrollAdjustments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "68bfd1ee-e998-445c-b9ed-119fbd40c822", "AQAAAAIAAYagAAAAECctaqQuaBWlLJ15Y8Xeext4OC91Sn7h7YCP0sUYtM3V0AG4fg4kOn48ceFb0nPWnA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "231f133f-4c50-469a-b815-6baa3c8ace78");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "d7ef6575-c4d7-4aa9-b739-258eddaeb1a0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "77b43a89-e43c-4999-a967-1e77dcbe07d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "173383f9-d60c-4079-b721-27140444bb3b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "5df27ecc-3c27-47c8-929a-36d9242ddbc2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "b5821b01-e9fb-4a31-bf9e-34f628625902");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "976aa104-f6b1-4376-8f3e-7d9b54e4ee48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "9a5fee9f-5cca-49a8-9ae9-9b63a56fb870");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "1d47a8f4-1063-4705-a77e-2aeb0a70caf3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "b834d4c0-b0c2-4314-b9bc-e4e65e7710cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "b679ae66-6c08-4c49-8a4c-1354f394f5ff");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 37, 9, 352, DateTimeKind.Local).AddTicks(212));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 37, 9, 352, DateTimeKind.Local).AddTicks(323));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 37, 9, 351, DateTimeKind.Local).AddTicks(2193));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 6, 23, 37, 9, 352, DateTimeKind.Local).AddTicks(196));

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdjustments_AdjustmentItems_AdjustmentItemID",
                table: "PayrollAdjustments",
                column: "AdjustmentItemID",
                principalTable: "AdjustmentItems",
                principalColumn: "ID");
        }
    }
}
