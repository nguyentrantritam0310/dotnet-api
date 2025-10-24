using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFaceRecognitionConfidence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceRecognitionConfidence",
                table: "Attendances");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4d7531e-6d00-46e3-a564-709d72c9286a", "AQAAAAIAAYagAAAAEE8Rq8KvI1xVECqbCouo5iQ8lFAEDpSTW3Ww/lTp5q4iKDOmeWnzPN2FXsioeAPeRw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "0c3d21b9-0ba0-49c8-ac0d-df364b091880");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "6b9a5a6d-cb4a-463a-8966-21380ecccc7f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "6d3505d3-aeb2-4e3e-b14d-2afdd35ec8d1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "33f28ae8-c072-484e-825f-d9321a2cb397");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "710dcf30-cdc5-4bbd-b9cb-e2976d015bd2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "2045b93b-b828-456c-b232-ac2ae3fec5bf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "e934fee9-4366-4515-8d29-32adc67b93c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "0b403690-a4fb-4ebb-8e1d-ed9326d7e18f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "69c3ad0a-35ca-494b-b58f-d5c18450b26d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "3459a873-5ddf-418f-847d-b5b218911c97");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "9da9309b-9c3b-45e0-82ae-8a4450eea9bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "4bd83e71-b7cc-47e7-931d-90a8c1c792f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "308d7141-8261-4625-a73b-3635af179e1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "f3faa677-8504-44dc-b521-9813e371eeeb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "e695d12c-09e6-48cb-972b-69ee7565f8c9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "4a3a445f-045a-41d9-b1fb-baa14072ae09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "795d893e-b72f-468b-8ac4-6bd2b4ec8ad4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "40e063c6-3319-4e32-8139-be7c6998b837");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated", "Notes" },
                values: new object[] { new DateTime(2025, 10, 23, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(1494), new DateTime(2025, 10, 23, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(1603), "Full day attendance" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(6203));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(6308));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(5670));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 24, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1375), new DateTime(2025, 9, 24, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1262) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 29, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1799), new DateTime(2025, 9, 29, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1798) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 4, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1803), new DateTime(2025, 10, 4, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1802) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 9, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1806), new DateTime(2025, 10, 9, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1805) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 14, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1808), new DateTime(2025, 10, 14, 18, 58, 21, 922, DateTimeKind.Local).AddTicks(1808) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FaceRecognitionConfidence",
                table: "Attendances",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f56ca123-ac32-4398-b03a-01679159e75f", "AQAAAAIAAYagAAAAEGKvq3UDv7U2mqRG8Xpp9+BLxxGB2q3Ct75+FThQjOpP8d8jrTwA8mhEkOEXbp28Gw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "4dfa62aa-eb26-48a7-b2cd-022ba16ad925");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "be33747c-b5ec-4903-af6c-af12eca02ec8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "9d90bbd0-a209-4189-a6d2-64c482101fd5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "d4062a9a-7c8a-4fff-bc6c-2a1a4ebade10");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "6cd83cb5-ef42-4053-904b-2b9009b34c47");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "4ead78c4-ceaf-41dd-aa09-45af67d2c260");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "a96f2a3c-1887-40ab-a8ba-e124952263bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "6de8adeb-8c34-4cde-bf4c-31afe87ee4f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "2bd84eda-1e94-4702-af8c-cc0ee509a29c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "62ce8f60-10b2-4f9d-8595-54bdd6a2c364");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "3f0cb289-92da-40c2-b5ca-fcf0581d8422");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "1adc39cc-a144-4cf3-b76d-22557001f6ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "3bad9a26-5d03-434e-bdee-4d601f863c57");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "e3d83bca-a937-4888-b449-0d56a0231835");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "2610828f-25d0-4e95-adb4-3f1dd4e83dbe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "68ab4dea-030f-4876-8fe1-9a5e41f334f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "7c397a37-9f22-445f-9c4e-6025153d8708");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "6fab4f52-6362-453e-9599-6069159e6437");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "FaceRecognitionConfidence", "LastUpdated", "Notes" },
                values: new object[] { new DateTime(2025, 10, 23, 0, 21, 26, 913, DateTimeKind.Local).AddTicks(8965), 0.95f, new DateTime(2025, 10, 23, 0, 21, 26, 913, DateTimeKind.Local).AddTicks(9068), "Full day attendance with face recognition" });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3393));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(2750));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(3299));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8055), new DateTime(2025, 9, 24, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(7948) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 29, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8457), new DateTime(2025, 9, 29, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8456) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 4, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8460), new DateTime(2025, 10, 4, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 9, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8463), new DateTime(2025, 10, 9, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8463) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 14, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8466), new DateTime(2025, 10, 14, 0, 21, 26, 914, DateTimeKind.Local).AddTicks(8465) });
        }
    }
}
