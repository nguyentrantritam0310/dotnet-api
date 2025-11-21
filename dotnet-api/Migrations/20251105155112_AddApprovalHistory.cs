using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApproverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApprovalHistories_AspNetUsers_ApproverID",
                        column: x => x.ApproverID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "adc2095f-c8c7-4983-9e57-a7b120a90066", "AQAAAAIAAYagAAAAENm2647+S3Fgvfo6M+fiS5echbmhoqqZUeBVXk7wE0+PTAPbp8dgwPFOfhub3M1MeQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "6e930f9d-3adb-44c4-811f-64e7501ec202");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "a41b83f6-282a-465f-8d64-e65f262914b6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "0a76efe5-b459-4162-9537-898abd0671cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "5406ac1c-5784-4396-bc83-6759b6bd0847");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "49a37465-0a51-42d3-8339-9658e45b8d83");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "55bbf751-987e-4e8b-b041-c9ee0974b866");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "3bc6fa4c-aa2c-4d6b-b563-43b4569ea001");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "9c2802ec-1861-405c-a559-386d3a740855");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "a669e9f2-28b8-4254-bc18-a9dbfbe7bf5b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "af7405ad-bc3b-444c-8a85-dd91b840316d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "aa164f6e-ec33-44a6-9fef-c1c8d0e20935");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "1f2bb66d-7fa3-4181-89b4-6e76348e5885");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "b5450c6c-16a0-4b62-afe2-697738f61e3a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "568153db-feec-444c-ae70-2dc6d7a9a8c0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "70afef66-27be-4368-9aa2-a9645dcd1def");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "6aea350d-d64b-4723-bfc9-5a896c09664e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "3ea6aa09-8f2e-4e7f-b631-9a7b449e467f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "c406ec3d-a244-45eb-b617-eedaa512e6d1");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 11, 4, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 4, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 4, 22, 51, 11, 566, DateTimeKind.Local).AddTicks(9179), new DateTime(2025, 11, 4, 22, 51, 11, 566, DateTimeKind.Local).AddTicks(9279) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 22, 51, 11, 567, DateTimeKind.Local).AddTicks(6219));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 22, 51, 11, 567, DateTimeKind.Local).AddTicks(6321));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 22, 51, 11, 567, DateTimeKind.Local).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 22, 51, 11, 567, DateTimeKind.Local).AddTicks(6216));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(949), new DateTime(2025, 10, 6, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(4326), new DateTime(2025, 10, 11, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(4317) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 16, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(5617), new DateTime(2025, 10, 16, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(5616) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 21, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(6772), new DateTime(2025, 10, 21, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(6771) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 26, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(8001), new DateTime(2025, 10, 26, 22, 51, 11, 581, DateTimeKind.Local).AddTicks(7999) });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalHistories_ApproverID",
                table: "ApprovalHistories",
                column: "ApproverID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalHistories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef476ead-4c00-4001-8719-40eac061a86b", "AQAAAAIAAYagAAAAEPk1RVuVsDZEJM12w69lJT+prkO8J5OC4N3z9zfn/OjcT6SStEooR5n13oZsg8iQgQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "a2a72f71-d908-475e-bf1d-88bd5fb8670e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "26ddfe60-e7c8-48a1-bc4f-244f9f9601d4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "6d14f5cd-f5ce-44c3-a4aa-879044a44a6a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "bb8c0cd4-fcd4-4848-a332-d5dd35206818");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "65e5f378-6043-49fd-9756-01d11792fa19");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "c7724eef-e126-4204-a1ee-978406d03604");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "19969c24-eb70-4e85-bc86-d1d1701ea43c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "8119df09-d0dc-4c2e-bfd4-dbbbfbd3678c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "7ad4d01e-5cfa-4fce-8a63-35d51076ca75");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "ad34bb9c-01c4-41fa-a44a-018cb4b26b85");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "7badeb85-293a-4649-bd83-e9003b119d76");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "34becab2-5568-4b1b-8d3f-e40a6cd3331f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "5b6b7747-6daa-4bab-ab9c-854c8b631755");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "ae2be5ed-fe1e-4471-9a01-86dd7f099a10");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "64a8e045-4380-4e1e-951e-a42740520a54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "0cc84667-f24f-45a2-8adf-6cff06bd4d03");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "2376c61f-4329-4bff-a6b8-f431d4b5e951");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "23b371b6-154b-4641-a7bd-b50bc347a170");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 3, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 3, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(3916), new DateTime(2025, 11, 3, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(4062) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(9131));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(8431));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(8975));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 5, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(572), new DateTime(2025, 10, 5, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(398) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 10, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(2526), new DateTime(2025, 10, 10, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(2523) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 15, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(3321), new DateTime(2025, 10, 15, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(3320) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 20, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(4048), new DateTime(2025, 10, 20, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(4047) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 25, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(4867), new DateTime(2025, 10, 25, 23, 45, 16, 205, DateTimeKind.Local).AddTicks(4865) });
        }
    }
}
