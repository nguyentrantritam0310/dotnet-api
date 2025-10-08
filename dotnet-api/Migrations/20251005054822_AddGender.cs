using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash" },
                values: new object[] { "12371d40-5f1f-4067-b324-602f2614ee91", "Nam", "AQAAAAIAAYagAAAAEJ1Xyi1etm2oBmOG8nYw3r3HU0AF/kN7PI+VtiBA7/6rl+roiLXBeHYizKrb+9kLdA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "32bbfda2-0f91-41d3-b6d1-f995d34ab87b", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "52b296d3-80cc-4f30-afe1-60ffd9a74f39", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "37dc6b69-682c-4486-bf2f-d3a3a85e443a", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "161bd947-283c-42ac-872c-01c354671507", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "bf7bf418-f7f2-480e-b470-252347c40bd7", "Nữ" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "caa650c6-f93b-447b-884d-8a46431c0cfe", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "1106f0ae-1faf-4115-b7f6-728c6499afbd", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "d344e423-0a30-4ccc-80da-0c2a60440a1d", "Nữ" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "de15dc5f-4c68-4245-a72b-999d1943e35b", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "f9434ee6-4bd0-4b57-8491-b5204eac5064", "Nam" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "Gender" },
                values: new object[] { "7dead6a7-e083-4a61-a010-f8c70cb26822", "Nữ" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

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
                column: "CreatedAt",
                value: new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 3, 0, 5, 6, 697, DateTimeKind.Local).AddTicks(8859));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 3, 0, 5, 6, 698, DateTimeKind.Local).AddTicks(8097));
        }
    }
}
