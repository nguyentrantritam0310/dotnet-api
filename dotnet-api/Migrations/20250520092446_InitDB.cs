using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_api.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructionTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitofMeasurements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitofMeasurements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypeItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypeItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Constructions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructionTypeID = table.Column<int>(type: "int", nullable: false),
                    ConstructionStatusID = table.Column<int>(type: "int", nullable: false),
                    ConstructionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TotalArea = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DesignBlueprint = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Constructions_ConstructionStatuses_ConstructionStatusID",
                        column: x => x.ConstructionStatusID,
                        principalTable: "ConstructionStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_ConstructionTypes_ConstructionTypeID",
                        column: x => x.ConstructionTypeID,
                        principalTable: "ConstructionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitOfMeasurementID = table.Column<int>(type: "int", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaterialTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_MaterialTypeID",
                        column: x => x.MaterialTypeID,
                        principalTable: "MaterialTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_UnitofMeasurements_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalTable: "UnitofMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkAttributes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAttributeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitOfMeasurementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAttributes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkAttributes_UnitofMeasurements_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalTable: "UnitofMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSubTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSubTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSubTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkSubTypes_WorkTypeItems_WorkTypeID",
                        column: x => x.WorkTypeID,
                        principalTable: "WorkTypeItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImportOrders_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConstructionID = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Constructions_ConstructionID",
                        column: x => x.ConstructionID,
                        principalTable: "Constructions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSubTypeVariants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSubTypeID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSubTypeVariants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkSubTypeVariants_WorkSubTypes_WorkSubTypeID",
                        column: x => x.WorkSubTypeID,
                        principalTable: "WorkSubTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportOrderEmployees",
                columns: table => new
                {
                    ImportOrderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportOrderEmployees", x => new { x.ImportOrderId, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_ImportOrderEmployees_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportOrderEmployees_ImportOrders_ImportOrderId",
                        column: x => x.ImportOrderId,
                        principalTable: "ImportOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportAttachments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAttachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportAttachments_Reports_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Reports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportStatusLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStatusLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportStatusLogs_Reports_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Reports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructionStatusID = table.Column<int>(type: "int", nullable: false),
                    WorkSubTypeVariantID = table.Column<int>(type: "int", nullable: false),
                    ConstructionID = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurementID = table.Column<int>(type: "int", nullable: false),
                    ConstructionItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalVolume = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionItems_ConstructionStatuses_ConstructionStatusID",
                        column: x => x.ConstructionStatusID,
                        principalTable: "ConstructionStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionItems_Constructions_ConstructionID",
                        column: x => x.ConstructionID,
                        principalTable: "Constructions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionItems_UnitofMeasurements_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalTable: "UnitofMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionItems_WorkSubTypeVariants_WorkSubTypeVariantID",
                        column: x => x.WorkSubTypeVariantID,
                        principalTable: "WorkSubTypeVariants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionTemplateItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSubTypeVarientID = table.Column<int>(type: "int", nullable: false),
                    ConstructionTypeID = table.Column<int>(type: "int", nullable: false),
                    ConstructionTemplateItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTemplateItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionTemplateItems_ConstructionTypes_ConstructionTypeID",
                        column: x => x.ConstructionTypeID,
                        principalTable: "ConstructionTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConstructionTemplateItems_WorkSubTypeVariants_WorkSubTypeVarientID",
                        column: x => x.WorkSubTypeVarientID,
                        principalTable: "WorkSubTypeVariants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialNorms",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    WorkSubTypeVariantID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialNorms", x => new { x.MaterialID, x.WorkSubTypeVariantID });
                    table.ForeignKey(
                        name: "FK_MaterialNorms_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialNorms_WorkSubTypeVariants_WorkSubTypeVariantID",
                        column: x => x.WorkSubTypeVariantID,
                        principalTable: "WorkSubTypeVariants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSubTypeVariant_WorkAttributes",
                columns: table => new
                {
                    WorkSubTypeVariantID = table.Column<int>(type: "int", nullable: false),
                    WorkAttributeID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSubTypeVariant_WorkAttributes", x => new { x.WorkSubTypeVariantID, x.WorkAttributeID });
                    table.ForeignKey(
                        name: "FK_WorkSubTypeVariant_WorkAttributes_WorkAttributes_WorkAttributeID",
                        column: x => x.WorkAttributeID,
                        principalTable: "WorkAttributes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkSubTypeVariant_WorkAttributes_WorkSubTypeVariants_WorkSubTypeVariantID",
                        column: x => x.WorkSubTypeVariantID,
                        principalTable: "WorkSubTypeVariants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionPlans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructionID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConstructionStatusID = table.Column<int>(type: "int", nullable: false),
                    ConstructionItemID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionPlans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionPlans_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConstructionPlans_ConstructionItems_ConstructionItemID",
                        column: x => x.ConstructionItemID,
                        principalTable: "ConstructionItems",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ConstructionPlans_ConstructionStatuses_ConstructionStatusID",
                        column: x => x.ConstructionStatusID,
                        principalTable: "ConstructionStatuses",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ExportOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConstructionItemID = table.Column<int>(type: "int", nullable: false),
                    ExportDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExportOrders_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportOrders_ConstructionItems_ConstructionItemID",
                        column: x => x.ConstructionItemID,
                        principalTable: "ConstructionItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPlans",
                columns: table => new
                {
                    ImportOrderID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    ConstructionItemID = table.Column<int>(type: "int", nullable: false),
                    ImportQuantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPlans", x => new { x.ImportOrderID, x.MaterialID, x.ConstructionItemID });
                    table.ForeignKey(
                        name: "FK_MaterialPlans_ConstructionItems_ConstructionItemID",
                        column: x => x.ConstructionItemID,
                        principalTable: "ConstructionItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialPlans_ImportOrders_ImportOrderID",
                        column: x => x.ImportOrderID,
                        principalTable: "ImportOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialPlans_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionTasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConstructionStatusID = table.Column<int>(type: "int", nullable: false),
                    ConstructionPlanID = table.Column<int>(type: "int", nullable: false),
                    Workload = table.Column<float>(type: "real", nullable: false),
                    ActualWorkload = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConstructionTasks_ConstructionPlans_ConstructionPlanID",
                        column: x => x.ConstructionPlanID,
                        principalTable: "ConstructionPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConstructionTasks_ConstructionStatuses_ConstructionStatusID",
                        column: x => x.ConstructionStatusID,
                        principalTable: "ConstructionStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Material_ExportOrders",
                columns: table => new
                {
                    ExportOrderID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material_ExportOrders", x => new { x.ExportOrderID, x.MaterialID });
                    table.ForeignKey(
                        name: "FK_Material_ExportOrders_ExportOrders_ExportOrderID",
                        column: x => x.ExportOrderID,
                        principalTable: "ExportOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Material_ExportOrders_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConstructionTaskID = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.EmployeeID, x.ConstructionTaskID, x.AttendanceDate });
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_ConstructionTasks_ConstructionTaskID",
                        column: x => x.ConstructionTaskID,
                        principalTable: "ConstructionTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "technician", "TECHNICIAN" },
                    { "2", null, "manager", "MANAGER" },
                    { "3", null, "director", "DIRECTOR" },
                    { "4", null, "employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "ConstructionStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "InProgress" },
                    { 3, "Paused" },
                    { 4, "Completed" },
                    { 5, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "ConstructionTypes",
                columns: new[] { "ID", "ConstructionTypeName" },
                values: new object[,]
                {
                    { 1, "House" },
                    { 2, "RoadBridge" },
                    { 3, "Industrial" },
                    { 4, "Irrigation" }
                });

            migrationBuilder.InsertData(
                table: "ImportOrders",
                columns: new[] { "ID", "ApplicationUserId", "ImportDate", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved" },
                    { 2, null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" }
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "ID", "MaterialTypeName" },
                values: new object[,]
                {
                    { 1, "BasicBuildingMaterial" },
                    { 2, "FinishingMaterial" },
                    { 3, "ElectricalWaterSystem" },
                    { 4, "MechanicalStructure" },
                    { 5, "SupportingMaterial" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "RoleName" },
                values: new object[,]
                {
                    { 1, "Nhân viên kỹ thuật" },
                    { 2, "Chỉ huy công trình" },
                    { 3, "Giám đốc" },
                    { 4, "Thợ" }
                });

            migrationBuilder.InsertData(
                table: "UnitofMeasurements",
                columns: new[] { "ID", "Category", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { 1, "Chiều dài", "m", "mét" },
                    { 2, "Chiều dài", "cm", "centimet" },
                    { 3, "Chiều dài", "km", "kilomet" },
                    { 4, "Khối lượng", "kg", "kilogram" },
                    { 5, "Khối lượng", "t", "tấn" },
                    { 6, "Thể tích", "m³", "mét khối" },
                    { 7, "Thể tích", "l", "lít" },
                    { 8, "Diện tích", "m²", "mét vuông" },
                    { 9, "Diện tích", "ha", "hecta" },
                    { 10, "Vật liệu", "bao", "bao" },
                    { 11, "Vật liệu", "thanh", "thanh" },
                    { 12, "Vật liệu", "viên", "viên" },
                    { 13, "Vật liệu", "cái", "cái" },
                    { 14, "Vật liệu", "bộ", "bộ" }
                });

            migrationBuilder.InsertData(
                table: "WorkTypeItems",
                columns: new[] { "ID", "WorkTypeName" },
                values: new object[,]
                {
                    { 1, "Công tác chuẩn bị mặt bằng" },
                    { 2, "Công tác đổ bê tông" },
                    { 3, "Công tác xây" },
                    { 4, "Công tác điện nước" },
                    { 5, "Công tác hoàn thiện" },
                    { 6, "Công tác lắp đặt thiết bị" },
                    { 7, "Công tác cảnh quan" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "RoleID", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin-id", 0, "9b49fe6a-c98e-4ecc-9ae2-98b982ece97d", "giamdoc@company.com", true, "Phạm", "Văn Đốc", false, null, "GIAMDOC@COMPANY.COM", "GIAMDOC@COMPANY.COM", "AQAAAAIAAYagAAAAEMdV6GUkfs6qwgt02YnxYDhvTinyv50xpvMUpXyuO9m3sGtqIVUTHtZPUp1rJiRVow==", "0901234567", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "427bafc5-bab1-42e5-a5d2-5974daf31890", "Active", false, "giamdoc@company.com" },
                    { "manager1-id", 0, "99973de2-2483-4312-8aa7-b2ffadaa232e", "chihuy1@company.com", true, "Nguyễn", "Chỉ Huy", false, null, "CHIHUY1@COMPANY.COM", "CHIHUY1@COMPANY.COM", "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==", "0912345678", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "227cd1d8-ed74-4f96-9851-ee15b48f8cf2", "Active", false, "chihuy1@company.com" },
                    { "manager2-id", 0, "cda1d267-00de-429c-8b6f-e0d724715104", "chihuy2@company.com", true, "Trần", "Công Trình", false, null, "CHIHUY2@COMPANY.COM", "CHIHUY2@COMPANY.COM", "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==", "0923456789", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "f6ab69f9-8a13-4414-81bd-77603126dc4d", "Active", false, "chihuy2@company.com" },
                    { "manager3-id", 0, "e0f393c8-f2ee-48be-b86f-a4c48dfe2ee1", "chihuy3@company.com", true, "Lê", "Xây Dựng", false, null, "CHIHUY3@COMPANY.COM", "CHIHUY3@COMPANY.COM", "AQAAAAIAAYagAAAAELES7SRaXmuGGtS6MEV0kzUq5SDOWE6ecydmGrGSbAOdCl60MK87guvf2UERMAi9zg==", "0934567890", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "7a7a5d4b-8ebd-4d56-b8b3-fdf964a80a4e", "Active", false, "chihuy3@company.com" },
                    { "tech1-id", 0, "c3ed808c-036f-4f23-8710-cbf6ad3a3943", "kythuat1@company.com", true, "Hoàng", "Kỹ Thuật", false, null, "KYTHUAT1@COMPANY.COM", "KYTHUAT1@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0945678901", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "6ffe7424-9000-41e1-871b-e744671266a6", "Active", false, "kythuat1@company.com" },
                    { "tech2-id", 0, "8cde40de-21e2-4f76-80af-fd9df353605c", "kythuat2@company.com", true, "Phan", "Thiết Kế", false, null, "KYTHUAT2@COMPANY.COM", "KYTHUAT2@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0956789012", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "18d4d1bc-78ea-445f-a362-d544e9ea1a9d", "Active", false, "kythuat2@company.com" },
                    { "tech3-id", 0, "f811d542-31a6-4018-8a2c-0a86f3cb3217", "kythuat3@company.com", true, "Vũ", "Vận Hành", false, null, "KYTHUAT3@COMPANY.COM", "KYTHUAT3@COMPANY.COM", "AQAAAAIAAYagAAAAELgmRFJ1LcM0Ym3M8AmCA0oo9QcjPmFIU3ZlIgl+R6NZlSbp+BV9J7VO0l6isUvY1w==", "0967890123", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "5f694007-36b9-4dbe-8a38-196c05e82cb2", "Active", false, "kythuat3@company.com" },
                    { "worker1-id", 0, "495f9dda-00ab-424d-abe5-e159e6ec6f81", "tho1@company.com", true, "Đinh", "Văn Thợ", false, null, "THO1@COMPANY.COM", "THO1@COMPANY.COM", null, "0978901234", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "2180abeb-5218-4df0-b695-2be63b8b8cd1", "Active", false, "tho1@company.com" },
                    { "worker2-id", 0, "d96c1224-d714-4ee9-bcea-b4ef0cf809e7", "tho2@company.com", true, "Mai", "Thị Hàn", false, null, "THO2@COMPANY.COM", "THO2@COMPANY.COM", null, "0989012345", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "eba6051e-c6ac-4847-baca-1f886c385128", "Active", false, "tho2@company.com" },
                    { "worker3-id", 0, "6e639e64-62b3-41e5-80ef-e90fd781ac65", "tho3@company.com", true, "Lý", "Văn Xây", false, null, "THO3@COMPANY.COM", "THO3@COMPANY.COM", null, "0990123456", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "4933e54c-c4fd-4875-9508-edc952ae52e3", "Active", false, "tho3@company.com" },
                    { "worker4-id", 0, "4f1122ef-230a-4b98-bcd1-14c6fd70fb77", "tho4@company.com", true, "Trịnh", "Công Mộc", false, null, "THO4@COMPANY.COM", "THO4@COMPANY.COM", null, "0911223344", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "f9757b33-c60a-4995-b62f-d0c899b42c68", "Active", false, "tho4@company.com" },
                    { "worker5-id", 0, "80ced72a-0350-4673-9aad-75b7eece1d4c", "tho5@company.com", true, "Võ", "Thị Sơn", false, null, "THO5@COMPANY.COM", "THO5@COMPANY.COM", null, "0922334455", null, false, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "a1b2c3d4-e5f6-4a5b-8c7d-9e0f1a2b3c4d", "Active", false, "tho5@company.com" }
                });

            migrationBuilder.InsertData(
                table: "Constructions",
                columns: new[] { "ID", "ActualCompletionDate", "ConstructionName", "ConstructionStatusID", "ConstructionTypeID", "DesignBlueprint", "ExpectedCompletionDate", "Location", "StartDate", "TotalArea" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khu chung cư An Hòa Garden", 4, 1, "Design_AnHoa.pdf", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Số 10, đường Nguyễn Văn Cừ, thị trấn Tuy Phước, huyện Tuy Phước, Bình Định", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500.5f },
                    { 2, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhà ở dân dụng Phù Mỹ", 4, 1, "Design_PhuyMy.pdf", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khu phố 3, thị trấn Phù Mỹ, huyện Phù Mỹ, Bình Định", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f },
                    { 3, null, "Nhà ở dân dụng An Nhơn", 1, 1, "Design_BinhDinh.pdf", new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Số 50, đường Nguyễn Du, thị xã An Nhơn, Bình Định", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800f },
                    { 4, null, "Cầu An Hòa", 2, 2, "Design_AnHoa.pdf", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Km 12, Quốc lộ 1A, huyện Tuy Phước, tỉnh Bình Định", new DateTime(2021, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500.5f },
                    { 5, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đường tránh QL1A - Phù Mỹ", 4, 2, "Design_PhuyMy.pdf", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đoạn từ Km 35 đến Km 50, Quốc lộ 1A, huyện Phù Mỹ, Bình Định", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f },
                    { 6, null, "Nhà máy sản xuất thép An Phát", 3, 3, "Design_NhaMayThep.pdf", new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Khu công nghiệp Long Mỹ, xã Long Mỹ, huyện Phù Cát, Bình Định", new DateTime(2020, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000f },
                    { 7, null, "Đập thủy lợi Phú Tài", 5, 4, "Design_ThuyLoiPhuTai.pdf", new DateTime(2022, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Số 20, xã Phú Tài, thành phố Quy Nhơn, Bình Định", new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000f }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "ID", "MaterialName", "MaterialTypeID", "Note", "Specification", "Status", "StockQuantity", "UnitOfMeasurementID", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Xi măng Portland PC40", 1, "Dùng cho bê tông móng, cột, dầm", "Mác PCB40, độ dẻo cao, thời gian đông kết 3-4 giờ", "Còn hàng", 500, 2, 90000m },
                    { 2, "Xi măng trắng", 1, "Dùng cho ốp lát, trang trí", "Độ trắng >85%, cường độ 35MPa", "Còn ít", 120, 2, 140000m },
                    { 3, "Cát vàng xây dựng", 1, "Không lẫn tạp chất, độ sạch >95%", "Module độ lớn 2.0-3.3, cỡ hạt 0.5-2mm", "Còn hàng", 300, 4, 450000m },
                    { 4, "Cát mịn xây tô", 1, "Dùng cho công tác xây, trát tường", "Module độ lớn 1.5-2.0, cỡ hạt 0.15-0.5mm", "Còn hàng", 120, 4, 350000m },
                    { 5, "Đá 1x2", 1, "Dùng cho bê tông móng, cọc", "Kích thước 10x20mm, cường độ >100MPa", "Còn hàng", 300, 4, 320000m },
                    { 6, "Đá mi sàng", 1, "Dùng làm lớp nền, san lấp", "Kích thước 0-5mm, độ ẩm <2%", "Còn hàng", 200, 4, 280000m },
                    { 7, "Gạch ống 4 lỗ", 1, "Dùng xây tường bao", "Cường độ M75, độ hút nước <16%", "Còn hàng", 50000, 3, 2000m },
                    { 8, "Gạch ba banh", 1, "Dùng xây tường chịu lực", "Cường độ M100, khối lượng 14kg/viên", "Còn hàng", 30000, 3, 8500m },
                    { 9, "Thép phi 6", 1, "Dùng làm thép đai, gia cố", "Giới hạn chảy 295MPa, độ dãn dài >18%", "Còn hàng", 3000, 1, 25000m },
                    { 10, "Thép phi 14", 1, "Dùng làm thép dầm, cột", "Giới hạn chảy 400MPa, chiều dài 11.7m/cây", "Còn hàng", 5000, 1, 22000m },
                    { 11, "Bê tông thương phẩm 30MPa", 1, "Dùng cho sàn, dầm, cột", "Cấp phối PC40-Đá 1x2-Cát vàng, độ sụt 10±2cm", "Đặt hàng trước 24h", 0, 4, 1650000m },
                    { 12, "Bê tông thương phẩm 40MPa", 1, "Dùng cho móng nhà cao tầng", "Cấp phối PC50-Đá 1x2-Cát vàng, độ sụt 8±2cm", "Đặt hàng trước 24h", 0, 4, 1850000m },
                    { 13, "Sơn lót nội thất cao cấp", 2, "Dùng cho bề mặt bê tông, tường gạch", "Độ phủ 8-10m²/lít (2 lớp), thời gian khô 2h", "Còn hàng", 50, 9, 1350000m },
                    { 14, "Sơn hoàn thiện ngoại thất", 2, "Dùng cho sơn ngoại thất", "Độ phủ 4-5m²/lít, chống UV, chống rêu mốc", "Còn hàng", 45, 9, 1950000m },
                    { 15, "Gạch ceramic 60x60", 2, "Dùng lát sàn phòng khách", "Độ hút nước <3%, độ dày 9mm, chống trơn", "Còn hàng", 500, 11, 350000m },
                    { 16, "Đá granite đen Phú Yên", 2, "Dùng lát mặt tiền, cầu thang", "Độ dày 20mm, khả năng chịu lực >200MPa", "Nhập theo đơn đặt", 70, 11, 1400000m },
                    { 17, "Keo dán gạch cao cấp", 2, "Dùng cho gạch lớn >60cm", "Độ bám dính >1.5MPa, thời gian chỉnh sửa 20 phút", "Còn hàng", 200, 2, 170000m },
                    { 18, "Keo chà ron", 2, "Dùng chà ron gạch men", "Độ trắng >85%, cường độ 45MPa", "Còn ít", 300, 2, 120000m },
                    { 19, "Màng chống thấm Sika", 2, "Dán bằng đèn khò", "Độ dày 3mm, kháng UV 2000h", "Còn hàng", 50, 10, 1350000m },
                    { 20, "Sơn chống thấm Kova CT-11A", 2, "Phun 2 lớp cách nhau 6h", "Độ phủ 1.5-2m²/lít, chịu được áp lực nước 3 bar", "Còn hàng", 35, 9, 1820000m },
                    { 21, "Ống PPR 32mm", 3, "Dùng cho hệ thống nước nóng lạnh", "Áp lực làm việc 2.0MPa, chịu nhiệt 95°C", "Còn hàng", 2000, 5, 42000m },
                    { 22, "Ống thoát nước PVC 90mm", 3, "Dùng cho thoát nước thải", "Áp lực 10bar, chống tia UV, đường kính ngoài 90mm", "Còn hàng", 1500, 5, 65000m },
                    { 23, "Dây điện đơn 2.5mm² - Daphaco", 3, "Dây đơn lõi đồng, dùng cho đèn", "Tiết diện 2.5mm², vỏ PVC chống cháy", "Còn hàng", 5000, 5, 8000m },
                    { 24, "Dây điện đơn 2.5mm² - Daphaco (dây nguội)", 3, "Dây đơn lõi đồng, dùng cho ổ cắm", "Tiết diện 2.5mm², vỏ PVC chống cháy", "Còn hàng", 4000, 5, 8000m },
                    { 25, "Dây điện đơn 3.5mm² - Daphaco", 3, "Dây đơn lõi đồng, dùng cho điều hòa", "Tiết diện 3.5mm², vỏ PVC chống cháy", "Còn hàng", 3000, 5, 12000m },
                    { 26, "Atomat 1 pha 40A", 3, "Lắp tủ điện chính", "Dòng cắt 40A, điện áp 230V, tiêu chuẩn IEC 60898", "Còn hàng", 150, 3, 98000m },
                    { 27, "Bóng đèn LED bulb 9W", 3, "Tuổi thọ 25,000 giờ", "Công suất 9W (~60W đèn sợi đốt), nhiệt độ màu 4000K", "Còn hàng", 300, 3, 48000m },
                    { 28, "Đèn LED pha 50W", 3, "Chống nước, chống bụi", "IP67, quang thông 5000lm, góc chiếu 120°", "Còn hàng", 50, 3, 350000m },
                    { 29, "Máy bơm nước Pentax", 3, "Bơm tăng áp cho biệt thự", "Lưu lượng 2.4m³/h, cột áp 32m, điện 1 pha 220V", "Đặt hàng trước", 10, 3, 3500000m },
                    { 30, "Bình nóng lạnh Ariston", 3, "Lắp phòng tắm gia đình", "Công suất 2500W, chống giật ELCB, inox 304", "Còn hàng", 15, 3, 3200000m },
                    { 31, "Cốp pha nhôm định hình", 4, "Tái sử dụng 200 lần, chống dính", "Độ dày 4mm, tải trọng 60kN/m², nhôm hợp kim 6061", "Còn hàng", 2000, 11, 950000m },
                    { 32, "Giàn giáo khung 1.7m x 1.2m", 4, "Kèm chéo giằng và mâm đứng", "Thép Q235, tải trọng 4.5 tấn/khung, mạ kẽm nhúng nóng", "Còn hàng", 250, 3, 1450000m },
                    { 33, "Bulong neo M16x150mm", 4, "Dùng neo cột thép vào móng", "Cấp bền 8.8, mạ kẽm điện phân, đầu hexagon", "Còn hàng", 2000, 3, 18000m },
                    { 34, "Vít khoan đuôi chuồn 12mm", 4, "Bắt tôn vào xà gồ", "Thép carbon, phủ lớp zinc 15µm, 100 cái/hộp", "Còn hàng", 15000, 3, 1200m },
                    { 35, "Kính cường lực 10mm", 4, "Thời gian gia công 3-5 ngày", "Độ dày 10±0.2mm, chịu lực 90MPa, an toàn khi vỡ", "Cắt theo yêu cầu", 100, 11, 480000m },
                    { 36, "Tấm inox 304 đánh bóng", 4, "Dùng làm lan can, bếp công nghiệp", "Độ dày 1.5mm, kích thước 1.2x2.4m, bề mặt No.4", "Còn hàng", 45, 11, 850000m },
                    { 37, "Dây thép buộc 1.1mm", 4, "Buộc thép, cốt thép", "Kẽm mạ đồng, độ bền kéo 400-500N/mm²", "Còn hàng", 500, 1, 32000m },
                    { 38, "Thanh nhôm hộp 50x100mm", 4, "Dựng vách ngăn, mặt dựng", "Hợp kim 6063-T5, dài 6m/cây, tải trọng 150kg/m", "Còn hàng", 300, 5, 180000m },
                    { 39, "Mũ bảo hộ lao động", 5, "Màu vàng, in logo công ty", "Nhựa HDPE, chịu lực 5kg, quai cài 4 điểm", "Còn hàng", 200, 3, 65000m },
                    { 40, "Giày bảo hộ lao động", 5, "Size 38-44, chống đinh đâm", "Mũi thép chịu lực 200J, đế chống trượt SR", "Còn hàng", 50, 7, 320000m },
                    { 41, "Keo silicone trung tính", 5, "Dán khe co giãn, chịu UV tốt", "Độ giãn dài 400%, lực bám dính 1.5MPa", "Còn hàng", 200, 12, 75000m },
                    { 42, "Băng keo hai mặt 50mm", 5, "Dán bạt, che phủ bề mặt", "Lực kéo 40N/10mm, chịu nhiệt 80°C", "Còn hàng", 100, 3, 28000m },
                    { 43, "Dây thừng PP 12mm", 5, "Buộc vật liệu, giàn giáo", "Chịu lực 150kg, kháng UV, chống mục", "Còn hàng", 1000, 5, 9000m },
                    { 44, "Lưới an toàn HDPE", 5, "Che chắn an toàn, chống rơi vãi", "Chỉ PE, mắt lưới 15mm, chống tia UV 95%", "Còn hàng", 1000, 11, 15000m },
                    { 45, "Thùng rác công trình 120 lít", 5, "Màu xanh lá, có nắp đậy", "Nhựa HDPE, bánh xe đẩy 360°, nắp kín", "Còn hàng", 30, 3, 550000m },
                    { 46, "Biển báo an toàn công trình", 5, "Theo tiêu chuẩn ISO 7010", "Nhựa PVC dày 3mm, kích thước 30x40cm", "Còn hàng", 50, 3, 120000m },
                    { 47, "Que hàn điện YAWATA-50", 5, "Dùng cho hàn kết cấu thép", "Điện cực thép cacbon E6013, Φ3.2mm", "Còn hàng", 500, 1, 48000m },
                    { 48, "Răng cào máy đào", 5, "Phụ tùng cho máy cào bóc", "Hợp kim thép chịu mài mòn cao", "Còn hàng", 100, 3, 650000m }
                });

            migrationBuilder.InsertData(
                table: "WorkAttributes",
                columns: new[] { "ID", "UnitOfMeasurementID", "WorkAttributeName" },
                values: new object[,]
                {
                    { 1, 6, "Chiều sâu san lấp" },
                    { 2, 13, "Vật liệu san lấp: Cát" },
                    { 3, 13, "Vật liệu san lấp: Đất" },
                    { 4, 6, "Chiều sâu đào" },
                    { 5, 13, "Loại đất" },
                    { 6, 13, "Loại kết cấu cần phá dỡ" },
                    { 7, 6, "Độ dày kết cấu" },
                    { 8, 2, "Chiều cao móng" },
                    { 9, 13, "Loại móng: Băng/Đơn/Bè" },
                    { 10, 13, "Loại bê tông" },
                    { 11, 6, "Chiều cao cột" },
                    { 12, 2, "Chiều dày sàn" },
                    { 13, 13, "Loại gạch" },
                    { 14, 2, "Chiều dày tường" },
                    { 15, 13, "Loại hệ thống: Âm/Nổi" },
                    { 16, 6, "Chiều dài ống" },
                    { 17, 13, "Loại vật liệu ốp lát" },
                    { 18, 2, "Kích thước viên gạch" },
                    { 19, 13, "Loại sơn" },
                    { 20, 6, "Số lớp sơn" },
                    { 21, 13, "Loại cửa" },
                    { 22, 2, "Kích thước cửa" },
                    { 23, 13, "Loại thiết bị vệ sinh" },
                    { 24, 13, "Loại thang máy" },
                    { 25, 6, "Số tầng phục vụ" },
                    { 26, 13, "Loại cảnh quan" },
                    { 27, 6, "Diện tích thi công" },
                    { 28, 13, "Loại bóng đèn" },
                    { 29, 6, "Chiều dài dây điện" },
                    { 30, 6, "Chiều cao tường" },
                    { 31, 2, "Chiều dày lớp trát" }
                });

            migrationBuilder.InsertData(
                table: "WorkSubTypes",
                columns: new[] { "ID", "WorkSubTypeName", "WorkTypeID" },
                values: new object[,]
                {
                    { 1, "San lấp mặt bằng", 1 },
                    { 2, "Đào đất", 1 },
                    { 3, "Phá dỡ công trình cũ", 1 },
                    { 4, "Đổ móng bê tông", 2 },
                    { 5, "Đổ bê tông cột", 2 },
                    { 6, "Đổ bê tông dầm sàn", 2 },
                    { 7, "Xây tường bao", 3 },
                    { 8, "Xây tường ngăn", 3 },
                    { 9, "Lắp đặt hệ thống điện", 4 },
                    { 10, "Lắp đặt hệ thống nước", 4 },
                    { 11, "Ốp lát", 5 },
                    { 12, "Sơn nước", 5 },
                    { 13, "Trát tường", 5 },
                    { 14, "Lắp đặt cửa", 6 },
                    { 15, "Lắp đặt thiết bị vệ sinh", 6 },
                    { 16, "Lắp đặt thang máy", 6 },
                    { 17, "Thi công sân vườn", 7 },
                    { 18, "Thi công hệ thống chiếu sáng", 7 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3", "admin-id" },
                    { "2", "manager1-id" },
                    { "2", "manager2-id" },
                    { "2", "manager3-id" },
                    { "1", "tech1-id" },
                    { "1", "tech2-id" },
                    { "1", "tech3-id" },
                    { "4", "worker1-id" },
                    { "4", "worker2-id" },
                    { "4", "worker3-id" },
                    { "4", "worker4-id" },
                    { "4", "worker5-id" }
                });

            migrationBuilder.InsertData(
                table: "ImportOrderEmployees",
                columns: new[] { "EmployeeID", "ImportOrderId", "Role" },
                values: new object[,]
                {
                    { "manager3-id", 1, "Planner" },
                    { "manager3-id", 2, "Planner" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ID", "ConstructionID", "Content", "EmployeeID", "Level", "ReportDate", "ReportType" },
                values: new object[,]
                {
                    { 1, 1, "Báo cáo tiến độ ngày 1", "manager1-id", "Cao", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố kĩ thuật" },
                    { 2, 2, "Báo cáo tiến độ ngày 2", "manager2-id", "Thấp", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố kĩ thuật" },
                    { 3, 1, "Hệ thống điện gặp trục trặc", "manager1-id", "Cao", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố kĩ thuật" },
                    { 4, 3, "Rò rỉ nước tại tầng hầm", "manager2-id", "Trung bình", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố kĩ thuật" },
                    { 5, 4, "Thiết bị giám sát không hoạt động", "manager3-id", "Thấp", new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố kĩ thuật" },
                    { 6, 1, "Ngã giàn giáo tại khu A", "manager1-id", "Cao", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố thi công" },
                    { 7, 2, "Máy xúc bị hỏng giữa ca", "manager2-id", "Trung bình", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố thi công" },
                    { 8, 3, "Công nhân đình công", "manager3-id", "Cao", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố thi công" },
                    { 9, 4, "Chậm tiến độ do mưa lớn", "manager2-id", "Thấp", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố thi công" },
                    { 10, 1, "Vật liệu không đạt chất lượng", "manager1-id", "Trung bình", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sự cố thi công" }
                });

            migrationBuilder.InsertData(
                table: "WorkSubTypeVariants",
                columns: new[] { "ID", "Description", "WorkSubTypeID" },
                values: new object[,]
                {
                    { 1, "San lấp bằng cát", 1 },
                    { 2, "San lấp bằng đất", 1 },
                    { 3, "Đào đất sét", 2 },
                    { 4, "Đào đất pha cát", 2 },
                    { 5, "Phá dỡ tường gạch", 3 },
                    { 6, "Phá dỡ bê tông cốt thép", 3 },
                    { 7, "Móng băng", 4 },
                    { 8, "Móng đơn", 4 },
                    { 9, "Cột vuông", 5 },
                    { 10, "Cột tròn", 5 },
                    { 11, "Dầm chịu lực chính", 6 },
                    { 12, "Sàn không dầm", 6 },
                    { 13, "Tường gạch đặc", 7 },
                    { 14, "Tường gạch lỗ", 7 },
                    { 15, "Tường thạch cao", 8 },
                    { 16, "Tường gạch nhẹ", 8 },
                    { 17, "Điện âm tường", 9 },
                    { 18, "Điện nổi", 9 },
                    { 19, "Ống nước PVC", 10 },
                    { 20, "Ống nước PPR", 10 },
                    { 21, "Ốp lát gạch ceramic", 11 },
                    { 22, "Ốp lát đá tự nhiên", 11 },
                    { 23, "Sơn nước nội thất", 12 },
                    { 24, "Sơn nước ngoại thất", 12 },
                    { 25, "Trát vữa xi măng cát", 13 },
                    { 26, "Trát vữa xi măng mịn", 13 },
                    { 27, "Cửa nhôm kính", 14 },
                    { 28, "Cửa gỗ công nghiệp", 14 },
                    { 29, "Bồn cầu treo tường", 15 },
                    { 30, "Chậu rửa đặt bàn", 15 },
                    { 31, "Thang máy tải khách", 16 },
                    { 32, "Thang máy tải hàng", 16 },
                    { 33, "Sân vườn biệt thự", 17 },
                    { 34, "Sân vườn công cộng", 17 },
                    { 35, "Chiếu sáng công viên", 18 },
                    { 36, "Chiếu sáng đường phố", 18 }
                });

            migrationBuilder.InsertData(
                table: "ConstructionItems",
                columns: new[] { "ID", "ActualCompletionDate", "ConstructionID", "ConstructionItemName", "ConstructionStatusID", "ExpectedCompletionDate", "StartDate", "TotalVolume", "UnitOfMeasurementID", "WorkSubTypeVariantID" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thi công móng", 3, new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 500f, 6, 3 },
                    { 2, new DateTime(2021, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thi công khung kết cấu", 3, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200f, 1, 8 },
                    { 3, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Xây tường bao và ngăn phòng", 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000f, 12, 13 },
                    { 4, new DateTime(2021, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lắp đặt hệ thống điện nước", 3, new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f, 1, 19 },
                    { 5, new DateTime(2021, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ốp lát nền và tường", 3, new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800f, 8, 21 },
                    { 6, new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sơn tường và chống thấm", 3, new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200f, 8, 23 },
                    { 7, new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lắp cửa và lan can", 3, new DateTime(2021, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 300f, 13, 27 },
                    { 8, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Thi công thang máy và cầu thang", 3, new DateTime(2021, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 10f, 13, 31 },
                    { 9, new DateTime(2021, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hoàn thiện nội thất cơ bản", 3, new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 150f, 13, 30 },
                    { 10, new DateTime(2022, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Cảnh quan và sân vườn", 3, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000f, 8, 33 },
                    { 11, new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "San lấp mặt bằng", 3, new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000f, 6, 1 },
                    { 12, new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Đổ móng bê tông cốt thép", 3, new DateTime(2021, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 800f, 6, 7 },
                    { 13, new DateTime(2021, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Dựng cột, dầm, sàn", 3, new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500f, 1, 9 },
                    { 14, new DateTime(2021, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Xây tường gạch", 3, new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 25000f, 12, 13 },
                    { 15, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Lắp đặt hệ thống điện âm", 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800f, 1, 17 },
                    { 16, new DateTime(2021, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Ốp lát gạch nền", 3, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000f, 8, 22 },
                    { 17, new DateTime(2021, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Sơn nước nội ngoại thất", 3, new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3500f, 8, 23 },
                    { 18, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Lắp đặt hệ thống nước sinh hoạt", 3, new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 60f, 13, 20 },
                    { 19, new DateTime(2021, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Thi công mái nhà", 3, new DateTime(2021, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 900f, 8, 15 },
                    { 20, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Hoàn thiện sân vườn trước nhà", 3, new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 700f, 8, 33 },
                    { 21, null, 3, "San lấp mặt bằng", 3, new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200f, 6, 2 },
                    { 22, null, 3, "Đổ móng bê tông cốt thép", 3, new DateTime(2021, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 900f, 6, 2 },
                    { 23, null, 3, "Dựng cột, dầm, sàn", 3, new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800f, 1, 7 },
                    { 24, null, 3, "Xây tường gạch", 3, new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000f, 12, 13 },
                    { 25, null, 3, "Lắp đặt hệ thống điện âm", 3, new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000f, 1, 17 },
                    { 26, null, 3, "Ốp lát gạch nền", 3, new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f, 8, 22 },
                    { 27, null, 3, "Sơn nước nội ngoại thất", 3, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000f, 8, 24 },
                    { 28, null, 3, "Lắp đặt hệ thống nước sinh hoạt", 3, new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 70f, 13, 30 },
                    { 29, null, 3, "Thi công mái nhà", 3, new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000f, 8, 16 },
                    { 30, null, 3, "Hoàn thiện sân vườn trước nhà", 3, new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 800f, 8, 33 },
                    { 31, new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Thi công nền đường", 3, new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f, 6, 1 },
                    { 32, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Lắp đặt móng cầu", 3, new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200f, 6, 8 },
                    { 33, null, 4, "Đổ bê tông cầu", 3, new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500f, 6, 9 },
                    { 34, null, 4, "Lắp đặt cầu giao thông", 3, new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500f, 8, 14 },
                    { 35, null, 4, "Lắp đặt hệ thống thoát nước", 3, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 800f, 6, 19 },
                    { 36, null, 4, "Hoàn thiện mặt cầu", 3, new DateTime(2022, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1200f, 8, 25 },
                    { 37, null, 4, "Thi công bảo trì", 3, new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 800f, 8, 26 },
                    { 38, null, 4, "Lắp đặt hệ thống chiếu sáng cầu", 3, new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50f, 13, 36 },
                    { 39, new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Thi công nền đường", 3, new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4000f, 6, 1 },
                    { 40, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lát mặt đường nhựa", 3, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000f, 8, 8 },
                    { 41, new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Thi công rãnh thoát nước", 3, new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500f, 1, 19 },
                    { 42, new DateTime(2022, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Sơn kẻ vạch đường", 3, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 600f, 8, 24 },
                    { 43, new DateTime(2022, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lắp đặt hệ thống đèn đường", 3, new DateTime(2022, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 100f, 13, 36 },
                    { 44, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Kiểm tra & nghiệm thu", 3, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1f, 13, 36 },
                    { 45, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "San lấp mặt bằng", 3, new DateTime(2020, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 12000f, 6, 2 },
                    { 46, new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Thi công móng", 3, new DateTime(2021, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5000f, 6, 7 },
                    { 47, null, 6, "Dựng khung thép nhà xưởng", 2, new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 350f, 7, 9 },
                    { 48, null, 6, "Lắp đặt máy móc thiết bị", 4, new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 45f, 13, 31 },
                    { 49, null, 6, "Xây dựng nhà kho nguyên liệu", 1, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800f, 8, 8 },
                    { 50, null, 6, "Thi công hệ thống xử lý nước thải", 1, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2f, 13, 20 },
                    { 51, new DateTime(2020, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Khảo sát địa chất", 3, new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1f, 13, 1 },
                    { 52, new DateTime(2020, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "San lấp mặt bằng", 3, new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000f, 6, 2 },
                    { 53, null, 7, "Đào hố móng đập", 2, new DateTime(2020, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000f, 6, 4 },
                    { 54, null, 7, "Lắp cống xả đáy", 4, new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3f, 13, 19 },
                    { 55, null, 7, "Xây thân đập", 1, new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15000f, 6, 7 },
                    { 56, null, 7, "Làm đường công vụ", 1, new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500f, 8, 33 }
                });

            migrationBuilder.InsertData(
                table: "ConstructionTemplateItems",
                columns: new[] { "ID", "ConstructionTemplateItemName", "ConstructionTypeID", "WorkSubTypeVarientID" },
                values: new object[,]
                {
                    { 1, "Chuẩn bị mặt bằng", 2, 1 },
                    { 2, "Thi công móng băng", 2, 7 },
                    { 3, "Thi công cột vuông", 2, 9 },
                    { 4, "Đổ bê tông dầm chịu lực", 2, 11 },
                    { 5, "Xây tường gạch đặc", 2, 13 },
                    { 6, "Lắp đặt hệ thống điện âm tường", 2, 17 },
                    { 7, "Lắp đặt hệ thống nước PVC", 2, 19 },
                    { 8, "Ốp lát gạch ceramic", 2, 21 },
                    { 9, "Sơn nước nội thất", 2, 23 },
                    { 10, "Lắp đặt cửa nhôm kính", 2, 27 },
                    { 11, "Lắp đặt thiết bị vệ sinh", 2, 29 },
                    { 12, "Thi công sân vườn biệt thự", 2, 33 },
                    { 13, "Giải phóng mặt bằng bằng cát", 1, 1 },
                    { 14, "Đào đất sét nền móng", 1, 3 },
                    { 15, "Phá dỡ công trình cũ", 1, 5 },
                    { 16, "Đổ móng băng cầu", 1, 7 },
                    { 17, "Đổ bê tông cột cầu", 1, 9 },
                    { 18, "Đổ bê tông dầm cầu", 1, 11 },
                    { 19, "Thi công hệ thống chiếu sáng đường", 1, 36 },
                    { 20, "Chuẩn bị mặt bằng nhà máy", 3, 1 },
                    { 21, "Đổ móng băng chịu tải", 3, 7 },
                    { 22, "Thi công cột vuông nhà xưởng", 3, 9 },
                    { 23, "Đổ bê tông dầm chịu lực chính", 3, 11 },
                    { 24, "Xây tường gạch đặc nhà máy", 3, 13 },
                    { 25, "Lắp hệ thống điện âm", 3, 17 },
                    { 26, "Lắp hệ thống nước PVC", 3, 19 },
                    { 27, "Thi công chiếu sáng công viên", 3, 35 },
                    { 28, "Chuẩn bị mặt bằng đập/kenh", 4, 1 },
                    { 29, "Đào đất kênh/mương", 4, 3 },
                    { 30, "Phá dỡ công trình thủy lợi cũ", 4, 5 },
                    { 31, "Thi công móng cống đập", 4, 7 },
                    { 32, "Đổ cột công trình thủy", 4, 9 },
                    { 33, "Đổ dầm sàn kênh", 4, 11 },
                    { 34, "Chiếu sáng khu vực công trình", 4, 36 }
                });

            migrationBuilder.InsertData(
                table: "MaterialNorms",
                columns: new[] { "MaterialID", "WorkSubTypeVariantID", "Quantity" },
                values: new object[,]
                {
                    { 1, 7, 350 },
                    { 1, 10, 380 },
                    { 1, 13, 340 },
                    { 1, 14, 150 },
                    { 1, 15, 100 },
                    { 1, 21, 50 },
                    { 3, 1, 105 },
                    { 3, 7, 650 },
                    { 3, 10, 620 },
                    { 3, 13, 660 },
                    { 3, 35, 200 },
                    { 4, 14, 300 },
                    { 4, 15, 200 },
                    { 5, 7, 950 },
                    { 5, 10, 930 },
                    { 5, 13, 940 },
                    { 6, 1, 10 },
                    { 6, 2, 15 },
                    { 6, 35, 300 },
                    { 7, 14, 1000 },
                    { 8, 15, 500 },
                    { 9, 7, 80 },
                    { 10, 10, 150 },
                    { 10, 13, 100 },
                    { 13, 24, 10 },
                    { 14, 24, 15 },
                    { 15, 22, 400 },
                    { 17, 22, 25 },
                    { 18, 22, 5 },
                    { 21, 20, 30 },
                    { 22, 21, 10 },
                    { 23, 18, 5 },
                    { 35, 28, 20 },
                    { 37, 7, 5 },
                    { 38, 28, 8 },
                    { 48, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "ReportAttachments",
                columns: new[] { "ID", "FilePath", "ReportID", "UploadDate" },
                values: new object[,]
                {
                    { 1, "/uploads/report1.pdf", 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "/uploads/report2.pdf", 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "/uploads/report3.pdf", 3, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "/uploads/report4.pdf", 4, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "/uploads/report5.pdf", 5, new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "/uploads/report6.pdf", 6, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "/uploads/report7.pdf", 7, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "/uploads/report8.pdf", 8, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "/uploads/report9.pdf", 9, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "/uploads/report10.pdf", 10, new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ReportStatusLogs",
                columns: new[] { "ID", "Note", "ReportDate", "ReportID", "Status" },
                values: new object[,]
                {
                    { 1, "Đang chờ phê duyệt", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Pending" },
                    { 2, "Đang chờ phê duyệt", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pending" },
                    { 3, "Đang chờ phê duyệt", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Pending" },
                    { 4, "Đang chờ phê duyệt", new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Pending" },
                    { 5, "Đang chờ phê duyệt", new DateTime(2023, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Pending" },
                    { 6, "Đang chờ phê duyệt", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pending" },
                    { 7, "Đang chờ phê duyệt", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Pending" },
                    { 8, "Đang chờ phê duyệt", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Pending" },
                    { 9, "Đang chờ phê duyệt", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, "Pending" },
                    { 10, "Đang chờ phê duyệt", new DateTime(2023, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "WorkSubTypeVariant_WorkAttributes",
                columns: new[] { "WorkAttributeID", "WorkSubTypeVariantID", "Value" },
                values: new object[,]
                {
                    { 1, 1, "2" },
                    { 2, 1, "Cát" },
                    { 1, 2, "2" },
                    { 3, 2, "Đất" },
                    { 4, 3, "3" },
                    { 5, 3, "Đất sét" },
                    { 4, 4, "3" },
                    { 5, 4, "Đất pha cát" },
                    { 6, 5, "Tường gạch" },
                    { 7, 5, "0.2" },
                    { 6, 6, "Bê tông cốt thép" },
                    { 7, 6, "0.3" },
                    { 8, 7, "1.5" },
                    { 9, 7, "Móng băng" },
                    { 8, 8, "1.2" },
                    { 9, 8, "Móng đơn" },
                    { 10, 9, "Bê tông M250" },
                    { 11, 9, "3.0" },
                    { 10, 10, "Bê tông M300" },
                    { 11, 10, "3.0" },
                    { 10, 11, "Bê tông M250" },
                    { 12, 11, "0.2" },
                    { 10, 12, "Bê tông nhẹ" },
                    { 12, 12, "0.15" },
                    { 13, 13, "Gạch đặc" },
                    { 14, 13, "0.22" },
                    { 13, 14, "Gạch lỗ" },
                    { 14, 14, "0.15" },
                    { 13, 15, "Thạch cao" },
                    { 14, 15, "0.10" },
                    { 13, 16, "Gạch nhẹ" },
                    { 14, 16, "0.15" },
                    { 15, 17, "Âm tường" },
                    { 16, 17, "50" },
                    { 15, 18, "Nổi" },
                    { 16, 18, "70" },
                    { 15, 19, "PVC" },
                    { 16, 19, "25" },
                    { 15, 20, "PPR" },
                    { 16, 20, "32" },
                    { 17, 21, "Gạch ceramic" },
                    { 18, 21, "30x30" },
                    { 17, 22, "Đá tự nhiên" },
                    { 18, 22, "40x40" },
                    { 19, 23, "Sơn nước nội thất" },
                    { 20, 23, "2" },
                    { 19, 24, "Sơn nước ngoại thất" },
                    { 20, 24, "3" },
                    { 13, 25, "5" },
                    { 14, 25, "0.03" },
                    { 13, 26, "10" },
                    { 14, 26, "0.02" },
                    { 21, 27, "Nhôm kính" },
                    { 22, 27, "2x2" },
                    { 21, 28, "Gỗ công nghiệp" },
                    { 22, 28, "2.5x2.5" },
                    { 23, 29, "Bồn cầu treo tường" },
                    { 23, 30, "Chậu rửa đặt bàn" },
                    { 24, 31, "Tải khách" },
                    { 25, 31, "12" },
                    { 24, 32, "Tải hàng" },
                    { 25, 32, "6" },
                    { 26, 33, "Biệt thự" },
                    { 27, 33, "500" },
                    { 26, 34, "Công cộng" },
                    { 27, 34, "1000" },
                    { 28, 35, "Đèn LED" },
                    { 29, 35, "100" },
                    { 28, 36, "Đèn Sodium" },
                    { 29, 36, "150" }
                });

            migrationBuilder.InsertData(
                table: "ConstructionPlans",
                columns: new[] { "ID", "ActualCompletionDate", "ConstructionID", "ConstructionItemID", "ConstructionStatusID", "EmployeeID", "ExpectedCompletionDate", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 3, "manager1-id", new DateTime(2021, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 3, "manager1-id", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2021, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 3, "manager2-id", new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2021, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 3, "manager2-id", new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2021, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3, 3, "manager3-id", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2021, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 4, 3, "manager3-id", new DateTime(2021, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2021, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 4, 3, "manager1-id", new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2021, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 5, 3, "manager2-id", new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 6, 3, "manager3-id", new DateTime(2021, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 6, 3, "manager1-id", new DateTime(2021, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 7, 3, "manager2-id", new DateTime(2021, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 8, 3, "manager3-id", new DateTime(2021, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2021, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 9, 3, "manager2-id", new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2022, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 10, 3, "manager1-id", new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2021, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 11, 3, "manager1-id", new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2021, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 11, 3, "manager2-id", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2021, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 12, 3, "manager3-id", new DateTime(2021, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2021, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 12, 3, "manager1-id", new DateTime(2021, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 13, 3, "manager2-id", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2021, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 13, 3, "manager3-id", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2021, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 14, 3, "manager1-id", new DateTime(2021, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2021, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 14, 3, "manager2-id", new DateTime(2021, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 15, 3, "manager3-id", new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2021, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 15, 3, "manager1-id", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2021, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 16, 3, "manager2-id", new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, new DateTime(2021, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 17, 3, "manager3-id", new DateTime(2021, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, new DateTime(2021, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 17, 3, "manager1-id", new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 18, 3, "manager2-id", new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, new DateTime(2021, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 19, 3, "manager3-id", new DateTime(2021, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 20, 3, "manager1-id", new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, null, 0, 21, 2, "manager1-id", new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, null, 0, 22, 2, "manager2-id", new DateTime(2021, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, null, 0, 23, 2, "manager3-id", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, null, 0, 24, 2, "manager1-id", new DateTime(2021, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, null, 0, 25, 2, "manager2-id", new DateTime(2021, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, null, 0, 26, 2, "manager3-id", new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, null, 0, 27, 2, "manager1-id", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, null, 0, 28, 2, "manager2-id", new DateTime(2022, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, null, 0, 29, 2, "manager3-id", new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, null, 0, 30, 2, "manager1-id", new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, new DateTime(2021, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 31, 3, "manager1-id", new DateTime(2021, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 31, 3, "manager2-id", new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 32, 3, "manager3-id", new DateTime(2021, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 32, 3, "manager1-id", new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, null, 0, 33, 2, "manager2-id", new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, null, 0, 33, 2, "manager3-id", new DateTime(2021, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, null, 0, 34, 2, "manager1-id", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, null, 0, 35, 2, "manager2-id", new DateTime(2022, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, null, 0, 35, 2, "manager3-id", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, null, 0, 36, 2, "manager1-id", new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, null, 0, 37, 2, "manager2-id", new DateTime(2022, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, null, 0, 38, 2, "manager3-id", new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 39, 3, "manager1-id", new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 39, 3, "manager2-id", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, new DateTime(2022, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 40, 3, "manager3-id", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 40, 3, "manager1-id", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 41, 3, "manager2-id", new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, new DateTime(2022, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 42, 3, "manager3-id", new DateTime(2022, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, new DateTime(2022, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 42, 3, "manager1-id", new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 43, 3, "manager2-id", new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, new DateTime(2022, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 43, 3, "manager3-id", new DateTime(2022, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 44, 3, "manager1-id", new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 44, 3, "manager2-id", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 45, 3, "manager1-id", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 45, 3, "manager2-id", new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 46, 3, "manager3-id", new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 46, 3, "manager1-id", new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, null, 0, 47, 2, "manager2-id", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, null, 0, 47, 2, "manager3-id", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, null, 0, 48, 4, "manager1-id", new DateTime(2021, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, null, 0, 49, 1, "manager2-id", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, null, 0, 50, 1, "manager3-id", new DateTime(2022, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, new DateTime(2020, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 51, 3, "manager1-id", new DateTime(2020, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, new DateTime(2020, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 51, 3, "manager2-id", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 52, 3, "manager3-id", new DateTime(2020, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, new DateTime(2020, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 52, 3, "manager1-id", new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, null, 0, 53, 2, "manager2-id", new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, null, 0, 53, 2, "manager3-id", new DateTime(2020, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, null, 0, 54, 4, "manager1-id", new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, null, 0, 55, 1, "manager2-id", new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, null, 0, 55, 1, "manager3-id", new DateTime(2021, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, null, 0, 56, 1, "manager1-id", new DateTime(2021, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ExportOrders",
                columns: new[] { "ID", "ConstructionItemID", "EmployeeID", "ExportDate" },
                values: new object[,]
                {
                    { 1, 1, "manager1-id", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "manager2-id", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MaterialPlans",
                columns: new[] { "ConstructionItemID", "ImportOrderID", "MaterialID", "ImportQuantity", "Note" },
                values: new object[,]
                {
                    { 1, 1, 1, 50, null },
                    { 1, 1, 3, 100, null },
                    { 2, 2, 10, 500, null },
                    { 2, 2, 48, 2, null }
                });

            migrationBuilder.InsertData(
                table: "ConstructionTasks",
                columns: new[] { "ID", "ActualWorkload", "ConstructionPlanID", "ConstructionStatusID", "Workload" },
                values: new object[,]
                {
                    { 1, null, 1, 3, 150f },
                    { 2, null, 1, 3, 100f },
                    { 3, null, 2, 3, 150f },
                    { 4, null, 2, 3, 100f },
                    { 5, null, 3, 3, 300f },
                    { 6, null, 3, 3, 300f },
                    { 7, null, 4, 3, 300f },
                    { 8, null, 4, 3, 300f },
                    { 9, null, 5, 3, 15000f },
                    { 10, null, 5, 3, 15000f },
                    { 11, null, 6, 3, 1250f },
                    { 12, null, 7, 3, 1250f },
                    { 13, null, 8, 3, 900f },
                    { 14, null, 8, 3, 900f },
                    { 15, null, 9, 3, 1100f },
                    { 16, null, 9, 3, 1100f },
                    { 17, null, 11, 3, 150f },
                    { 18, null, 11, 3, 150f },
                    { 19, null, 12, 3, 5f },
                    { 20, null, 12, 3, 5f },
                    { 21, null, 13, 3, 100f },
                    { 22, null, 13, 3, 50f },
                    { 23, null, 14, 3, 500f },
                    { 24, null, 14, 3, 500f },
                    { 25, null, 15, 3, 600f },
                    { 26, null, 15, 3, 200f },
                    { 27, null, 16, 3, 200f },
                    { 28, null, 17, 3, 400f },
                    { 29, null, 17, 3, 200f },
                    { 30, null, 17, 3, 200f },
                    { 31, null, 18, 3, 500f },
                    { 32, null, 18, 3, 300f },
                    { 33, null, 19, 3, 200f },
                    { 34, null, 19, 3, 100f },
                    { 35, null, 20, 3, 600f },
                    { 36, null, 20, 3, 600f },
                    { 37, null, 21, 3, 12000f },
                    { 38, null, 22, 3, 13000f },
                    { 39, null, 23, 3, 900f },
                    { 40, null, 24, 3, 300f },
                    { 41, null, 24, 3, 600f },
                    { 42, null, 25, 3, 1500f },
                    { 43, null, 25, 3, 500f },
                    { 44, null, 26, 3, 1500f },
                    { 45, null, 26, 3, 1000f },
                    { 46, null, 27, 3, 1000f },
                    { 47, null, 28, 3, 40f },
                    { 48, null, 28, 3, 20f },
                    { 49, null, 29, 3, 400f },
                    { 50, null, 29, 3, 500f },
                    { 51, null, 30, 3, 350f },
                    { 52, null, 30, 3, 350f },
                    { 53, null, 31, 2, 400f },
                    { 54, null, 31, 2, 300f },
                    { 55, null, 31, 2, 250f },
                    { 56, null, 31, 2, 150f },
                    { 57, null, 31, 2, 100f },
                    { 58, null, 32, 2, 300f },
                    { 59, null, 32, 2, 200f },
                    { 60, null, 32, 2, 250f },
                    { 61, null, 32, 2, 150f },
                    { 62, null, 33, 2, 500f },
                    { 63, null, 33, 2, 400f },
                    { 64, null, 33, 2, 300f },
                    { 65, null, 33, 2, 300f },
                    { 66, null, 33, 2, 300f },
                    { 67, null, 34, 2, 8000f },
                    { 68, null, 34, 2, 7000f },
                    { 69, null, 34, 2, 6000f },
                    { 70, null, 34, 2, 5000f },
                    { 71, null, 34, 2, 4000f },
                    { 72, null, 35, 2, 500f },
                    { 73, null, 35, 2, 400f },
                    { 74, null, 35, 2, 400f },
                    { 75, null, 35, 2, 400f },
                    { 76, null, 35, 2, 300f },
                    { 77, null, 36, 2, 600f },
                    { 78, null, 36, 2, 600f },
                    { 79, null, 36, 2, 500f },
                    { 80, null, 36, 2, 400f },
                    { 81, null, 36, 2, 400f },
                    { 82, null, 37, 2, 1000f },
                    { 83, null, 37, 2, 1000f },
                    { 84, null, 37, 2, 800f },
                    { 85, null, 37, 2, 700f },
                    { 86, null, 37, 2, 500f },
                    { 87, null, 38, 2, 20f },
                    { 88, null, 38, 2, 20f },
                    { 89, null, 38, 2, 15f },
                    { 90, null, 38, 2, 15f },
                    { 91, null, 39, 2, 300f },
                    { 92, null, 39, 2, 250f },
                    { 93, null, 39, 2, 200f },
                    { 94, null, 39, 2, 150f },
                    { 95, null, 39, 2, 100f },
                    { 96, null, 40, 2, 300f },
                    { 97, null, 40, 2, 200f },
                    { 98, null, 40, 2, 200f },
                    { 99, null, 40, 2, 100f },
                    { 100, null, 41, 3, 1300f },
                    { 101, null, 41, 3, 200f },
                    { 102, null, 42, 3, 600f },
                    { 103, null, 42, 3, 400f },
                    { 104, null, 43, 3, 700f },
                    { 105, null, 44, 3, 300f },
                    { 106, null, 44, 3, 200f },
                    { 107, null, 45, 2, 400f },
                    { 108, null, 45, 2, 300f },
                    { 109, null, 46, 2, 500f },
                    { 110, null, 46, 2, 300f },
                    { 111, null, 47, 2, 150f },
                    { 112, null, 47, 2, 200f },
                    { 113, null, 47, 2, 150f },
                    { 114, null, 48, 2, 300f },
                    { 115, null, 48, 2, 150f },
                    { 116, null, 49, 2, 250f },
                    { 117, null, 49, 2, 100f },
                    { 118, null, 50, 2, 300f },
                    { 119, null, 50, 2, 300f },
                    { 200, null, 50, 2, 300f },
                    { 201, null, 50, 2, 300f },
                    { 202, null, 51, 2, 250f },
                    { 203, null, 51, 2, 250f },
                    { 204, null, 51, 2, 300f },
                    { 205, null, 52, 2, 20f },
                    { 206, null, 52, 2, 15f },
                    { 207, null, 52, 2, 15f },
                    { 208, null, 53, 3, 1800f },
                    { 209, null, 53, 3, 200f },
                    { 210, null, 54, 3, 1500f },
                    { 211, null, 54, 3, 500f },
                    { 212, null, 55, 3, 2300f },
                    { 213, null, 56, 3, 1800f },
                    { 214, null, 56, 3, 900f },
                    { 215, null, 57, 3, 400f },
                    { 216, null, 57, 3, 300f },
                    { 217, null, 57, 3, 300f },
                    { 218, null, 57, 3, 250f },
                    { 219, null, 57, 3, 250f },
                    { 220, null, 58, 3, 300f },
                    { 221, null, 59, 3, 300f },
                    { 222, null, 60, 3, 60f },
                    { 223, null, 61, 3, 40f },
                    { 224, null, 62, 3, 0.5f },
                    { 225, null, 63, 3, 0.5f },
                    { 226, null, 64, 3, 6000f },
                    { 227, null, 65, 3, 6000f },
                    { 228, null, 66, 3, 2500f },
                    { 229, null, 67, 3, 2500f },
                    { 230, null, 68, 2, 175f },
                    { 231, null, 69, 2, 175f },
                    { 232, null, 70, 4, 22.5f },
                    { 233, null, 70, 4, 22.5f },
                    { 234, null, 71, 1, 900f },
                    { 235, null, 71, 1, 900f },
                    { 236, null, 72, 1, 1f },
                    { 237, null, 72, 1, 1f },
                    { 238, null, 73, 3, 1f },
                    { 239, null, 75, 3, 5000f },
                    { 240, null, 76, 3, 5000f },
                    { 241, null, 77, 2, 3500f },
                    { 242, null, 78, 2, 3500f },
                    { 243, null, 79, 4, 3f },
                    { 244, null, 80, 1, 7500f },
                    { 245, null, 81, 1, 7500f },
                    { 246, null, 82, 1, 1250f },
                    { 247, null, 82, 1, 1250f }
                });

            migrationBuilder.InsertData(
                table: "Material_ExportOrders",
                columns: new[] { "ExportOrderID", "MaterialID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 20 },
                    { 1, 2, 10 },
                    { 2, 3, 15 }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceDate", "ConstructionTaskID", "EmployeeID", "Status" },
                values: new object[,]
                {
                    { new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "manager2-id", "có mặt" },
                    { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "manager2-id", "vắng mặt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleID",
                table: "AspNetUsers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ConstructionTaskID",
                table: "Attendances",
                column: "ConstructionTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionItems_ConstructionID",
                table: "ConstructionItems",
                column: "ConstructionID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionItems_ConstructionStatusID",
                table: "ConstructionItems",
                column: "ConstructionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionItems_UnitOfMeasurementID",
                table: "ConstructionItems",
                column: "UnitOfMeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionItems_WorkSubTypeVariantID",
                table: "ConstructionItems",
                column: "WorkSubTypeVariantID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionPlans_ConstructionItemID",
                table: "ConstructionPlans",
                column: "ConstructionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionPlans_ConstructionStatusID",
                table: "ConstructionPlans",
                column: "ConstructionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionPlans_EmployeeID",
                table: "ConstructionPlans",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ConstructionStatusID",
                table: "Constructions",
                column: "ConstructionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ConstructionTypeID",
                table: "Constructions",
                column: "ConstructionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionTasks_ConstructionPlanID",
                table: "ConstructionTasks",
                column: "ConstructionPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionTasks_ConstructionStatusID",
                table: "ConstructionTasks",
                column: "ConstructionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionTemplateItems_ConstructionTypeID",
                table: "ConstructionTemplateItems",
                column: "ConstructionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionTemplateItems_WorkSubTypeVarientID",
                table: "ConstructionTemplateItems",
                column: "WorkSubTypeVarientID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportOrders_ConstructionItemID",
                table: "ExportOrders",
                column: "ConstructionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ExportOrders_EmployeeID",
                table: "ExportOrders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportOrderEmployees_EmployeeID",
                table: "ImportOrderEmployees",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportOrders_ApplicationUserId",
                table: "ImportOrders",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_ExportOrders_MaterialID",
                table: "Material_ExportOrders",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialNorms_WorkSubTypeVariantID",
                table: "MaterialNorms",
                column: "WorkSubTypeVariantID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPlans_ConstructionItemID",
                table: "MaterialPlans",
                column: "ConstructionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPlans_MaterialID",
                table: "MaterialPlans",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeID",
                table: "Materials",
                column: "MaterialTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_UnitOfMeasurementID",
                table: "Materials",
                column: "UnitOfMeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAttachments_ReportID",
                table: "ReportAttachments",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ConstructionID",
                table: "Reports",
                column: "ConstructionID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EmployeeID",
                table: "Reports",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStatusLogs_ReportID",
                table: "ReportStatusLogs",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAttributes_UnitOfMeasurementID",
                table: "WorkAttributes",
                column: "UnitOfMeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSubTypes_WorkTypeID",
                table: "WorkSubTypes",
                column: "WorkTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSubTypeVariant_WorkAttributes_WorkAttributeID",
                table: "WorkSubTypeVariant_WorkAttributes",
                column: "WorkAttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSubTypeVariants_WorkSubTypeID",
                table: "WorkSubTypeVariants",
                column: "WorkSubTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "ConstructionTemplateItems");

            migrationBuilder.DropTable(
                name: "ImportOrderEmployees");

            migrationBuilder.DropTable(
                name: "Material_ExportOrders");

            migrationBuilder.DropTable(
                name: "MaterialNorms");

            migrationBuilder.DropTable(
                name: "MaterialPlans");

            migrationBuilder.DropTable(
                name: "ReportAttachments");

            migrationBuilder.DropTable(
                name: "ReportStatusLogs");

            migrationBuilder.DropTable(
                name: "WorkSubTypeVariant_WorkAttributes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ConstructionTasks");

            migrationBuilder.DropTable(
                name: "ExportOrders");

            migrationBuilder.DropTable(
                name: "ImportOrders");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "WorkAttributes");

            migrationBuilder.DropTable(
                name: "ConstructionPlans");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ConstructionItems");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Constructions");

            migrationBuilder.DropTable(
                name: "UnitofMeasurements");

            migrationBuilder.DropTable(
                name: "WorkSubTypeVariants");

            migrationBuilder.DropTable(
                name: "ConstructionStatuses");

            migrationBuilder.DropTable(
                name: "ConstructionTypes");

            migrationBuilder.DropTable(
                name: "WorkSubTypes");

            migrationBuilder.DropTable(
                name: "WorkTypeItems");
        }
    }
}
