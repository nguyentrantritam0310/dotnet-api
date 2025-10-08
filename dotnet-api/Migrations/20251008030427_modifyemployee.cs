using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class modifyemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "decisionDate",
                table: "PayrollAdjustments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date");

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
                values: new object[] { "7db60c9e-b7fb-4055-81cb-c633a5978348", "GD001", "AQAAAAIAAYagAAAAEAkFOBB3Yi1J64W3zuzFT9z+y/bn06zBXAhV+8oawrIjjVkdrTIB6Jldwj7meZfh9g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "ed4cdd14-407e-4f42-b1a9-d2cb5398a0e8", "CH001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "4937d42e-0fc7-4d26-91ae-76c2e50308ca", "CH002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "36d3291f-cba7-4e97-8f2e-45be94bebd80", "CH003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "cc0b3882-3aad-4f60-8fc5-6f40d2f9140f", "KT001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "9801fc94-38c0-4be9-bd95-fde835e70d82", "KT002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "c3a7d10f-52d5-44f3-a1d3-9f390aa1feed", "KT003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "8cbd455c-1609-4c5c-9eff-0cfc88c57a8d", "T001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "62618614-5c4e-4c1b-8ebf-1aee550fc7a2", "T002" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "c2517648-c784-4f92-abc1-278a3cda1894", "T003" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "94e8d4dc-acb3-419c-91ad-fefe48c489d6", "T004" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "EmployeeCode" },
                values: new object[] { "3280bacd-fcb1-44a0-bc46-f59e6aaad7d2", "T005" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 4, 25, 798, DateTimeKind.Local).AddTicks(8964));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 4, 25, 798, DateTimeKind.Local).AddTicks(9066));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 4, 25, 798, DateTimeKind.Local).AddTicks(1366));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 4, 25, 798, DateTimeKind.Local).AddTicks(8943));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "decisionDate",
                table: "PayrollAdjustments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

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
        }
    }
}
