using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class themsinhnhat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birthday",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "birthday" },
                values: new object[] { "33d40dbf-9516-4cf8-ad5e-1b56c1991efa", "AQAAAAIAAYagAAAAEG5MIahIC/rDizWw5h3NUYWTnlKFpFFLGq8vN6eIxsfDK2FN/AJzF/O6F/kBJ4nL4w==", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "05e3b704-4376-42a0-b84e-511267b87208", new DateTime(1980, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "f5bf17d6-36a2-466d-bab5-7af2ab5fb336", new DateTime(1980, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "c431c4ea-c536-4674-9b39-f024a5d8a467", new DateTime(1980, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "613dd251-7714-4697-b805-e02b0265f5b8", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "75c7add5-d4f5-429c-a708-819f02cc8fa8", new DateTime(1990, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "22702b3a-aa61-43d4-b52a-e3d0a647dc7c", new DateTime(1990, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "eadef557-f745-46ae-8f43-d72b557a771b", new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "d66387ff-38a2-4348-a45e-5b088ef76e6d", new DateTime(1995, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "3f61ffd0-2646-4306-a0af-435a3001d813", new DateTime(1995, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "0237bb4d-3a07-4cde-b6d5-dff1575f44d1", new DateTime(1995, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                columns: new[] { "ConcurrencyStamp", "birthday" },
                values: new object[] { "e3d42221-179c-48a8-b45b-ea0adce3df81", new DateTime(1995, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthday",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3ec17435-8bff-4d5e-9624-c29f447f4be5", "AQAAAAIAAYagAAAAEINih4vI4KjLCnirCIkfhaVp9tBwfLsYAUk3gam9bWIAmYO+Z27EkNILbwTrPM5Vuw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "5ee1ceb6-69ce-43a0-8953-fb69edc1e584");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "573ad772-788f-499d-bc17-fd6828b1f616");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "29a3d6b4-fc2a-4c41-9006-95e4f9832708");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "ed7c3d9a-7cc8-40ce-b1cc-6c5ed8cef4d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "84a863cb-2f08-497f-975f-b8408322cbcc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "40ca3f6f-55d2-4866-98cb-85073295ab5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "55692b49-74c3-42e7-a8b4-0355ffb02119");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "fb52a96b-9106-4fc9-a950-9aa034e41c06");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "0eaa678a-1c77-4a63-bac0-8883dbe67b94");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "8ef20efd-d951-46e0-8ebd-1c726578444e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "3e72cd45-0c20-4091-b5dc-c5b7cb9bb752");

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 23, 40, 34, 165, DateTimeKind.Local).AddTicks(4403));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 23, 40, 34, 165, DateTimeKind.Local).AddTicks(4410));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 23, 40, 34, 164, DateTimeKind.Local).AddTicks(5137));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 27, 23, 40, 34, 165, DateTimeKind.Local).AddTicks(4383));
        }
    }
}
