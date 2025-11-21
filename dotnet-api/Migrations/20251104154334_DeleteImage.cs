using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCheckIn",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ImageCheckOut",
                table: "Attendances");

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
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 11, 3, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 3, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 3, 22, 43, 33, 658, DateTimeKind.Local).AddTicks(6492), new DateTime(2025, 11, 3, 22, 43, 33, 658, DateTimeKind.Local).AddTicks(6597) });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageCheckIn",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageCheckOut",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5dfeaf9d-eeeb-43e2-8c6a-64a1d6e2c9de", "AQAAAAIAAYagAAAAEL3QTo1RXLeMDQL52w+1bNxJEzU0YUytQtcPFVI88MDfzBpWkJWsRicmML95q8wigw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "1c709f6c-798d-425a-aad1-ca38a8f72145");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "96cc4508-c8c4-4cca-9896-696bb53711ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "7a4efea9-1d12-4d90-93c4-6c0c9e9ee450");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "84b0df54-8cb7-4dee-a154-ce505a6c1985");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "e32ef62d-516a-4a98-930f-ddf113f1b8f5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "6958e4fe-573a-4293-836c-34e9730b08b2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "365088eb-e671-41c4-b85c-03e84ca5598f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "a52c34a2-96da-411b-b8ad-12c2fb77d808");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "69437743-8ef6-4421-8638-7edc535490cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "de035177-579f-4af0-bb0d-adc07e8801e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "e80831fe-8c4b-4d43-9a6c-01b83f5767a2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "5c9d0a5e-bee0-46a7-b075-6356add34163");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "e7912d3b-ffe9-466e-82a5-ad2c0e14fe8b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "9a29b167-bf76-434d-b04c-3e2cd8e91234");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "fc4c177c-ffdd-4197-8a44-d97ab72e28df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "795cb8ba-7184-41ed-9f42-99e3879669ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "92aa4d21-0d1f-4c31-bc54-1ae9918a8fb3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "832ffd30-8ee4-42bd-8ee0-f739f8b52c73");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 30, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 30, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 30, 0, 28, 19, 206, DateTimeKind.Local).AddTicks(9105), new DateTime(2025, 10, 30, 0, 28, 19, 206, DateTimeKind.Local).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 0, 28, 19, 207, DateTimeKind.Local).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 0, 28, 19, 207, DateTimeKind.Local).AddTicks(4245));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 0, 28, 19, 207, DateTimeKind.Local).AddTicks(3649));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 31, 0, 28, 19, 207, DateTimeKind.Local).AddTicks(4141));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(2476), new DateTime(2025, 10, 1, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(2308) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4195), new DateTime(2025, 10, 6, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4191) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4935), new DateTime(2025, 10, 11, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4934) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 16, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(5635), new DateTime(2025, 10, 16, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 21, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(6453), new DateTime(2025, 10, 21, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(6452) });
        }
    }
}
