using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class shiftassignmentimprove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAssignments_ConstructionTasks_ConstructionTaskID",
                table: "ShiftAssignments");

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
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 11, 3, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(3916), new DateTime(2025, 11, 3, 23, 45, 16, 190, DateTimeKind.Local).AddTicks(4062) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAssignments_ConstructionTasks_ConstructionTaskID",
                table: "ShiftAssignments",
                column: "ConstructionTaskID",
                principalTable: "ConstructionTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftAssignments_ConstructionTasks_ConstructionTaskID",
                table: "ShiftAssignments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a178708-7750-4bcd-aa82-8baf4344e861", "AQAAAAIAAYagAAAAEGhrsgYqW27X2XDXGnL0FGZtjw3XrvUutzN16TCFQHE8jRKlquh3bYamHUdfzDWjow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "779a3908-84f4-4e87-866d-50221c465a0b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "8ea6f0f3-d27d-4d13-8e78-a34a658f1cfa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "ef7fca0b-409f-4fec-8eb9-c77ea8ecb108");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "cf69cee6-bb54-4dc9-ab3f-14af889812e9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "9c7c6dd7-6370-433a-a532-51a516021772");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "0be818e9-720e-496e-b00b-73af38d6c482");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "452849a8-f4db-4ce5-977b-80452b5815ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "9ed08558-e59d-4bbc-8c3e-42408d2cedcd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "2fb60366-4457-48f6-a3df-e294d0c11450");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "3023988d-fbf1-4c1f-af93-d666b332990c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "129374af-e780-49b6-8a6b-feda348b25fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "a85daa69-8909-4a1e-888e-7b8ff43c5f58");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "a9e3ef65-d656-477d-9936-093d14e7c2e2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "3e100f3e-c51a-4eaf-b3a2-0ad9b37dfa8c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "1f22e768-e78f-4e76-a05b-214ea14fef8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "06cfb825-2512-4a49-bc9f-dadd1bdd7413");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "38e6d250-dab9-4abc-89fa-ef77914551ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "dd6b197f-085d-4ce5-b93b-6e4b06a5fcf2");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 11, 3, 22, 43, 33, 658, DateTimeKind.Local).AddTicks(6492), new DateTime(2025, 11, 3, 22, 43, 33, 658, DateTimeKind.Local).AddTicks(6597) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 22, 43, 33, 659, DateTimeKind.Local).AddTicks(957));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 22, 43, 33, 659, DateTimeKind.Local).AddTicks(1061));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 22, 43, 33, 659, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 11, 4, 22, 43, 33, 659, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 5, 22, 43, 33, 672, DateTimeKind.Local).AddTicks(8583), new DateTime(2025, 10, 5, 22, 43, 33, 672, DateTimeKind.Local).AddTicks(8409) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 10, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(259), new DateTime(2025, 10, 10, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(257) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 15, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(1012), new DateTime(2025, 10, 15, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(1011) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 20, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(1707), new DateTime(2025, 10, 20, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(1705) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 25, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(2557), new DateTime(2025, 10, 25, 22, 43, 33, 673, DateTimeKind.Local).AddTicks(2556) });

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftAssignments_ConstructionTasks_ConstructionTaskID",
                table: "ShiftAssignments",
                column: "ConstructionTaskID",
                principalTable: "ConstructionTasks",
                principalColumn: "ID");
        }
    }
}
