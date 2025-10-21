using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePayrollTimeSheetRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_TimeSheets_ID",
                table: "Payrolls");

            // Drop and recreate the ID column to fix IDENTITY property
            migrationBuilder.DropColumn(
                name: "ID",
                table: "Payrolls");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Payrolls",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payrolls",
                table: "Payrolls",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a523b999-06f9-4111-9af8-e07582680ed5", "AQAAAAIAAYagAAAAEJnTKOgLVz+8qe9kr1slSCUXwrDxQ+/+dxN6JDWH09ANTNZN2P8MgWAfgfJDTcIY2A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "51feae64-2a92-4e51-908d-53661848c262");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "539614f2-d6ec-4a7d-b608-aa2776cc65fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "2454871e-4e9f-49bf-a01f-aaa70df74335");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "0859bc9c-8cdf-4ca3-bb56-7a430c10103d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "d53eeec1-d1b5-46b4-8a6f-4ef29c0df29d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "00e5179e-7438-4525-b9c6-a283a21808d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "20c2e8de-e811-4b86-a727-8eaed2fde1ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "2093331c-b7a3-4f61-8257-877312a17d22");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "eacbfaff-fac1-4c6c-8cf5-c46cfbc15d99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "3a09c906-bac5-4bb0-9ac1-1ad4dc71f3ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "529701ad-b3ca-4fe7-983c-a050211d04e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "614ff4c1-e2dc-4022-9878-27d9b558565a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "916c57d8-aca8-4dfc-8a5d-fd4c27ee2b7a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "f6a552d9-00b9-4d19-b695-150f0d872ce2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "dbdd4c8d-e8f2-44ed-acdd-04602e608788");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "564c5776-c5ca-4af8-ad2b-86f463709ade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "6138a4b0-fd09-475e-95d6-b3e6519ec824");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "afe7e508-5af6-4043-bdac-2f75227bdbb2");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 15, 20, 33, 488, DateTimeKind.Local).AddTicks(5143), new DateTime(2025, 10, 20, 15, 20, 33, 488, DateTimeKind.Local).AddTicks(5248) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 20, 33, 489, DateTimeKind.Local).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 20, 33, 489, DateTimeKind.Local).AddTicks(171));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 20, 33, 488, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 20, 33, 489, DateTimeKind.Local).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 21, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(3928), new DateTime(2025, 9, 21, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(3812) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4345), new DateTime(2025, 9, 26, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4344) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4349), new DateTime(2025, 10, 1, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4348) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4352), new DateTime(2025, 10, 6, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4351) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4355), new DateTime(2025, 10, 11, 15, 20, 33, 490, DateTimeKind.Local).AddTicks(4354) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop and recreate the ID column to remove IDENTITY property
            migrationBuilder.DropPrimaryKey(
                name: "PK_Payrolls",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Payrolls");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Payrolls",
                type: "int",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payrolls",
                table: "Payrolls",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c5b61890-a0ea-4dd9-8929-2a75d8311ffc", "AQAAAAIAAYagAAAAEJ2+7Gv/+h1NdkX9Y/1Cbyhvzv+7F3A0lYiRdggNcAWuLzYSXYK+J8BPTgGnsEcniA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "39876796-4c84-45cf-a5f8-7acb8915ad67");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "47b80ad8-2697-4e3f-9cc6-292876609713");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "a9244846-6393-41c6-bc96-85e83b8f48eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "3a8eff8f-bf03-4152-a299-dc5bf9dc3474");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "b865145f-91fe-46dd-b164-2d11f9d8f2a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "b2298465-8cda-4e58-8ab6-2d80826091c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "41616f14-b1c7-4c21-b388-c401058e16c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "963b0012-5298-4a18-ae34-386627a805e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "d5376451-9537-4d9e-a5e4-72731aa15ed2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "fc62652f-ecc3-4f4f-b233-6f9ee8c98a6b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "f5a151bf-5bbe-4121-8c02-16c79be46761");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "6369decb-9a8f-4654-b51d-3e02a174b6d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "64065020-04ab-412c-a9e7-7faf82b38cfa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "9efc55cc-d783-48cf-9dff-7cd61c9defe4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "35f68799-5a74-4d9d-816a-a3de9814da37");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "d0a4068c-2f0c-418d-bdf8-69a30fd19d42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "47f032e1-6d0e-495a-a042-8ee99635310e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "904eb49b-dc39-4d22-91e2-0153a5dfeed0");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(872), new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(976) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6047));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 21, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(5450), new DateTime(2025, 9, 21, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(5308) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7124), new DateTime(2025, 9, 26, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7107) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7128), new DateTime(2025, 10, 1, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7127) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7134), new DateTime(2025, 10, 6, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7133) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7137), new DateTime(2025, 10, 11, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7136) });

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_TimeSheets_ID",
                table: "Payrolls",
                column: "ID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
