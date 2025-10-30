using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pose",
                table: "FaceRegistrations",
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
                columns: new[] { "LastUpdated", "Pose", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(2476), null, new DateTime(2025, 10, 1, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(2308) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "Pose", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4195), null, new DateTime(2025, 10, 6, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4191) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "Pose", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4935), null, new DateTime(2025, 10, 11, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(4934) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "Pose", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 16, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(5635), null, new DateTime(2025, 10, 16, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "Pose", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 21, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(6453), null, new DateTime(2025, 10, 21, 0, 28, 19, 222, DateTimeKind.Local).AddTicks(6452) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pose",
                table: "FaceRegistrations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e59f1deb-a73b-427b-b3e2-d35b4d7e0e37", "AQAAAAIAAYagAAAAEMkA+vM05DWibzjlqfOOqH+PdKlCwa26mohqUdC9FcbE+1LGvmRsAhRAV0dPJKc8mg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "8090cedd-9e27-4585-80f6-71d7ef2cc3eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "8219b848-67c4-4c4d-99ac-865ee4f80f12");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "f9358db6-f53e-479f-86ff-fd56958c2c5b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "09b064ba-bb84-4556-a052-6e011c3af822");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "484a4bcc-8ea7-4693-aae9-f7508a7a963e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "b2e64663-3025-4118-85d0-24753fc41af7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "6f7f3ad5-fae3-4d2c-8a15-3ab506dd78d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "0bf81f2f-b3bb-4c8c-97ee-d8623d992c48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "cc371a0f-07b3-4486-9175-2bb17a62f48e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "fe3081d5-b884-4c29-ad27-14de8eb7385b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "1fe44777-e559-4a3d-9dd8-87d31a515cbc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "59b47999-4458-4251-b992-c2fe674a6f7a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "ec6a067d-8a58-4e78-a451-422634f92fcd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "b94f273d-8d21-4d8a-bfc9-445182fe7747");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "40f1929c-b8eb-4f70-bccc-daf673d3c263");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "cceadff9-2258-4924-bbca-12376be78596");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "516cc918-ba07-4a54-a100-c0906d5dd9f0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "db69dbe3-e889-4036-80a9-2c314b6d7730");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CheckInDateTime", "CheckOutDateTime", "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 28, 8, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 28, 17, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 10, 28, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(2258), new DateTime(2025, 10, 28, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(2365) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(6663));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(6761));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(6139));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 29, 21, 51, 20, 720, DateTimeKind.Local).AddTicks(6658));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 29, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(6583), new DateTime(2025, 9, 29, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(6394) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 4, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(8744), new DateTime(2025, 10, 4, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(8741) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 9, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(9573), new DateTime(2025, 10, 9, 21, 51, 20, 736, DateTimeKind.Local).AddTicks(9572) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 14, 21, 51, 20, 737, DateTimeKind.Local).AddTicks(375), new DateTime(2025, 10, 14, 21, 51, 20, 737, DateTimeKind.Local).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 19, 21, 51, 20, 737, DateTimeKind.Local).AddTicks(1258), new DateTime(2025, 10, 19, 21, 51, 20, 737, DateTimeKind.Local).AddTicks(1258) });
        }
    }
}
