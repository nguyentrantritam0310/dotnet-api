using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FixContractDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, update data to convert string values to int
            migrationBuilder.Sql(@"
                UPDATE [Contracts] 
                SET [ApproveStatus] = CASE 
                    WHEN [ApproveStatus] = 'Đã duyệt' THEN 2
                    WHEN [ApproveStatus] = 'Chờ duyệt' THEN 1
                    WHEN [ApproveStatus] = 'Từ chối' THEN 3
                    WHEN [ApproveStatus] = 'Tạo mới' THEN 0
                    ELSE 1
                END
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ContractForms_ContractFormID",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "ContractForms");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractFormID",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractFormID",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "ApproveStatus",
                table: "Contracts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6e07e62-9ee1-4892-a983-3dea6e44d36e", "AQAAAAIAAYagAAAAEEXtx0h+U95q5tNGLM24jnikxkTKOHDq3n/fInzJHZWArMph5I4sBjLUHZaKV3V42Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "3ef13cd2-8d2e-41e2-a0c2-3eb6573613b0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "7887bb69-0a9c-4385-bb5d-afd777ff87b6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "81f584b1-1446-428d-a56a-a00c5cc9caf2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "0027dcc6-4f7f-4a0e-9ed5-c845598fe4bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "79f4bb67-962a-4712-85a4-6a33100a59ad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "bb4dc7d4-a581-43c5-8e96-6977479f59f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "b8e9eb6f-8567-4025-b6f1-e9b50b7f920f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "dd4359e8-becd-4451-90e1-5bb2c4c92da1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "860ffc48-6fd0-4e76-8410-0311afce16a1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "e6d241f2-5179-49be-a5be-0b82b0f80ea4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "5cc46a71-9b64-4876-baf8-1dcaf87f979b");

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "ContractTypeName",
                value: "Hợp đồng thử việc");

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 2,
                column: "ContractTypeName",
                value: "Hợp đồng xác định thời hạn");

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "ContractTypeName",
                value: "Hợp đồng không xác định thời hạn");

            migrationBuilder.UpdateData(
                table: "Contracts",
                keyColumn: "ID",
                keyValue: 1,
                column: "ApproveStatus",
                value: 2);

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 19, 22, 998, DateTimeKind.Local).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 19, 22, 998, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 19, 22, 997, DateTimeKind.Local).AddTicks(7345));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 11, 19, 22, 998, DateTimeKind.Local).AddTicks(5305));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApproveStatus",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContractFormID",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4d1490e3-c81d-4df1-a070-89d34ce938cc", "AQAAAAIAAYagAAAAEOkDE61DoK5fhM0WA3XgwPpBmWT9/FFUrukJ3c9jVON8jfcg5VYAXMQcrN6lfbbuxA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "66c7fca3-69a3-4dd6-858d-b26bde9a437d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "1926db8d-f144-4063-82f5-f4b385b229fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "44ee3304-cede-4306-b5a3-f787f7ec823c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "0ffdde0e-c50c-4d23-8dd1-ab43f2ace9dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "4217864d-3117-4cb5-a9ab-4bc09baa2deb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "362a2a7a-6d4a-4f24-ac9e-d5ee25d80c49");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "50cd23f0-e200-4965-8b63-250dcd26d163");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "2c9eef03-7b1e-4ae2-9401-30a220e0d553");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "c05b85de-c15c-4099-9343-62c84f8c8455");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "df370baa-187b-4a7c-ad6f-bcce7ef215e6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "d862f630-203a-4c1f-b6b1-eae903596919");

            migrationBuilder.InsertData(
                table: "ContractForms",
                columns: new[] { "ID", "ContractFormName" },
                values: new object[,]
                {
                    { 1, "Chính thức" },
                    { 2, "Thử việc" },
                    { 3, "Khoán việc" }
                });

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "ContractTypeName",
                value: "Hợp đồng lao động");

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 2,
                column: "ContractTypeName",
                value: "Hợp đồng thử việc");

            migrationBuilder.UpdateData(
                table: "ContractTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "ContractTypeName",
                value: "Hợp đồng khoán việc");

            migrationBuilder.UpdateData(
                table: "Contracts",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ApproveStatus", "ContractFormID", "Status" },
                values: new object[] { "Đã duyệt", 1, "Đã ký" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 52, 14, 614, DateTimeKind.Local).AddTicks(9755));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 52, 14, 614, DateTimeKind.Local).AddTicks(9847));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 52, 14, 614, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 52, 14, 614, DateTimeKind.Local).AddTicks(9719));

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractFormID",
                table: "Contracts",
                column: "ContractFormID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ContractForms_ContractFormID",
                table: "Contracts",
                column: "ContractFormID",
                principalTable: "ContractForms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
