using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class Face : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegisteredBy",
                table: "FaceRegistrations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "FaceRegistrations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "FaceRegistrations",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "FaceRegistrations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FaceId",
                table: "FaceRegistrations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmbeddingData",
                table: "FaceRegistrations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "Confidence",
                table: "FaceRegistrations",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e3e1a82-4278-4bf1-9a94-a4901b5cbcf9", "AQAAAAIAAYagAAAAEGMbvF5NfGQBAvAUA9NxSYMK1m+sdfvWjxg7FLrAF1Dp1jEfxO3DKxqFTr7ZbwRa3w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "ddc81700-58b6-410f-aa0e-2f2411775b8e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "1082b110-241b-403f-90f7-25886e56a7d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "7912a89c-f0fa-4701-a4a3-cb573cf6ae50");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "53eb274d-3a5d-469c-9ecc-d5beeea69983");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "ed6511e2-3534-4e19-883e-da1397fad65c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "9882059a-a438-4b62-b1d9-ecfd3ec18b09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "592be549-3fcb-47d8-a92f-ac6c5c29e42b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "ed5ac3c7-cc12-405f-9203-435fb14bdecd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "480321ef-ae00-46f0-ab71-0be4fe2cff42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "a978a77f-863c-4869-aadb-d558c24fab5c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "deb3e1f2-0b0b-49a9-bdc7-3ab417b8ed43");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "5f85cd07-4bea-4e17-a6fe-8e9ae6669de4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "ca82e38c-ad94-4b56-8d16-a9a62c146e91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "9aa89a41-cc1f-4e9a-b84b-cc7780d7c233");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "c5e0ff32-b8a8-401e-8753-d4633d48d2d9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "db624050-e45e-4e29-940d-fb63a3d18206");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "f7b83b9a-45a5-4e43-8981-a6311d24ef46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "c9f0effd-b9fb-40b3-8ed9-2fc07decbc55");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 25, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 25, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 25, 21, 24, 44, 423, DateTimeKind.Local).AddTicks(8069), new DateTime(2025, 10, 25, 21, 24, 44, 423, DateTimeKind.Local).AddTicks(8176) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(2596));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(2721));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(2059));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(2593));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7545), new DateTime(2025, 9, 26, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7435) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7977), new DateTime(2025, 10, 1, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7976) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7981), new DateTime(2025, 10, 6, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7983), new DateTime(2025, 10, 11, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7983) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 16, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7986), new DateTime(2025, 10, 16, 21, 24, 44, 424, DateTimeKind.Local).AddTicks(7985) });

            migrationBuilder.CreateIndex(
                name: "IX_FaceRegistrations_FaceId",
                table: "FaceRegistrations",
                column: "FaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaceRegistrations_IsActive",
                table: "FaceRegistrations",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_FaceRegistrations_RegisteredDate",
                table: "FaceRegistrations",
                column: "RegisteredDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FaceRegistrations_FaceId",
                table: "FaceRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_FaceRegistrations_IsActive",
                table: "FaceRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_FaceRegistrations_RegisteredDate",
                table: "FaceRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredBy",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "FaceRegistrations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FaceId",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "EmbeddingData",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<float>(
                name: "Confidence",
                table: "FaceRegistrations",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

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
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 23, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 23, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 23, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(1494), new DateTime(2025, 10, 23, 18, 58, 21, 921, DateTimeKind.Local).AddTicks(1603) });

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
    }
}
