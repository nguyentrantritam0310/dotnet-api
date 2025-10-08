using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FixShiftLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkShiftID",
                table: "EmployeeRequests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ecd86f82-138c-45da-93d8-f8b34331611e", "AQAAAAIAAYagAAAAEOHxZPBCm1ORrrdqKAkqtBEaNUChYLjnaX/bAyBJOXKrjEAe4u07QU6borevRERUOg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "00cb1f87-55fe-4de8-866d-8a284098e6ac");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "87677d06-f240-46b9-aaff-08f78fcafed9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "b88741e9-94c2-41ac-8ee2-b34eb57e4610");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "699dea83-f2de-4c53-be29-7152ee24cb7d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "ad6a6f21-dd9d-45fe-a92f-a50d0bf71ddb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "0d23fdfa-acf5-4e17-bb2a-7e518fad3055");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "bfaabeeb-7192-4f5c-b383-904b05908f8b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "8c303afe-6571-48e7-972e-a38b83be6a44");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "b1071386-b71b-4705-9d76-bf131907852f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "fbc031cf-c787-4de5-8e42-3b95280766b6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "60d2784d-e039-404c-8ab0-1f8c45a7e863");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                columns: new[] { "CreatedAt", "WorkShiftID" },
                values: new object[] { new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8118), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                columns: new[] { "CreatedAt", "WorkShiftID" },
                values: new object[] { new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8226), 1 });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                columns: new[] { "CreatedAt", "WorkShiftID" },
                values: new object[] { new DateTime(2025, 10, 3, 0, 5, 6, 697, DateTimeKind.Local).AddTicks(8859), null });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                columns: new[] { "CreatedAt", "WorkShiftID" },
                values: new object[] { new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8097), null });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRequests_WorkShiftID",
                table: "EmployeeRequests",
                column: "WorkShiftID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRequests_WorkShifts_WorkShiftID",
                table: "EmployeeRequests",
                column: "WorkShiftID",
                principalTable: "WorkShifts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRequests_WorkShifts_WorkShiftID",
                table: "EmployeeRequests");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRequests_WorkShiftID",
                table: "EmployeeRequests");

            migrationBuilder.DropColumn(
                name: "WorkShiftID",
                table: "EmployeeRequests");

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
        }
    }
}
