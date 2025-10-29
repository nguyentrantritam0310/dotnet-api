using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class Face2810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FaceFeaturesData",
                table: "FaceRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "FaceQualityScore",
                table: "FaceRegistrations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99152a41-9465-42a6-aef8-7cce0393b9d9", "AQAAAAIAAYagAAAAEC/ymAh4c5o6mNxzqIgTM2knANdRVgt+KGdIdCN4WpUX0ZloR9u5hmWp8BFBoKpHOg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "4f1b05b1-b045-449a-9be1-8187b9545368");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "d86e4c2c-12af-4833-820c-3ead113111f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "b13a4e77-6ead-4bda-93b4-1d36fc6e517a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "7dd31296-bb4f-467a-8d4d-92cdc3f04aa1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "d6c37edb-47e0-46b9-b78c-edfd15178fa8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "0aa149fc-827f-4680-82f9-1b76e9befa2b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "f017b888-b55d-4f64-b5ad-291bcd8f6a91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "aa64c760-806f-425c-b3e9-b5048940fbae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "27f84c2d-0e26-46ab-8009-bda20cbdaac4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "a2ad0253-5e71-49f0-b2d8-6a72c48a1a8a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "5fd0bf58-113f-45e4-a13c-d64efaefb03a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "2894efdc-1632-4df9-ad09-2b9d1e6e1bfb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "30c140c0-95b2-4305-9b48-374052c539f8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "59a2a129-8194-4557-9db6-976274d4a487");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "b7755ae5-7a0a-469a-b096-286239c66780");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "6e71ea25-dbcc-463f-86a6-4934b86d9d56");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "c48f1dd3-5003-4959-b0b3-d3c7f19e94ea");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "2a76a5ec-df18-451c-aa02-84f6b23c9b5a");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 27, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 27, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 27, 19, 30, 53, 133, DateTimeKind.Local).AddTicks(5991), new DateTime(2025, 10, 27, 19, 30, 53, 133, DateTimeKind.Local).AddTicks(6093) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 28, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(315));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 28, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(414));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 28, 19, 30, 53, 133, DateTimeKind.Local).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 28, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(311));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "FaceFeaturesData", "FaceQualityScore", "LastUpdated", "RegisteredDate" },
                values: new object[] { "{\"bounds\":{\"x\":0.2,\"y\":0.3,\"width\":0.6,\"height\":0.7},\"landmarks\":[],\"contours\":[],\"headEulerAngles\":{\"x\":2.5,\"y\":-1.2,\"z\":0.8},\"probabilities\":{\"leftEyeOpenProbability\":0.9,\"rightEyeOpenProbability\":0.85,\"smilingProbability\":0.7}}", 88.5f, new DateTime(2025, 9, 28, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5189), new DateTime(2025, 9, 28, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5046) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "FaceFeaturesData", "FaceQualityScore", "LastUpdated", "RegisteredDate" },
                values: new object[] { "{\"bounds\":{\"x\":0.25,\"y\":0.28,\"width\":0.55,\"height\":0.65},\"landmarks\":[],\"contours\":[],\"headEulerAngles\":{\"x\":-1.8,\"y\":2.1,\"z\":-0.5},\"probabilities\":{\"leftEyeOpenProbability\":0.8,\"rightEyeOpenProbability\":0.9,\"smilingProbability\":0.6}}", 85.2f, new DateTime(2025, 10, 3, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5647), new DateTime(2025, 10, 3, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5646) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "FaceFeaturesData", "FaceQualityScore", "LastUpdated", "RegisteredDate" },
                values: new object[] { "{\"bounds\":{\"x\":0.18,\"y\":0.32,\"width\":0.62,\"height\":0.72},\"landmarks\":[],\"contours\":[],\"headEulerAngles\":{\"x\":3.2,\"y\":-2.8,\"z\":1.1},\"probabilities\":{\"leftEyeOpenProbability\":0.75,\"rightEyeOpenProbability\":0.82,\"smilingProbability\":0.8}}", 82.1f, new DateTime(2025, 10, 8, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5651), new DateTime(2025, 10, 8, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "FaceFeaturesData", "FaceQualityScore", "LastUpdated", "RegisteredDate" },
                values: new object[] { "{\"bounds\":{\"x\":0.22,\"y\":0.26,\"width\":0.58,\"height\":0.68},\"landmarks\":[],\"contours\":[],\"headEulerAngles\":{\"x\":-0.9,\"y\":1.5,\"z\":-0.3},\"probabilities\":{\"leftEyeOpenProbability\":0.95,\"rightEyeOpenProbability\":0.92,\"smilingProbability\":0.75}}", 91.3f, new DateTime(2025, 10, 13, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5654), new DateTime(2025, 10, 13, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5653) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "FaceFeaturesData", "FaceQualityScore", "LastUpdated", "RegisteredDate" },
                values: new object[] { "{\"bounds\":{\"x\":0.2,\"y\":0.3,\"width\":0.6,\"height\":0.7},\"landmarks\":[],\"contours\":[],\"headEulerAngles\":{\"x\":1.2,\"y\":-0.8,\"z\":0.4},\"probabilities\":{\"leftEyeOpenProbability\":0.88,\"rightEyeOpenProbability\":0.85,\"smilingProbability\":0.9}}", 89.7f, new DateTime(2025, 10, 18, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5657), new DateTime(2025, 10, 18, 19, 30, 53, 134, DateTimeKind.Local).AddTicks(5656) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceFeaturesData",
                table: "FaceRegistrations");

            migrationBuilder.DropColumn(
                name: "FaceQualityScore",
                table: "FaceRegistrations");

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
        }
    }
}
