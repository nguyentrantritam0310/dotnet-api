using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class FixExportOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionPlanID",
                table: "ExportOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ConstructionItemID",
                table: "ExportOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "ConstructionItemID", "ConstructionPlanID" },
                values: new object[] { 1, null });

            migrationBuilder.UpdateData(
                table: "ExportOrders",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "ConstructionItemID", "ConstructionPlanID" },
                values: new object[] { 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_ExportOrders_ConstructionItemID",
                table: "ExportOrders",
                column: "ConstructionItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportOrders_ConstructionItems_ConstructionItemID",
                table: "ExportOrders",
                column: "ConstructionItemID",
                principalTable: "ConstructionItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders",
                column: "ConstructionPlanID",
                principalTable: "ConstructionPlans",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportOrders_ConstructionItems_ConstructionItemID",
                table: "ExportOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExportOrders_ConstructionItemID",
                table: "ExportOrders");

            migrationBuilder.DropColumn(
                name: "ConstructionItemID",
                table: "ExportOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ConstructionPlanID",
                table: "ExportOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-id",
                column: "ConcurrencyStamp",
                value: "4eb2f307-1017-4f57-8842-836d60dcfc5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager1-id",
                column: "ConcurrencyStamp",
                value: "b5e88aec-7d75-4bca-87f0-05aec2338226");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager2-id",
                column: "ConcurrencyStamp",
                value: "f4a27c44-6140-4b4a-88c5-ff14c090d8eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "manager3-id",
                column: "ConcurrencyStamp",
                value: "6aa84b27-89dd-4b93-8e22-164dca078c68");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech1-id",
                column: "ConcurrencyStamp",
                value: "2eebf210-9fb1-477a-bc6d-8d502f164373");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech2-id",
                column: "ConcurrencyStamp",
                value: "61e7754f-15c5-4a63-81d4-8c808871110e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "tech3-id",
                column: "ConcurrencyStamp",
                value: "be7dbaeb-a2e3-43de-9e4c-ef812a0f3f18");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker1-id",
                column: "ConcurrencyStamp",
                value: "6081b318-93a7-4460-97a9-a14716cd12d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker2-id",
                column: "ConcurrencyStamp",
                value: "645213ab-e71c-49d0-ac4a-ab16caf3e477");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker3-id",
                column: "ConcurrencyStamp",
                value: "666168cd-42aa-48e6-827a-a5170dc7ea9d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker4-id",
                column: "ConcurrencyStamp",
                value: "b88ca5fa-7bd9-45f8-9020-112230e658db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "worker5-id",
                column: "ConcurrencyStamp",
                value: "572c92fd-ea8d-4251-bd61-b1cdf1b24213");

            migrationBuilder.UpdateData(
                table: "ExportOrders",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConstructionPlanID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ExportOrders",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConstructionPlanID",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportOrders_ConstructionPlans_ConstructionPlanID",
                table: "ExportOrders",
                column: "ConstructionPlanID",
                principalTable: "ConstructionPlans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
