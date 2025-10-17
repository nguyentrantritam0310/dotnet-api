using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FaceRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceMachineId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDateTime",
                table: "Attendances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckInLocation",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDateTime",
                table: "Attendances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckOutLocation",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FaceRecognitionConfidence",
                table: "Attendances",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FaceRegistrations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FaceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmbeddingData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confidence = table.Column<float>(type: "real", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceRegistrations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FaceRegistrations_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c79a61fc-51da-4367-be99-ae7d78ae0cce", "AQAAAAIAAYagAAAAEMOb2yn6NGbkaUHt+lN4dxyz2Cqzk1JInAijojBT4ian3ldh0mYMY0EyAj3RAFlZLw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "ff50db70-070a-4400-9ed6-66fe5923d1a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "f61054b6-c919-48f6-a4a0-ce8ce993b931");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "3922acc6-9032-4ef5-b81a-4b8bacbad885");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "7250905a-a011-4df9-adb7-3084c3b46e98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "409accd9-4bf9-4155-85e0-8abad0211e86");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "24e85273-313f-4689-94d9-f010f8e1dca2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "c436d47b-b568-4f8a-97a2-54ff86661804");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "fb29be0e-b76d-4c11-853a-2fa6f27a115b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "b5ec7a54-5310-4c34-809d-4d6ec051fad4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "478a8348-7da6-4159-a73d-9da01802ed64");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "9bb7852f-66ce-43bb-b973-8756dca224e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "6dacd2d5-4eb0-4f69-a9e5-bb45a84ea4a4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "cef8e7d3-e1e1-46c5-b305-f5bdc4858a65");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "6988fc3d-df80-4170-b271-9a1bebf7a4fe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "420b0cd2-78ae-4649-a910-6ba0b17ca5d6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "c42908dc-2b49-45a8-b898-530a195e41ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "6f23e188-57e5-47b2-836b-578e35b7a9d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "aea2d41a-a0c2-47b6-a1c3-0a58426853f4");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "AttendanceMachineId", "CheckInDateTime", "CheckInLocation", "CheckOutDateTime", "CheckOutLocation", "CreatedDate", "EmployeeId", "FaceRecognitionConfidence", "ImageCheckIn", "ImageCheckOut", "LastUpdated", "Notes" },
                values: new object[] { 2, new DateTime(2025, 10, 16, 8, 0, 0, 0, DateTimeKind.Local), "Construction Site A", new DateTime(2025, 10, 16, 17, 0, 0, 0, DateTimeKind.Local), "Construction Site A", new DateTime(2025, 10, 16, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(1190), "worker1-id", 0.95f, "/uploads/attendance/worker1-20240912-checkin.jpg", "/uploads/attendance/worker1-20240912-checkout.jpg", new DateTime(2025, 10, 16, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(1288), "Full day attendance with face recognition" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(5358));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(5483));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 17, 18, 26, 37, 83, DateTimeKind.Local).AddTicks(5352));

            migrationBuilder.InsertData(
                table: "FaceRegistrations",
                columns: new[] { "ID", "Confidence", "EmbeddingData", "EmployeeId", "FaceId", "ImagePath", "IsActive", "LastUpdated", "Notes", "RegisteredBy", "RegisteredDate" },
                values: new object[,]
                {
                    { 1, 0.95f, "[0.1234, -0.5678, 0.9012, -0.3456, 0.7890, -0.1234, 0.5678, -0.9012, 0.3456, -0.7890]", "worker1-id", "face-worker1-001", "/uploads/faces/worker1-20241217080000.jpg", true, new DateTime(2025, 9, 17, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5566), "Face registration for Đinh Văn Thợ (worker1)", "admin", new DateTime(2025, 9, 17, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5463) },
                    { 2, 0.92f, "[0.2345, -0.6789, 0.0123, -0.4567, 0.8901, -0.2345, 0.6789, -0.0123, 0.4567, -0.8901]", "worker2-id", "face-worker2-002", "/uploads/faces/worker2-20241217080100.jpg", true, new DateTime(2025, 9, 22, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5994), "Face registration for Mai Thị Hàn (worker2)", "admin", new DateTime(2025, 9, 22, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5993) },
                    { 3, 0.88f, "[0.3456, -0.7890, 0.1234, -0.5678, 0.9012, -0.3456, 0.7890, -0.1234, 0.5678, -0.9012]", "tech1-id", "face-tech1-003", "/uploads/faces/tech1-20241217080200.jpg", true, new DateTime(2025, 9, 27, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5998), "Face registration for Hoàng Kỹ Thuật (tech1)", "hr-manager1-id", new DateTime(2025, 9, 27, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(5997) },
                    { 4, 0.94f, "[0.4567, -0.8901, 0.2345, -0.6789, 0.0123, -0.4567, 0.8901, -0.2345, 0.6789, -0.0123]", "manager1-id", "face-manager1-004", "/uploads/faces/manager1-20241217080300.jpg", true, new DateTime(2025, 10, 2, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(6000), "Face registration for Nguyễn Quản Lý (manager1)", "admin", new DateTime(2025, 10, 2, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(6000) },
                    { 5, 0.91f, "[0.5678, -0.9012, 0.3456, -0.7890, 0.1234, -0.5678, 0.9012, -0.3456, 0.7890, -0.1234]", "hr-employee1-id", "face-hr1-005", "/uploads/faces/hr1-20241217080400.jpg", true, new DateTime(2025, 10, 7, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(6003), "Face registration for Lê Thị Lan (hr-employee1)", "hr-manager1-id", new DateTime(2025, 10, 7, 18, 26, 37, 84, DateTimeKind.Local).AddTicks(6002) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AttendanceMachineId",
                table: "Attendances",
                column: "AttendanceMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FaceRegistrations_EmployeeId",
                table: "FaceRegistrations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_EmployeeId",
                table: "Attendances",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceMachines_AttendanceMachineId",
                table: "Attendances",
                column: "AttendanceMachineId",
                principalTable: "AttendanceMachines",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceMachines_AttendanceMachineId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "FaceRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_AttendanceMachineId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceMachineId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CheckInDateTime",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CheckInLocation",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CheckOutDateTime",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CheckOutLocation",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "FaceRecognitionConfidence",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Attendances");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b65f066f-4170-4caf-a4cb-92f3da30d11e", "AQAAAAIAAYagAAAAEPix5c3XCwEhnz5sUkLy+O6pmXcK7aKk9auGPujAYFDhYrcS+2adkTVRQJvNP+cGXw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "4e35f11c-6bc2-4754-8383-8db1567f735c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "1f4135a1-23b7-4c89-ad04-603c00b086bc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "afd5fd64-7443-4c18-8e60-0ed518b6abc9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "6b93ba3f-bb21-44dc-b5c6-4562df148c72");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "9ad1bfd5-db5f-4817-a164-41876eca7f28");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "d8eea111-9ab6-4ec5-aa62-324cc3fcfd67");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "7e760697-f72f-4686-9ff1-64abca678690");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "26102491-980f-49dd-942f-e558d404357e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "5e114c72-292b-41e8-8c3e-59eccb139457");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "b8d49aec-342e-4573-a548-37603a2f4123");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "26b345ba-1ccc-4169-9d1a-bcf06fba9ca6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "0884cdb3-3b30-4d5d-9554-dea175361ecc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "2ce633d4-1811-4192-9040-3bde51e3cd7e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "9c458131-32bc-4fcb-ad89-4d64e74c4474");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "92515fd6-d143-4cde-af14-c286a9ed12d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "3c3de628-ebd1-4ce0-a506-faf394b4327f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "649e3495-cc18-4717-817d-42c0c43692eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "a41dc298-f341-41d3-9c09-dcb990e0033f");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ImageCheckIn", "ImageCheckOut" },
                values: new object[] { "/uploads/attendace/worker1-20240912-checkin.jpg", "/uploads/attendace/worker1-20240912-checkin.jpg" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 54, 10, 411, DateTimeKind.Local).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 54, 10, 411, DateTimeKind.Local).AddTicks(5822));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 54, 10, 409, DateTimeKind.Local).AddTicks(8463));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 54, 10, 411, DateTimeKind.Local).AddTicks(5637));
        }
    }
}
