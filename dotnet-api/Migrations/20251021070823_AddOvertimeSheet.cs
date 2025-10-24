using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddOvertimeSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "TimeSheets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosedBy",
                table: "TimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "TimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Payrolls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosedBy",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Payrolls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OvertimeSheets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalOvertimeDays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalOvertimeHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeCoefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvertimeNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeSheets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OvertimeSheets_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeSheets_Payrolls_PayrollID",
                        column: x => x.PayrollID,
                        principalTable: "Payrolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 20, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(872), new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(976) });

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

            migrationBuilder.InsertData(
                table: "OvertimeSheets",
                columns: new[] { "ID", "ClosedAt", "ClosedBy", "EmployeeID", "EmployeeName", "IsClosed", "OvertimeClosingDate", "OvertimeCoefficient", "OvertimeNotes", "OvertimeSalary", "PayrollID", "TotalOvertimeDays", "TotalOvertimeHours" },
                values: new object[] { 1, null, null, "admin-id", "Phạm Văn Đốc", false, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.5m, "Bảng công tăng ca tháng 9/2025", 1000000m, 1, 3m, 24m });

            migrationBuilder.UpdateData(
                table: "Payrolls",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ClosedAt", "ClosedBy", "IsClosed" },
                values: new object[] { null, null, false });

            migrationBuilder.UpdateData(
                table: "TimeSheets",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ClosedAt", "ClosedBy", "IsClosed" },
                values: new object[] { null, null, false });

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeSheets_EmployeeID",
                table: "OvertimeSheets",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeSheets_PayrollID",
                table: "OvertimeSheets",
                column: "PayrollID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OvertimeSheets");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "ClosedBy",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "ClosedBy",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Payrolls");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6c484c03-2994-4a92-bf63-a61ac80e4564", "AQAAAAIAAYagAAAAEGNdxYsOBH5fozlffrVUQnrdQL2G9TAx5wKRPiKHUg+yBHztJhWC8g0koLYsaJKfSA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "dc723dcf-6eb0-43ee-a42a-f1293772fcff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "958b1484-fb11-4d68-b072-cd1a36c8d142");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "6f93c179-92d6-4230-ba42-7c3597260b63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "ba9bd07d-467b-4b3c-98f4-72de4ceae356");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "c9d600f6-cf6e-4e73-9c7a-28c7f4eb73c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "89bd2939-5eb8-407d-8e7f-9e935994771f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "64eb8c76-8126-4bc1-94d9-c13989e48be6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "03ee3a65-9d62-4181-944a-99eb9aa217d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "35e5a5de-c852-4384-90d2-9fe2c0cd494b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "22488519-ea2a-46cb-a93a-eae788a3394c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "ffc81f35-9ede-4ec6-99a4-5d7c496ca884");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "4298c76e-48b1-40d0-88c8-a51c9525c7c3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "78d05726-1c81-41ae-a852-04da8420954b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "31eb8b34-4871-46cf-97db-172956ebc69e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "09a14310-a855-42dc-8b30-8baf9c1120d1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "4551481a-911b-400e-9e3e-3ee92035420a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "66d5b1f9-3c7a-46dc-926b-39379190fc00");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "8666632a-549c-4217-8e1b-96a74f1ad920");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 17, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 17, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 17, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(1679), new DateTime(2025, 10, 17, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(1813) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(6009));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(6126));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(6005));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 18, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6549), new DateTime(2025, 9, 18, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6361) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 23, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6991), new DateTime(2025, 9, 23, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6989) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 28, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6994), new DateTime(2025, 9, 28, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6994) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 3, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6997), new DateTime(2025, 10, 3, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6996) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 8, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(7000), new DateTime(2025, 10, 8, 19, 16, 55, 559, DateTimeKind.Local).AddTicks(6999) });
        }
    }
}
