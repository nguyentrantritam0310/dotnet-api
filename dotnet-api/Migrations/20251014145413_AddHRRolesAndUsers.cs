using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddHRRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5", null, "hr_manager", "HR_MANAGER" },
                    { "6", null, "hr_employee", "HR_EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b65f066f-4170-4caf-a4cb-92f3da30d11e", "AQAAAAIAAYagAAAAEPix5c3XCwEhnz5sUkLy+O6pmXcK7aKk9auGPujAYFDhYrcS+2adkTVRQJvNP+cGXw==" });

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "RoleName" },
                values: new object[,]
                {
                    { 5, "Trưởng phòng Hành chính – Nhân sự" },
                    { 6, "Nhân viên phòng Hành chính - Nhân sự" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "RequiresPasswordChange", "RoleID", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "birthday", "joinDate" },
                values: new object[,]
                {
                    { "hr-employee1-id", 0, "4e35f11c-6bc2-4754-8383-8db1567f735c", "nhanvienhr1@company.com", true, "Lê", "Nữ", "Thị Lan", false, null, "NHANVIENHR1@COMPANY.COM", "NHANVIENHR1@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654323", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "c3d4e5f6-a7b8-4c5d-8e7f-9a0b1c2d3e4f", "Active", false, "nhanvienhr1@company.com", new DateTime(1992, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-employee2-id", 0, "1f4135a1-23b7-4c89-ad04-603c00b086bc", "nhanvienhr2@company.com", true, "Phạm", "Nam", "Văn Đức", false, null, "NHANVIENHR2@COMPANY.COM", "NHANVIENHR2@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654324", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "d4e5f6a7-b8c9-4d5e-8f7a-9b0c1d2e3f4a", "Active", false, "nhanvienhr2@company.com", new DateTime(1990, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-employee3-id", 0, "afd5fd64-7443-4c18-8e60-0ed518b6abc9", "nhanvienhr3@company.com", true, "Hoàng", "Nữ", "Thị Mai", false, null, "NHANVIENHR3@COMPANY.COM", "NHANVIENHR3@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654325", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "e5f6a7b8-c9d0-4e5f-8a7b-9c0d1e2f3a4b", "Active", false, "nhanvienhr3@company.com", new DateTime(1993, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-employee4-id", 0, "6b93ba3f-bb21-44dc-b5c6-4562df148c72", "nhanvienhr4@company.com", true, "Vũ", "Nam", "Văn Tài", false, null, "NHANVIENHR4@COMPANY.COM", "NHANVIENHR4@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654326", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "f6a7b8c9-d0e1-4f5a-8b7c-9d0e1f2a3b4c", "Active", false, "nhanvienhr4@company.com", new DateTime(1988, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-employee5-id", 0, "9ad1bfd5-db5f-4817-a164-41876eca7f28", "nhanvienhr5@company.com", true, "Đặng", "Nữ", "Thị Hương", false, null, "NHANVIENHR5@COMPANY.COM", "NHANVIENHR5@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654327", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6, "a7b8c9d0-e1f2-4a5b-8c7d-9e0f1a2b3c4d", "Active", false, "nhanvienhr5@company.com", new DateTime(1991, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-manager1-id", 0, "d8eea111-9ab6-4ec5-aa62-324cc3fcfd67", "truongphonghr1@company.com", true, "Nguyễn", "Nữ", "Thị Hoa", false, null, "TRUONGPHONGHR1@COMPANY.COM", "TRUONGPHONGHR1@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654321", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, "a1b2c3d4-e5f6-4a5b-8c7d-9e0f1a2b3c4e", "Active", false, "truongphonghr1@company.com", new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "hr-manager2-id", 0, "7e760697-f72f-4686-9ff1-64abca678690", "truongphonghr2@company.com", true, "Trần", "Nam", "Văn Minh", false, null, "TRUONGPHONGHR2@COMPANY.COM", "TRUONGPHONGHR2@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0987654322", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, "b2c3d4e5-f6a7-4b5c-8d7e-9f0a1b2c3d4f", "Active", false, "truongphonghr2@company.com", new DateTime(1982, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6", "hr-employee1-id" },
                    { "6", "hr-employee2-id" },
                    { "6", "hr-employee3-id" },
                    { "6", "hr-employee4-id" },
                    { "6", "hr-employee5-id" },
                    { "5", "hr-manager1-id" },
                    { "5", "hr-manager2-id" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "hr-employee1-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "hr-employee2-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "hr-employee3-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "hr-employee4-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6", "hr-employee5-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5", "hr-manager1-id" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5", "hr-manager2-id" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cf7c9883-87b0-4b10-9b12-16f7f237d57a", "AQAAAAIAAYagAAAAEC/dxOidsbZf8QpVMuO84ejPp7WSJ/9Lt9JEKBFjdu1oP1ZVp7SlpSBWK7Kx1ZiUgg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "f8767681-0d77-4b9b-ae9e-9570e35da19c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "1a39b387-4001-489c-bc1c-ea672e2994e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "67934f5f-56f9-46d0-8bac-e49dab6e714e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "b385eeac-8030-4c66-8fb8-06015daef02b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "eb687d15-85d8-46ce-90da-e1ad5ff7956d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "599a04bd-52fe-48a1-8c1f-0eb5cd894cd9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "5f058db8-46e4-44d1-bb64-b769d3d87410");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "971b7a96-19af-4614-857a-cb74b44d8b0d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "b4e2497f-31e7-4bc8-a608-177646d2eb31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "258d3366-65bd-4834-8286-d928ce78e7be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "676907ac-3adf-4434-905a-fec290db8487");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6599));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 975, DateTimeKind.Local).AddTicks(7988));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 14, 21, 22, 35, 976, DateTimeKind.Local).AddTicks(6438));
        }
    }
}
