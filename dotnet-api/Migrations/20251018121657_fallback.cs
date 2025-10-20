using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class fallback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Roles");

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
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 17, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(1679), new DateTime(2025, 10, 17, 19, 16, 55, 558, DateTimeKind.Local).AddTicks(1813) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Page = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "46a1e734-9787-4d47-abd0-77dbc67acd67", "AQAAAAIAAYagAAAAEDc4I3Xf4xgCiFCNQ5i7BOEv7wP7cYSw4/EYoyjfc7r/lY6dmEP97PCW5OadiChckg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "96e4d716-2e30-40e5-a708-56c94a4c5031");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "d020ce1f-cc86-4d5f-bace-a2cdf6fc0f99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "ed382fca-2b0a-4dcb-adfd-3f4a4902eb78");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "6f75c3b3-ff24-4569-9864-2929d2c3ba82");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "6d0cc5aa-5c6d-4077-bde9-08f3017e557c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "9effdb71-394b-4722-bc9b-825715161c98");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "9a0d502f-c436-40a6-80ea-28cedcf22b33");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "94ec5021-87c2-4d68-be8d-d62dab7dd3ff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "aad7a87d-369a-4227-b5cc-534149237bce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "99839d03-3767-4f62-b5c9-bbb63a2b3ad3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "112fd5b1-bbd8-4a0d-b14f-4ea03ca0c790");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "7ddc3ecb-068a-469c-80ef-75e3ee13d6db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "c1971292-1c80-43fb-8096-1af9ce87bb6d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "eade7bc0-5265-40a4-abd8-1b03f32fc005");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "8eb99309-117b-4cdb-9734-6f31977eed3c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "03e9146a-b34d-4e15-b82f-87fe75d24e61");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "ef496408-6722-4370-8158-95bc1d66fe68");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "4b14f733-be97-4de4-b6b8-c7b1b305b7bb");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 17, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(3033), new DateTime(2025, 10, 17, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(3138) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(7404));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(6866));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 18, 19, 1, 3, 399, DateTimeKind.Local).AddTicks(7387));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 18, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(7729), new DateTime(2025, 9, 18, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(7621) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 23, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8140), new DateTime(2025, 9, 23, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8139) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 28, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8143), new DateTime(2025, 9, 28, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8142) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 3, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8146), new DateTime(2025, 10, 3, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8145) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 8, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8149), new DateTime(2025, 10, 8, 19, 1, 3, 400, DateTimeKind.Local).AddTicks(8148) });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "CreatedAt", "Description", "IsActive", "Page", "PermissionType", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3226), "Có thể truy cập trang/chức năng", true, "leave", "functional", null },
                    { 2, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3535), "Có thể tạo mới dữ liệu", true, "leave", "functional", null },
                    { 3, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3550), "Có thể chỉnh sửa dữ liệu", true, "leave", "functional", null },
                    { 4, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3562), "Có thể xóa dữ liệu", true, "leave", "functional", null },
                    { 5, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3574), "Có thể truy cập trang/chức năng", true, "attendance", "functional", null },
                    { 6, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3589), "Có thể tạo mới dữ liệu", true, "attendance", "functional", null },
                    { 7, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3642), "Có thể chỉnh sửa dữ liệu", true, "attendance", "functional", null },
                    { 8, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3654), "Có thể xóa dữ liệu", true, "attendance", "functional", null },
                    { 9, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3666), "Có thể truy cập trang/chức năng", true, "payroll", "functional", null },
                    { 10, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3679), "Có thể tạo mới dữ liệu", true, "payroll", "functional", null },
                    { 11, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3689), "Có thể chỉnh sửa dữ liệu", true, "payroll", "functional", null },
                    { 12, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3700), "Có thể xóa dữ liệu", true, "payroll", "functional", null },
                    { 13, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3710), "Có thể truy cập trang/chức năng", true, "employee", "functional", null },
                    { 14, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3721), "Có thể tạo mới dữ liệu", true, "employee", "functional", null },
                    { 15, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3731), "Có thể chỉnh sửa dữ liệu", true, "employee", "functional", null },
                    { 16, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3741), "Có thể xóa dữ liệu", true, "employee", "functional", null },
                    { 17, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3751), "Có thể truy cập trang/chức năng", true, "construction", "functional", null },
                    { 18, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3763), "Có thể tạo mới dữ liệu", true, "construction", "functional", null },
                    { 19, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3773), "Có thể chỉnh sửa dữ liệu", true, "construction", "functional", null },
                    { 20, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3784), "Có thể xóa dữ liệu", true, "construction", "functional", null },
                    { 21, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3794), "Có thể truy cập trang/chức năng", true, "material", "functional", null },
                    { 22, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3805), "Có thể tạo mới dữ liệu", true, "material", "functional", null },
                    { 23, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3814), "Có thể chỉnh sửa dữ liệu", true, "material", "functional", null },
                    { 24, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3824), "Có thể xóa dữ liệu", true, "material", "functional", null },
                    { 25, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3834), "Có thể truy cập trang/chức năng", true, "report", "functional", null },
                    { 26, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3844), "Có thể tạo mới dữ liệu", true, "report", "functional", null },
                    { 27, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3854), "Có thể chỉnh sửa dữ liệu", true, "report", "functional", null },
                    { 28, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3863), "Có thể xóa dữ liệu", true, "report", "functional", null },
                    { 29, "view", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3900), "Có thể truy cập trang/chức năng", true, "system", "functional", null },
                    { 30, "create", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3912), "Có thể tạo mới dữ liệu", true, "system", "functional", null },
                    { 31, "edit", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3922), "Có thể chỉnh sửa dữ liệu", true, "system", "functional", null },
                    { 32, "delete", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3933), "Có thể xóa dữ liệu", true, "system", "functional", null },
                    { 33, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3947), "Có thể xem dữ liệu của tất cả người dùng", true, "leave", "data_access", null },
                    { 34, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3958), "Chỉ xem được dữ liệu của chính mình", true, "leave", "data_access", null },
                    { 35, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3969), "Có thể chọn role cụ thể để xem dữ liệu", true, "leave", "data_access", null },
                    { 36, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3979), "Có thể xem dữ liệu của tất cả người dùng", true, "attendance", "data_access", null },
                    { 37, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(3991), "Chỉ xem được dữ liệu của chính mình", true, "attendance", "data_access", null },
                    { 38, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4001), "Có thể chọn role cụ thể để xem dữ liệu", true, "attendance", "data_access", null },
                    { 39, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4011), "Có thể xem dữ liệu của tất cả người dùng", true, "payroll", "data_access", null },
                    { 40, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4022), "Chỉ xem được dữ liệu của chính mình", true, "payroll", "data_access", null },
                    { 41, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4032), "Có thể chọn role cụ thể để xem dữ liệu", true, "payroll", "data_access", null },
                    { 42, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4043), "Có thể xem dữ liệu của tất cả người dùng", true, "employee", "data_access", null },
                    { 43, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4053), "Chỉ xem được dữ liệu của chính mình", true, "employee", "data_access", null },
                    { 44, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4142), "Có thể chọn role cụ thể để xem dữ liệu", true, "employee", "data_access", null },
                    { 45, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4175), "Có thể xem dữ liệu của tất cả người dùng", true, "construction", "data_access", null },
                    { 46, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4186), "Chỉ xem được dữ liệu của chính mình", true, "construction", "data_access", null },
                    { 47, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4197), "Có thể chọn role cụ thể để xem dữ liệu", true, "construction", "data_access", null },
                    { 48, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4209), "Có thể xem dữ liệu của tất cả người dùng", true, "material", "data_access", null },
                    { 49, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4220), "Chỉ xem được dữ liệu của chính mình", true, "material", "data_access", null },
                    { 50, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4230), "Có thể chọn role cụ thể để xem dữ liệu", true, "material", "data_access", null },
                    { 51, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4276), "Có thể xem dữ liệu của tất cả người dùng", true, "report", "data_access", null },
                    { 52, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4314), "Chỉ xem được dữ liệu của chính mình", true, "report", "data_access", null },
                    { 53, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4326), "Có thể chọn role cụ thể để xem dữ liệu", true, "report", "data_access", null },
                    { 54, "view_all", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4337), "Có thể xem dữ liệu của tất cả người dùng", true, "system", "data_access", null },
                    { 55, "view_own", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4348), "Chỉ xem được dữ liệu của chính mình", true, "system", "data_access", null },
                    { 56, "view_by_role", new DateTime(2025, 10, 18, 12, 1, 3, 401, DateTimeKind.Utc).AddTicks(4359), "Có thể chọn role cụ thể để xem dữ liệu", true, "system", "data_access", null }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 6,
                column: "Description",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsActive",
                table: "Permissions",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Page_Action",
                table: "Permissions",
                columns: new[] { "Page", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionType",
                table: "Permissions",
                column: "PermissionType");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_IsGranted",
                table: "RolePermissions",
                column: "IsGranted");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);
        }
    }
}
