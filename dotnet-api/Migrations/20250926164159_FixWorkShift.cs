using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "coefficient",
                table: "OvertimeType",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ac3893c9-4b01-4463-bc2e-4cb50f5d18c8", "AQAAAAIAAYagAAAAEOD0Vpls0F3ZYGBxRwLSgflreC9b+hJrTMxBWOBs3nu7Uina0XAV9y0LBEdLgc5BDA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "08e3d1b3-da27-43e1-b54a-d33dc35cdbba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "800ef09c-eebd-4a13-91b6-c7bdf0416207");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "967ef4d3-37c6-4270-bf94-e7448978671c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "463d65d7-a4e1-4426-9855-65b93343be83");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "448ff29f-3f38-4f1c-b44d-69d0a3e99595");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "f0536376-d1bc-421b-b775-4d1556f26a1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "08e2c06d-8734-4499-9dd1-b8c216e27ae4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "a33c68c9-58b7-422b-8f47-3680b257d561");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "e9c0a97d-0d05-4401-b612-646d7a9bf0ce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "48f7157e-69e4-42bf-b4be-a860a7e9fc59");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "f02ce474-40ee-43f6-a4d1-ef1bea4f1412");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 26, 23, 41, 57, 162, DateTimeKind.Local).AddTicks(844));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 26, 23, 41, 57, 162, DateTimeKind.Local).AddTicks(848));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 26, 23, 41, 57, 161, DateTimeKind.Local).AddTicks(3474));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 26, 23, 41, 57, 162, DateTimeKind.Local).AddTicks(828));

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 1,
                column: "coefficient",
                value: 1.5f);

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 2,
                column: "coefficient",
                value: 2f);

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 3,
                column: "coefficient",
                value: 3f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "coefficient",
                table: "OvertimeType",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "60dab946-a52f-4771-96d0-91d65b98ebf7", "AQAAAAIAAYagAAAAEGTCQKgZqsB9d4NgVlgq7lGTqE1QqkQBlirV42pyDtHAwToG8BpLsZmp6pwSvg+hcQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "10686254-3617-4fa1-a959-6adbae24f42d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "ddfaff9c-e950-4177-a515-a9e82611fd09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "af1eb258-c24f-46f9-9324-f26d4b50dc66");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "213e2d94-9fc1-4c51-9409-f6eb1048de4a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "491c4593-f11b-40a7-9953-1608b6b8890f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "46ad31fd-8f52-4c4c-a401-027eb9129c99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "b9d3ffb7-c825-4046-9a49-e7cbe985092f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "50a6d3a9-fdea-4b0e-8af4-7aef3b906b33");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "78d8e365-542f-4def-84b5-5dcb8d9b2c26");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "5de6f526-2580-4ce6-81b6-00873eead4c5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "962f6bf5-4126-4aa0-bb2a-1d9839c6912e");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8030));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 22, 47, 42, 680, DateTimeKind.Local).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 1,
                column: "coefficient",
                value: 1.5);

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 2,
                column: "coefficient",
                value: 2.0);

            migrationBuilder.UpdateData(
                table: "OvertimeType",
                keyColumn: "ID",
                keyValue: 3,
                column: "coefficient",
                value: 3.0);
        }
    }
}
