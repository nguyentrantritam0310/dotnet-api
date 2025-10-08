using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FamilyModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_FamilyRelations_AspNetUsers_EmployeeID",
                table: "Employee_FamilyRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_FamilyRelations",
                table: "Employee_FamilyRelations");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FamilyRelations_FamilyRelationID",
                table: "Employee_FamilyRelations");

            migrationBuilder.DeleteData(
                table: "Employee_FamilyRelations",
                keyColumns: new[] { "EmployeeID", "FamilyRelationID" },
                keyValues: new object[] { "admin-id", 1 });

            migrationBuilder.DeleteData(
                table: "Employee_FamilyRelations",
                keyColumns: new[] { "EmployeeID", "FamilyRelationID" },
                keyValues: new object[] { "admin-id", 2 });

            migrationBuilder.AlterColumn<string>(
                name: "RelativeName",
                table: "FamilyRelations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RelationShipName",
                table: "Employee_FamilyRelations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_FamilyRelations",
                table: "Employee_FamilyRelations",
                columns: new[] { "FamilyRelationID", "EmployeeID" });

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

            migrationBuilder.InsertData(
                table: "Employee_FamilyRelations",
                columns: new[] { "EmployeeID", "FamilyRelationID", "RelationShipName" },
                values: new object[,]
                {
                    { "admin-id", 1, "Cha" },
                    { "admin-id", 2, "Mẹ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FamilyRelations_EmployeeID",
                table: "Employee_FamilyRelations",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_FamilyRelations_AspNetUsers_EmployeeID",
                table: "Employee_FamilyRelations",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_FamilyRelations_AspNetUsers_EmployeeID",
                table: "Employee_FamilyRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee_FamilyRelations",
                table: "Employee_FamilyRelations");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FamilyRelations_EmployeeID",
                table: "Employee_FamilyRelations");

            migrationBuilder.DeleteData(
                table: "Employee_FamilyRelations",
                keyColumns: new[] { "EmployeeID", "FamilyRelationID" },
                keyValues: new object[] { "admin-id", 1 });

            migrationBuilder.DeleteData(
                table: "Employee_FamilyRelations",
                keyColumns: new[] { "EmployeeID", "FamilyRelationID" },
                keyValues: new object[] { "admin-id", 2 });

            migrationBuilder.AlterColumn<string>(
                name: "RelativeName",
                table: "FamilyRelations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RelationShipName",
                table: "Employee_FamilyRelations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee_FamilyRelations",
                table: "Employee_FamilyRelations",
                columns: new[] { "EmployeeID", "FamilyRelationID" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7db60c9e-b7fb-4055-81cb-c633a5978348", "AQAAAAIAAYagAAAAEAkFOBB3Yi1J64W3zuzFT9z+y/bn06zBXAhV+8oawrIjjVkdrTIB6Jldwj7meZfh9g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "ed4cdd14-407e-4f42-b1a9-d2cb5398a0e8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "4937d42e-0fc7-4d26-91ae-76c2e50308ca");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "36d3291f-cba7-4e97-8f2e-45be94bebd80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "cc0b3882-3aad-4f60-8fc5-6f40d2f9140f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "9801fc94-38c0-4be9-bd95-fde835e70d82");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "c3a7d10f-52d5-44f3-a1d3-9f390aa1feed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "8cbd455c-1609-4c5c-9eff-0cfc88c57a8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "62618614-5c4e-4c1b-8ebf-1aee550fc7a2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "c2517648-c784-4f92-abc1-278a3cda1894");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "94e8d4dc-acb3-419c-91ad-fefe48c489d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "3280bacd-fcb1-44a0-bc46-f59e6aaad7d2");

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

            migrationBuilder.InsertData(
                table: "Employee_FamilyRelations",
                columns: new[] { "EmployeeID", "FamilyRelationID", "RelationShipName" },
                values: new object[,]
                {
                    { "admin-id", 1, "Cha" },
                    { "admin-id", 2, "Mẹ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_FamilyRelations_FamilyRelationID",
                table: "Employee_FamilyRelations",
                column: "FamilyRelationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_FamilyRelations_AspNetUsers_EmployeeID",
                table: "Employee_FamilyRelations",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
