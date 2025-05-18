using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class ThemActualWorkload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExportOrders_ConstructionPlanID",
                table: "ExportOrders");

            migrationBuilder.DropColumn(
                name: "ConstructionPlanID",
                table: "ExportOrders");

            migrationBuilder.AddColumn<float>(
                name: "ActualWorkload",
                table: "ConstructionTasks",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                column: "ConcurrencyStamp",
                value: "08e247ed-26e4-416f-80a8-e3663cbbbd8c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "0b3ea52e-218b-403c-a03a-af0c76d22daa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "15868cb2-085c-4e89-a5c9-4e4ed7af6a53");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "ceb07b9f-296b-4eaf-b18c-18cdf48556a4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "13e1e725-d94c-47bf-b3ef-e0a83669618f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "da325b24-c815-4972-8523-c227b1c25351");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "ad6707f7-900f-4e55-a7ee-9f44c6cab949");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "61498494-c17e-43eb-8f11-8119e2688cc0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "06954b68-994e-4288-957b-5a2dfd0b9182");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "0ab9aff1-6b7b-4158-9d94-12d6111f9e20");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "f5376297-7088-473e-8bff-7cb0744b33fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "d94ef863-ea59-4ae1-816b-74390cbf22d2");

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 1,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 2,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 3,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 4,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 5,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 6,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 7,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 8,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 9,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 10,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 11,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 12,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 13,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 14,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 15,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 16,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 17,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 18,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 19,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 20,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 21,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 22,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 23,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 24,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 25,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 26,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 27,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 28,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 29,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 30,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 31,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 32,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 33,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 34,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 35,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 36,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 37,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 38,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 39,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 40,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 41,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 42,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 43,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 44,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 45,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 46,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 47,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 48,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 49,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 50,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 51,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 52,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 53,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 54,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 55,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 56,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 57,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 58,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 59,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 60,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 61,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 62,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 63,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 64,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 65,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 66,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 67,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 68,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 69,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 70,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 71,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 72,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 73,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 74,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 75,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 76,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 77,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 78,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 79,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 80,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 81,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 82,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 83,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 84,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 85,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 86,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 87,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 88,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 89,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 90,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 91,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 92,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 93,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 94,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 95,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 96,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 97,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 98,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 99,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 100,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 101,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 102,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 103,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 104,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 105,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 106,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 107,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 108,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 109,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 110,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 111,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 112,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 113,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 114,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 115,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 116,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 117,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 118,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 119,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 200,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 201,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 202,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 203,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 204,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 205,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 206,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 207,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 208,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 209,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 210,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 211,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 212,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 213,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 214,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 215,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 216,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 217,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 218,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 219,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 220,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 221,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 222,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 223,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 224,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 225,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 226,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 227,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 228,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 229,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 230,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 231,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 232,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 233,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 234,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 235,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 236,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 237,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 238,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 239,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 240,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 241,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 242,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 243,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 244,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 245,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 246,
                column: "ActualWorkload",
                value: null);

            migrationBuilder.UpdateData(
                table: "ConstructionTasks",
                keyColumn: "ID",
                keyValue: 247,
                column: "ActualWorkload",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualWorkload",
                table: "ConstructionTasks");

            migrationBuilder.AddColumn<int>(
                name: "ConstructionPlanID",
                table: "ExportOrders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                column: "ConcurrencyStamp",
                value: "1aa20e01-2c13-4aa6-a88d-01f5cb3db9de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "cc940a6d-5547-435a-87ca-7ebe0fa754d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "f28b037e-50ce-4a70-895e-f7a65979b864");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "be36e66e-73cf-4830-ab48-e29e60295004");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "03dc839a-5d95-4944-9be8-ffa49a279d1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "b526228c-f1ba-43a2-a2d7-94e9dd6fdce4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "292be499-0af6-45e1-a62e-657ac539314a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "ff90b683-631e-4ca5-bb1e-758daa0ef005");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "c46695fb-dad1-402c-87de-c9e62698cd01");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "ee4461b3-b2e9-4ac2-bbcc-7108e783ee75");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "5d2c2e51-e8f3-4bdf-8124-c7125892a5ef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "09def347-5d6d-4432-bd98-abfc3783f994");

            migrationBuilder.UpdateData(
                table: "ExportOrders",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConstructionPlanID",
                value: null);

            migrationBuilder.UpdateData(
                table: "ExportOrders",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConstructionPlanID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ExportOrders_ConstructionPlanID",
                table: "ExportOrders",
                column: "ConstructionPlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders",
                column: "ConstructionPlanID",
                principalTable: "ConstructionPlans",
                principalColumn: "ID");
        }
    }
}
