using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeForm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OvertimeFormName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeForm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OvertimeTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coefficient = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRequests",
                columns: table => new
                {
                    VoucherCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvertimeTypeID = table.Column<int>(type: "int", nullable: true),
                    OvertimeFormID = table.Column<int>(type: "int", nullable: true),
                    LeaveTypeID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRequests", x => x.VoucherCode);
                    table.ForeignKey(
                        name: "FK_EmployeeRequests_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRequests_LeaveType_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "LeaveType",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeRequests_OvertimeForm_OvertimeFormID",
                        column: x => x.OvertimeFormID,
                        principalTable: "OvertimeForm",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_EmployeeRequests_OvertimeType_OvertimeTypeID",
                        column: x => x.OvertimeTypeID,
                        principalTable: "OvertimeType",
                        principalColumn: "ID");
                });

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

            migrationBuilder.InsertData(
                table: "LeaveType",
                columns: new[] { "ID", "LeaveTypeName" },
                values: new object[,]
                {
                    { 1, "Phép năm" },
                    { 2, "Nghỉ bù" }
                });

            migrationBuilder.InsertData(
                table: "OvertimeForm",
                columns: new[] { "ID", "OvertimeFormName" },
                values: new object[,]
                {
                    { 1, "Tăng ca tính lương" },
                    { 2, "Tăng ca nghỉ bù" }
                });

            migrationBuilder.InsertData(
                table: "OvertimeType",
                columns: new[] { "ID", "OvertimeTypeName", "coefficient" },
                values: new object[,]
                {
                    { 1, "Tăng ca ngày thường", 1.5 },
                    { 2, "Tăng ca ngày nghỉ", 2.0 },
                    { 3, "Tăng ca ngày lễ", 3.0 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRequests",
                columns: new[] { "VoucherCode", "ApproveStatus", "CreatedAt", "EmployeeID", "EndDateTime", "LeaveTypeID", "OvertimeFormID", "OvertimeTypeID", "Reason", "RequestType", "StartDateTime" },
                values: new object[,]
                {
                    { "LV001", 0, new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8027), "tech1-id", new DateTime(2025, 9, 27, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, "Về quê thăm gia đình", "Nghỉ phép", new DateTime(2025, 9, 25, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "LV002", 0, new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8030), "tech1-id", new DateTime(2025, 9, 30, 17, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, "Nghỉ bù sau khi tăng ca", "Nghỉ phép", new DateTime(2025, 9, 30, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "OT001", 0, new DateTime(2025, 9, 19, 22, 47, 42, 680, DateTimeKind.Local).AddTicks(9395), "tech1-id", new DateTime(2025, 9, 21, 22, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1, "Hoàn thành báo cáo dự án", "Tăng ca", new DateTime(2025, 9, 21, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "OT002", 0, new DateTime(2025, 9, 19, 22, 47, 42, 681, DateTimeKind.Local).AddTicks(8007), "tech1-id", new DateTime(2025, 9, 22, 23, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 2, "Bảo trì hệ thống ngoài giờ", "Tăng ca", new DateTime(2025, 9, 22, 19, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRequests_EmployeeID",
                table: "EmployeeRequests",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRequests_LeaveTypeID",
                table: "EmployeeRequests",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRequests_OvertimeFormID",
                table: "EmployeeRequests",
                column: "OvertimeFormID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRequests_OvertimeTypeID",
                table: "EmployeeRequests",
                column: "OvertimeTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRequests");

            migrationBuilder.DropTable(
                name: "LeaveType");

            migrationBuilder.DropTable(
                name: "OvertimeForm");

            migrationBuilder.DropTable(
                name: "OvertimeType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6682d7a-7f04-4700-9820-b8f6fa5b6389", "AQAAAAIAAYagAAAAECt7mVfEirbO3Mt34bIyXvq30zMOeWWOWegWL1t8G5cmAUwJcRcuvtusx0XlkEeEDg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "2acc9f96-4a84-42af-a835-ccfd1e75b486");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "69e5e1f3-64f1-4931-bd5b-6449a18a37a3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "da6f2550-e908-402f-ae54-9036dbbf5500");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "deba8060-0990-49ca-9073-440fb053081a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "de66db6a-26ff-4fea-8480-d6ba86bf7014");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "fd327bfd-0090-45cf-a3dd-cb0fb5bd27ce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "720120b3-08d7-4b50-be2b-3d3cc0aeb053");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "63680edb-7047-4db6-bbf9-4dda2b1dc11e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "b2b7d44d-3e0c-444a-9624-665117c1d8eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "b966921c-3dac-477c-83bd-7bd0ebb5f3ba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "8329936d-052e-42a1-a74d-affc9660ca41");
        }
    }
}
