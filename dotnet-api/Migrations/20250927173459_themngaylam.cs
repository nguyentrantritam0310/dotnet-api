using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class themngaylam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "joinDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "joinDate" },
                values: new object[] { "4861b1e5-99bf-4e1a-a767-0d88b1facfbe", "AQAAAAIAAYagAAAAEMcV5mYNIXFdjdQ8NAIvfDWoQNJFA9cA5U8bdyMkxN6e3IBiHY1KeH5hlcPkTl3VeA==", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "84ad64ba-d1e0-4ee0-9e07-7b67a24055eb", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "571ca0ac-762e-4e3e-b54c-590272c84881", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "149bf284-0032-4690-88fc-d7302294cbfb", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "b4423058-6698-46fb-9ce3-dad11e717321", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "a58b412a-62f1-4ae7-b22b-36b00ad1edab", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "a090f057-ff6a-4a91-937a-c8367e6178cb", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "a3b2e296-a5ec-4d5f-941a-a3b924f2978b", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "b605266b-c4ec-4311-be4d-7218b8c2014a", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "a25ed9fa-a070-4a59-b238-65c2a6a47448", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "27fa7c70-fed5-4995-94f3-ab371db1dd92", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "joinDate" },
                values: new object[] { "518adc5f-dc90-49b0-8c46-0c193f7916c9", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8073));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 352, DateTimeKind.Local).AddTicks(9774));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 34, 58, 353, DateTimeKind.Local).AddTicks(8050));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "joinDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "33d40dbf-9516-4cf8-ad5e-1b56c1991efa", "AQAAAAIAAYagAAAAEG5MIahIC/rDizWw5h3NUYWTnlKFpFFLGq8vN6eIxsfDK2FN/AJzF/O6F/kBJ4nL4w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "05e3b704-4376-42a0-b84e-511267b87208");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "f5bf17d6-36a2-466d-bab5-7af2ab5fb336");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "c431c4ea-c536-4674-9b39-f024a5d8a467");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "613dd251-7714-4697-b805-e02b0265f5b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "75c7add5-d4f5-429c-a708-819f02cc8fa8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "22702b3a-aa61-43d4-b52a-e3d0a647dc7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "eadef557-f745-46ae-8f43-d72b557a771b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "d66387ff-38a2-4348-a45e-5b088ef76e6d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "3f61ffd0-2646-4306-a0af-435a3001d813");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "0237bb4d-3a07-4cde-b6d5-dff1575f44d1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "e3d42221-179c-48a8-b45b-ea0adce3df81");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 18, 52, 731, DateTimeKind.Local).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 18, 52, 731, DateTimeKind.Local).AddTicks(7041));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 18, 52, 730, DateTimeKind.Local).AddTicks(9272));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 28, 0, 18, 52, 731, DateTimeKind.Local).AddTicks(7025));
        }
    }
}
