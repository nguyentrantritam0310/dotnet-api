using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePayrollTimeSheetRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_TimeSheets_ID",
                table: "Payrolls");

            // Use raw SQL to drop and recreate the ID column with IDENTITY
            migrationBuilder.Sql(@"
                -- Drop all foreign key constraints that reference Payrolls table first
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PayrollFeedbacks_Payrolls_PayrollID')
                    ALTER TABLE [PayrollFeedbacks] DROP CONSTRAINT [FK_PayrollFeedbacks_Payrolls_PayrollID];
                
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_OvertimeSheets_Payrolls_PayrollID')
                    ALTER TABLE [OvertimeSheets] DROP CONSTRAINT [FK_OvertimeSheets_Payrolls_PayrollID];
                
                -- Drop the primary key constraint
                ALTER TABLE [Payrolls] DROP CONSTRAINT [PK_Payrolls];
                
                -- Drop the ID column
                ALTER TABLE [Payrolls] DROP COLUMN [ID];
                
                -- Add the ID column back with IDENTITY
                ALTER TABLE [Payrolls] ADD [ID] int IDENTITY(1,1) NOT NULL;
                
                -- Add the primary key constraint back
                ALTER TABLE [Payrolls] ADD CONSTRAINT [PK_Payrolls] PRIMARY KEY ([ID]);
                
                -- Recreate foreign key constraints
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PayrollFeedbacks')
                    ALTER TABLE [PayrollFeedbacks] ADD CONSTRAINT [FK_PayrollFeedbacks_Payrolls_PayrollID] 
                        FOREIGN KEY ([PayrollID]) REFERENCES [Payrolls] ([ID]) ON DELETE NO ACTION;
                
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'OvertimeSheets')
                    ALTER TABLE [OvertimeSheets] ADD CONSTRAINT [FK_OvertimeSheets_Payrolls_PayrollID] 
                        FOREIGN KEY ([PayrollID]) REFERENCES [Payrolls] ([ID]) ON DELETE NO ACTION;
            ");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "31207bfb-e702-4820-b7b0-7ca238eb991c", "AQAAAAIAAYagAAAAEOO2i5Te3GWEc8fL4dlZQYugszAl6MXiMHGPq6t2l/bq8Rwkd6BdTX0PH7TVhSdiHw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "241f955b-e84b-40ca-8976-decd5c36fcf0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "76e842ec-a41b-417d-8299-ace45900d12f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "78149344-dabb-4d15-a2e0-65a10e8389cc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "f850a219-7694-4bc6-b46a-e90f32176c4f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "fc07660b-69f1-45ec-948f-3583616286ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "c7aea5e1-b3d9-4c71-8d81-36622ae4944d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "dac0682f-36ff-4f91-b0d2-e28e9cf4f9e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "dd439793-5b63-4062-8b84-01a83854821b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "e6bb822a-8d81-47dc-be37-1230bc6d881b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "9580771e-7edb-4d94-8f95-59bd509afca8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "340b27dc-b636-4519-9e27-73406fcc6acc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "dc41d7c2-ce98-4093-ac87-ba340f0fce8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "5429aed5-02da-46cc-b407-cbb12ed9046c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "6f88fcd3-97e4-466d-b11c-48cd65eab663");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "cf46e736-f8b5-4293-b2a2-7b4a6ec9bc39");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "9f279da9-bd1c-4d30-92cc-32714c1068af");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "fefda413-77df-47a8-895b-82a0b888b74f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "28d0777b-a6c5-44ba-a8b5-856b9dc7b43d");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 15, 25, 19, 350, DateTimeKind.Local).AddTicks(3664), new DateTime(2025, 10, 20, 15, 25, 19, 350, DateTimeKind.Local).AddTicks(3801) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(610));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 15, 25, 19, 351, DateTimeKind.Local).AddTicks(607));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 21, 15, 25, 19, 352, DateTimeKind.Local).AddTicks(6756), new DateTime(2025, 9, 21, 15, 25, 19, 352, DateTimeKind.Local).AddTicks(6552) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8170), new DateTime(2025, 9, 26, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8150) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8185), new DateTime(2025, 10, 1, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8189), new DateTime(2025, 10, 6, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8189) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8192), new DateTime(2025, 10, 11, 15, 25, 19, 353, DateTimeKind.Local).AddTicks(8192) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Use raw SQL to drop and recreate the ID column without IDENTITY
            migrationBuilder.Sql(@"
                -- Drop the primary key constraint first
                ALTER TABLE [Payrolls] DROP CONSTRAINT [PK_Payrolls];
                
                -- Drop the ID column
                ALTER TABLE [Payrolls] DROP COLUMN [ID];
                
                -- Add the ID column back without IDENTITY
                ALTER TABLE [Payrolls] ADD [ID] int NOT NULL;
                
                -- Add the primary key constraint back
                ALTER TABLE [Payrolls] ADD CONSTRAINT [PK_Payrolls] PRIMARY KEY ([ID]);
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_TimeSheets_ID",
                table: "Payrolls",
                column: "ID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c5b61890-a0ea-4dd9-8929-2a75d8311ffc", "AQAAAAIAAYagAAAAEJ2+7Gv/+h1NdkX9Y/1Cbyhvzv+7F3A0lYiRdggNcAWuLzYSXYK+J8BPTgGnsEcniA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee1-id",
                column: "ConcurrencyStamp",
                value: "39876796-4c84-45cf-a5f8-7acb8915ad67");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee2-id",
                column: "ConcurrencyStamp",
                value: "47b80ad8-2697-4e3f-9cc6-292876609713");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee3-id",
                column: "ConcurrencyStamp",
                value: "a9244846-6393-41c6-bc96-85e83b8f48eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee4-id",
                column: "ConcurrencyStamp",
                value: "3a8eff8f-bf03-4152-a299-dc5bf9dc3474");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-employee5-id",
                column: "ConcurrencyStamp",
                value: "b865145f-91fe-46dd-b164-2d11f9d8f2a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager1-id",
                column: "ConcurrencyStamp",
                value: "b2298465-8cda-4e58-8ab6-2d80826091c8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "hr-manager2-id",
                column: "ConcurrencyStamp",
                value: "41616f14-b1c7-4c21-b388-c401058e16c4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "963b0012-5298-4a18-ae34-386627a805e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "d5376451-9537-4d9e-a5e4-72731aa15ed2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "fc62652f-ecc3-4f4f-b233-6f9ee8c98a6b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "f5a151bf-5bbe-4121-8c02-16c79be46761");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "6369decb-9a8f-4654-b51d-3e02a174b6d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "64065020-04ab-412c-a9e7-7faf82b38cfa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "9efc55cc-d783-48cf-9dff-7cd61c9defe4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "35f68799-5a74-4d9d-816a-a3de9814da37");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "d0a4068c-2f0c-418d-bdf8-69a30fd19d42");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "47f032e1-6d0e-495a-a042-8ee99635310e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "904eb49b-dc39-4d22-91e2-0153a5dfeed0");

            migrationBuilder.UpdateData(
                table: "Attendances",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(872), new DateTime(2025, 10, 20, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(976) });

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6047));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "LV002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT001",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(5513));

            migrationBuilder.UpdateData(
                table: "EmployeeRequests",
                keyColumn: "VoucherCode",
                keyValue: "OT002",
                column: "CreatedAt",
                value: new DateTime(2025, 10, 21, 14, 8, 20, 645, DateTimeKind.Local).AddTicks(6042));

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 21, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(5450), new DateTime(2025, 9, 21, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(5308) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 9, 26, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7124), new DateTime(2025, 9, 26, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7107) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 1, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7128), new DateTime(2025, 10, 1, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7127) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 6, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7134), new DateTime(2025, 10, 6, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7133) });

            migrationBuilder.UpdateData(
                table: "FaceRegistrations",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "LastUpdated", "RegisteredDate" },
                values: new object[] { new DateTime(2025, 10, 11, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7137), new DateTime(2025, 10, 11, 14, 8, 20, 647, DateTimeKind.Local).AddTicks(7136) });

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_TimeSheets_ID",
                table: "Payrolls",
                column: "ID",
                principalTable: "TimeSheets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
