using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FixEmployeeCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cf7c9883-87b0-4b10-9b12-16f7f237d57a", "AQAAAAIAAYagAAAAEC/dxOidsbZf8QpVMuO84ejPp7WSJ/9Lt9JEKBFjdu1oP1ZVp7SlpSBWK7Kx1ZiUgg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "f8767681-0d77-4b9b-ae9e-9570e35da19c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "1a39b387-4001-489c-bc1c-ea672e2994e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "67934f5f-56f9-46d0-8bac-e49dab6e714e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "b385eeac-8030-4c66-8fb8-06015daef02b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "eb687d15-85d8-46ce-90da-e1ad5ff7956d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "599a04bd-52fe-48a1-8c1f-0eb5cd894cd9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "5f058db8-46e4-44d1-bb64-b769d3d87410");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "971b7a96-19af-4614-857a-cb74b44d8b0d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "b4e2497f-31e7-4bc8-a608-177646d2eb31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "258d3366-65bd-4834-8286-d928ce78e7be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "676907ac-3adf-4434-905a-fec290db8487");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6599));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 975, DateTimeKind.Local).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6438));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode", "PasswordHash" },
                values: new object[] { "d33264f2-bd95-48e2-9199-da2d65eae373", "GD001", "AQAAAAIAAYagAAAAEDl8k1jLV8lP/d6561YQWj5fspE5mI1+zJYMILcJtL8W4zmnAd2Jb9ibfuzyA/8U8Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "1fa83d71-a0ad-44ea-9e37-d70c31a0c267", "CH001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "3717df94-4bc8-4041-8ed5-d5a1762b8b32", "CH002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "d60dc3b7-d14f-4969-bfcd-edc5dfc2e245", "CH003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "6efb31a1-a042-4c56-9484-0824d965d467", "KT001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "9d5b5f63-e54c-4142-8be9-5fb1d3872b23", "KT002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "3adbab28-aad4-4264-a6b2-72be13d71f2e", "KT003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "c14cd1b3-a3c5-4b2f-9155-2967f71defb2", "T001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "77fa12b7-c64a-405f-a061-63c6837e4341", "T002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "a4372bf0-b574-49de-8517-ba9515b5e44d", "T003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "1cab0fc9-6e37-4219-bb37-3c1ecd171d6f", "T004" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "bfd260c8-ee6e-4279-ad14-b57e626921fe", "T005" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 57, 20, 257, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 57, 20, 257, DateTimeKind.Local).AddTicks(8416));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 57, 20, 257, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 57, 20, 257, DateTimeKind.Local).AddTicks(8287));
        }
    }
}
