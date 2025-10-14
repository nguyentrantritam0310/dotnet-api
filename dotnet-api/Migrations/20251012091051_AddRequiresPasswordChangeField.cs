using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiresPasswordChangeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiresPasswordChange",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RequiresPasswordChange" },
                values: new object[] { "a7860fc0-f6a9-4d46-9cec-ad2a5d2afa0d", "AQAAAAIAAYagAAAAEJQan4g1MznxMMgwkso21jpPluIfJ5k5HTWq7EAhMW5bXDHJfpJGRcbQAep4j1qeng==", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "5a75c064-6903-4497-a45a-54611cf426ac", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "ac2ff2ba-24b4-4ace-b987-1beec98a2d89", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "20b7dfe1-c0ca-4046-be11-943d155a82ff", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "bd47c35b-191f-4099-8e6f-faa3e5c17ccf", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "abcdcc17-959a-4000-a0a6-a905bfe9b317", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "0e3e4a42-f710-4c33-939c-0044d98051bc", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "3023c5d9-e83c-4055-a4cb-a3c840d5c33a", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "36cc650f-8e31-4a65-9533-e63cf3c2703e", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "93516133-ae9c-49cf-a390-9248889176fb", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "5e43b55d-4c81-4899-b622-07cb7d9ca19a", false });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "RequiresPasswordChange" },
                values: new object[] { "cb94a345-14bb-463e-a879-d8d611c277e9", false });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 10, 49, 324, DateTimeKind.Local).AddTicks(517));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 10, 49, 324, DateTimeKind.Local).AddTicks(674));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 10, 49, 322, DateTimeKind.Local).AddTicks(9961));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 12, 16, 10, 49, 324, DateTimeKind.Local).AddTicks(478));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresPasswordChange",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b28e0c72-ca49-4d07-ac96-2706909445a9", "AQAAAAIAAYagAAAAEL2QQC618mx/QBxyrTLsObVkuTjOf1L5P400w7olBCTya52hCBwqoumkJg46KAg2gw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "2cbb884c-17cf-4278-ba78-553b4fedbdc0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "8104c57c-0ae4-4618-a0c5-2bdd5a774b3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "d9d8b2ba-c857-4f87-8f07-11eac9d886d4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "50531371-8b90-4d69-95e3-e6f289da46df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "593cca53-245a-4bc8-b7b1-3aee65f68523");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "39b2cc9b-1a50-40d5-975b-29846accc594");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "84d02d8b-6aff-4cb7-9796-4417ac7e4735");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "d186eeb9-dc84-4626-9fda-0adc4368090b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "8a818258-291a-4b4d-be5f-2e2dcac4e1d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "2f90d8aa-1091-4b46-8e6d-63eb0ad264ca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "60818620-9506-450e-a435-6d29ced6fdd6");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 22, 44, 701, DateTimeKind.Local).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 22, 44, 701, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 22, 44, 700, DateTimeKind.Local).AddTicks(9003));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 8, 10, 22, 44, 701, DateTimeKind.Local).AddTicks(6556));
        }
    }
}
