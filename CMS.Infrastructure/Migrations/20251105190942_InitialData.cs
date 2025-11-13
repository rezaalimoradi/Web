using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsCouponRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StartOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RuleToApply = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountStep = table.Column<int>(type: "int", nullable: true),
                    UsageLimitPerCoupon = table.Column<int>(type: "int", nullable: true),
                    UsageLimitPerCustomer = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StartOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompareLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompareLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxUsageCount = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    SizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "Unknown"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ConfigureUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LandingViewComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AdditionalSettings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProviders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ConfigureUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LandingViewComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AdditionalSettings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopLevelDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Extension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLevelDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebsiteId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "CartRuleTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRuleTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartRuleTranslations_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExpiresOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupons_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogRuleTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogRuleId = table.Column<long>(type: "bigint", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogRuleTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogRuleTranslations_CatalogRules_CatalogRuleId",
                        column: x => x.CatalogRuleId,
                        principalTable: "CatalogRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutAddressTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutAddressId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutAddressTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutAddressTranslations_CheckoutAddresses_CheckoutAddressId",
                        column: x => x.CheckoutAddressId,
                        principalTable: "CheckoutAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountTranslations_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingProviderId = table.Column<long>(type: "bigint", nullable: false),
                    BaseFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstimatedDeliveryDays = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingMethods_ShippingProviders_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingFreeRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ShippingId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingFreeRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingFreeRules_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ShippingId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingRates_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouponTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<long>(type: "bigint", nullable: false),
                    Culture = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponTranslations_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "CartRuleCustomerGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRuleCustomerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartRuleCustomerGroups_AspNetUsers_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartRuleCustomerGroups_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartRuleUsages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    UsedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRuleUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartRuleUsages_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartRuleUsages_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogRuleCustomerGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogRuleId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogRuleCustomerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogRuleCustomerGroups_AspNetUsers_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CatalogRuleCustomerGroups_CatalogRules_CatalogRuleId",
                        column: x => x.CatalogRuleId,
                        principalTable: "CatalogRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CouponRuleName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ShippingMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsProductPriceIncludeTax = table.Column<bool>(type: "bit", nullable: false),
                    ShippingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
                    ShippingData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkouts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checkouts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CouponUsages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    UsedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponUsages_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CouponUsages_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedById = table.Column<long>(type: "bigint", nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CouponRuleName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotalWithDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddressId = table.Column<long>(type: "bigint", nullable: false),
                    BillingAddressId = table.Column<long>(type: "bigint", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsMasterOrder = table.Column<bool>(type: "bit", nullable: false),
                    ShippingFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_LatestUpdatedById",
                        column: x => x.LatestUpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderAddresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "OrderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "OrderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Orders_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebSites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactInfo_Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSites_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutPayments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    GatewayTransactionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutPayments_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutShippings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutId = table.Column<long>(type: "bigint", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutShippings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutShippings_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShippingMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutTranslations_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: true),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    GatewayTransactionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LatestUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategories_BlogCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogCategories_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReadingTime = table.Column<long>(type: "bigint", nullable: false),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllowComment = table.Column<bool>(type: "bit", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTags_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerIdentifier = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowInMenu = table.Column<bool>(type: "bit", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    AllowMultipleValues = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxs_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebSiteDomains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    TldId = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteDomains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSiteDomains_TopLevelDomains_TldId",
                        column: x => x.TldId,
                        principalTable: "TopLevelDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebSiteDomains_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebSiteLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSiteLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebSiteLanguages_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WebSiteRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSiteRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebSiteRoles_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebSiteThemes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ThemeId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebSiteThemes_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebSiteThemes_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutPaymentTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutPaymentId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PaymentMethodName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutPaymentTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutPaymentTranslations_CheckoutPayments_CheckoutPaymentId",
                        column: x => x.CheckoutPaymentId,
                        principalTable: "CheckoutPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutShippingTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutShippingId = table.Column<long>(type: "bigint", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutShippingTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutShippingTranslations_CheckoutShippings_CheckoutShippingId",
                        column: x => x.CheckoutShippingId,
                        principalTable: "CheckoutShippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostCategories",
                columns: table => new
                {
                    BlogPostId = table.Column<long>(type: "bigint", nullable: false),
                    BlogCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategories", x => new { x.BlogPostId, x.BlogCategoryId });
                    table.ForeignKey(
                        name: "FK_BlogPostCategories_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPostCategories_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTags",
                columns: table => new
                {
                    BlogPostId = table.Column<long>(type: "bigint", nullable: false),
                    BlogTagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTags", x => new { x.BlogPostId, x.BlogTagId });
                    table.ForeignKey(
                        name: "FK_BlogPostTags_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogPostTags_BlogTags_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "BlogTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartRuleCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRuleCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartRuleCategories_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartRuleCategories_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SpecialPriceStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpecialPriceEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: true),
                    WebSiteId = table.Column<long>(type: "bigint", nullable: false),
                    TaxId = table.Column<long>(type: "bigint", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ManageInventory = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsOriginal = table.Column<bool>(type: "bit", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    ShowOnHomepage = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    AllowCustomerReviews = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsCallForPrice = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    BrandId1 = table.Column<long>(type: "bigint", nullable: true),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId1",
                        column: x => x.BrandId1,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Taxs_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_WebSites_WebSiteId",
                        column: x => x.WebSiteId,
                        principalTable: "WebSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogCategoryTranslations_BlogCategories_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogCategoryTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeoTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostTranslations_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogTagTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogTagId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTagTranslations_BlogTags_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "BlogTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandTranslations_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartTranslations_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderAddressTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderAddressId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddressTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAddressTranslations_OrderAddresses_OrderAddressId",
                        column: x => x.OrderAddressId,
                        principalTable: "OrderAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderAddressTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistoryTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHistoryId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OrderSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistoryTranslations_OrderHistories_OrderHistoryId",
                        column: x => x.OrderHistoryId,
                        principalTable: "OrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHistoryTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ShippingMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTranslations_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PageTranslation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeoTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageTranslation_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageTranslation_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviderTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentProviderId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ConfigureUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LandingViewComponentName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviderTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentProviderTranslations_PaymentProviders_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProviderTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTranslations_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeTranslations_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategoryTranslations_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategoryTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipmentTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentTranslations_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingFreeTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingFreeId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingFreeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingFreeTranslations_ShippingFreeRules_ShippingFreeId",
                        column: x => x.ShippingFreeId,
                        principalTable: "ShippingFreeRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingFreeTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethodTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingMethodId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethodTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingMethodTranslations_ShippingMethods_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingMethodTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProviderTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingProviderId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviderTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingProviderTranslations_ShippingProviders_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingProviderTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingRateTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingRateId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingRateTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingRateTranslations_ShippingRates_ShippingRateId",
                        column: x => x.ShippingRateId,
                        principalTable: "ShippingRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingRateTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingTranslations_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartTranslations_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxTranslations_Taxs_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaxTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSiteRoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPermissions_WebSiteRoles_WebSiteRoleId",
                        column: x => x.WebSiteRoleId,
                        principalTable: "WebSiteRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MenuItemId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemTranslations_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValueTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAttributeValueId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValueTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValueTranslations_ProductAttributeValues_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValueTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartRuleProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartRuleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartRuleProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartRuleProducts_CartRules_CartRuleId",
                        column: x => x.CartRuleId,
                        principalTable: "CartRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartRuleProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutItems_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompareItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CompareListId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompareItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompareItems_CompareLists_CompareListId",
                        column: x => x.CompareListId,
                        principalTable: "CompareLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompareItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MediaAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaFileId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    AttachedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaAttachments_MediaFiles_MediaFileId",
                        column: x => x.MediaFileId,
                        principalTable: "MediaFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAttachments_ProductCategories_EntityId",
                        column: x => x.EntityId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAttachments_Products_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TaxPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductCategory_Mappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductCategory_Mappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_Mappings_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_Mappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductProductAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProductAttributes_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    RelatedProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRelations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRelations_Products_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTranslations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<long>(type: "bigint", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipmentItems_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissionPermissions",
                columns: table => new
                {
                    GroupPermissionId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissionPermissions", x => new { x.GroupPermissionId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_GroupPermissionPermissions_GroupPermissions_GroupPermissionId",
                        column: x => x.GroupPermissionId,
                        principalTable: "GroupPermissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupPermissionPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGroupPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GroupPermissionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroupPermissions_GroupPermissions_GroupPermissionId",
                        column: x => x.GroupPermissionId,
                        principalTable: "GroupPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartItemId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemTranslations_CartItems_CartItemId",
                        column: x => x.CartItemId,
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItemTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutItemId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutItemTranslations_CheckoutItems_CheckoutItemId",
                        column: x => x.CheckoutItemId,
                        principalTable: "CheckoutItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemTranslations_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductAttribute_ValueMappings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductProductAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    ProductAttributeValueId = table.Column<long>(type: "bigint", nullable: true),
                    CustomValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PriceAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WeightAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductAttribute_ValueMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_ValueMappings_ProductAttributeValues_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductProductAttribute_ValueMappings_ProductProductAttributes_ProductProductAttributeId",
                        column: x => x.ProductProductAttributeId,
                        principalTable: "ProductProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentItemId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentItemTranslations_ShipmentItems_ShipmentItemId",
                        column: x => x.ShipmentItemId,
                        principalTable: "ShipmentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentItemTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItemTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartItemId = table.Column<long>(type: "bigint", nullable: false),
                    WebSiteLanguageId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItemTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemTranslations_ShoppingCartItems_ShoppingCartItemId",
                        column: x => x.ShoppingCartItemId,
                        principalTable: "ShoppingCartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemTranslations_WebSiteLanguages_WebSiteLanguageId",
                        column: x => x.WebSiteLanguageId,
                        principalTable: "WebSiteLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1L, null, "Admin", "ADMIN" },
                    { 2L, null, "User", "USER" },
                    { 3L, null, "Developer", "DEVELOPER" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "fa", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Persian", null },
                    { 2L, "en", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "English", null },
                    { 3L, "es", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spanish", null },
                    { 4L, "fr", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "French", null },
                    { 5L, "de", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "German", null },
                    { 6L, "zh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Chinese (Mandarin)", null },
                    { 7L, "ar", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arabic", null },
                    { 8L, "ru", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Russian", null },
                    { 9L, "ja", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Japanese", null },
                    { 10L, "pt", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Portuguese", null },
                    { 11L, "hi", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hindi", null },
                    { 12L, "it", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Italian", null },
                    { 13L, "ko", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Korean", null },
                    { 14L, "nl", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dutch", null },
                    { 15L, "sv", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Swedish", null },
                    { 16L, "no", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Norwegian", null },
                    { 17L, "da", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Danish", null },
                    { 18L, "fi", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Finnish", null },
                    { 19L, "tr", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Turkish", null },
                    { 20L, "pl", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Polish", null },
                    { 21L, "he", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hebrew", null }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hicommerce", null });

            migrationBuilder.InsertData(
                table: "TopLevelDomains",
                columns: new[] { "Id", "CreatedAt", "Extension", "IsAllowed", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".com", true, null },
                    { 2L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".ir", true, null },
                    { 3L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".nl", true, null },
                    { 4L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".be", true, null },
                    { 5L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".de", true, null },
                    { 6L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".eu", true, null },
                    { 7L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".fr", true, null },
                    { 8L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".net", true, null },
                    { 9L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".info", true, null },
                    { 10L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".org", true, null },
                    { 11L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".biz", true, null },
                    { 12L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".couk", true, null },
                    { 13L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".nu", true, null },
                    { 14L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".tv", true, null },
                    { 15L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".co", true, null },
                    { 16L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".me", true, null },
                    { 17L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".us", true, null },
                    { 18L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".in", true, null },
                    { 19L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".ca", true, null },
                    { 20L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".com.co", true, null },
                    { 21L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".net.in", true, null },
                    { 22L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".ws", true, null },
                    { 23L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".se", true, null },
                    { 24L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".pro", true, null },
                    { 25L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".ac.ir", true, null },
                    { 26L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".shop", true, null },
                    { 27L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".co.ir", true, null },
                    { 28L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".com.au", true, null },
                    { 29L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".institute", true, null },
                    { 30L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".co.com", true, null },
                    { 31L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".net.co", true, null },
                    { 32L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".bio", true, null },
                    { 33L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".love", true, null },
                    { 34L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".online", true, null },
                    { 35L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".com.tr", true, null },
                    { 36L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".graphics", true, null },
                    { 37L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".it", true, null },
                    { 38L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".sch.ir", true, null },
                    { 39L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".ae", true, null },
                    { 40L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".holdings", true, null },
                    { 41L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".om", true, null },
                    { 42L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".click", true, null },
                    { 43L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), ".vip", true, null }
                });

            migrationBuilder.InsertData(
                table: "WebSites",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Description", "IsActive", "LogoUrl", "OwnerId", "UpdatedAt" },
                values: new object[] { 1L, "hicommerce", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WebSiteId" },
                values: new object[] { 1L, 0, "a8110c5e-04e0-4d5b-9c3a-3f3c594ed8d6", "razani.mohammad1@gmail.com", true, true, false, null, "RAZANI.MOHAMMAD1@GMAIL.COM", "DEVELOPER", "AQAAAAIAAYagAAAAEBVdxFugIr9Mk7NEqWRzjER3CNX1mZ2/+cGK9rifM/qnH4jaukCUX16iEUH+80kbiQ==", "09360735388", true, "f2b5b3b4-4a65-4d4d-a7d4-305f7a1e4f3e", false, "Developer", 1L });

            migrationBuilder.InsertData(
                table: "WebSiteDomains",
                columns: new[] { "Id", "CreatedAt", "DomainName", "IsDefault", "TldId", "UpdatedAt", "WebSiteId" },
                values: new object[] { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hicommerce", true, 2L, null, 1L });

            migrationBuilder.InsertData(
                table: "WebSiteLanguages",
                columns: new[] { "Id", "CreatedAt", "IsDefault", "LanguageId", "UpdatedAt", "WebSiteId" },
                values: new object[] { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 1L, null, 1L });

            migrationBuilder.InsertData(
                table: "WebSiteRoles",
                columns: new[] { "Id", "CreatedAt", "RoleId", "UpdatedAt", "WebSiteId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1L, null, 1L },
                    { 2L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2L, null, 1L },
                    { 3L, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3L, null, 1L }
                });

            migrationBuilder.InsertData(
                table: "WebSiteThemes",
                columns: new[] { "Id", "CreatedAt", "IsDefault", "ThemeId", "UpdatedAt", "WebSiteId" },
                values: new object[] { 1L, new DateTime(2025, 11, 5, 19, 9, 40, 72, DateTimeKind.Utc).AddTicks(1182), true, 1L, null, 1L });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 3L, 1L }
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
                name: "IX_AspNetUsers_WebSiteId",
                table: "AspNetUsers",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_ParentId",
                table: "BlogCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_WebSiteId",
                table: "BlogCategories",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryTranslations_BlogCategoryId_WebSiteLanguageId",
                table: "BlogCategoryTranslations",
                columns: new[] { "BlogCategoryId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryTranslations_Slug_WebSiteLanguageId",
                table: "BlogCategoryTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategoryTranslations_WebSiteLanguageId",
                table: "BlogCategoryTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategories_BlogCategoryId",
                table: "BlogPostCategories",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_WebSiteId",
                table: "BlogPosts",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTags_BlogTagId",
                table: "BlogPostTags",
                column: "BlogTagId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTranslations_BlogPostId_WebSiteLanguageId",
                table: "BlogPostTranslations",
                columns: new[] { "BlogPostId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTranslations_Slug_WebSiteLanguageId",
                table: "BlogPostTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTranslations_WebSiteLanguageId",
                table: "BlogPostTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_WebSiteId",
                table: "BlogTags",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagTranslations_BlogTagId_WebSiteLanguageId",
                table: "BlogTagTranslations",
                columns: new[] { "BlogTagId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagTranslations_Slug_WebSiteLanguageId",
                table: "BlogTagTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagTranslations_WebSiteLanguageId",
                table: "BlogTagTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_WebSiteId",
                table: "Brands",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslations_BrandId_WebSiteLanguageId",
                table: "BrandTranslations",
                columns: new[] { "BrandId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslations_Slug_WebSiteLanguageId",
                table: "BrandTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrandTranslations_WebSiteLanguageId",
                table: "BrandTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Cart_Product",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemTranslations_CartItem_Language",
                table: "CartItemTranslations",
                columns: new[] { "CartItemId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItemTranslations_WebSiteLanguageId",
                table: "CartItemTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleCategories_CartRuleId_CategoryId",
                table: "CartRuleCategories",
                columns: new[] { "CartRuleId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleCategories_CategoryId",
                table: "CartRuleCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleCustomerGroups_CartRuleId_CustomerGroupId",
                table: "CartRuleCustomerGroups",
                columns: new[] { "CartRuleId", "CustomerGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleCustomerGroups_CustomerGroupId",
                table: "CartRuleCustomerGroups",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleProducts_CartRuleId_ProductId",
                table: "CartRuleProducts",
                columns: new[] { "CartRuleId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleProducts_ProductId",
                table: "CartRuleProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleTranslations_CartRuleId_Culture",
                table: "CartRuleTranslations",
                columns: new[] { "CartRuleId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleUsages_CartRuleId_CustomerId_UsedOn",
                table: "CartRuleUsages",
                columns: new[] { "CartRuleId", "CustomerId", "UsedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_CartRuleUsages_CustomerId",
                table: "CartRuleUsages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_WebSite_Customer",
                table: "Carts",
                columns: new[] { "WebSiteId", "CustomerIdentifier" });

            migrationBuilder.CreateIndex(
                name: "IX_CartTranslations_Cart_Language",
                table: "CartTranslations",
                columns: new[] { "CartId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartTranslations_WebSiteLanguageId",
                table: "CartTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogRuleCustomerGroups_CatalogRuleId_CustomerGroupId",
                table: "CatalogRuleCustomerGroups",
                columns: new[] { "CatalogRuleId", "CustomerGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CatalogRuleCustomerGroups_CustomerGroupId",
                table: "CatalogRuleCustomerGroups",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogRules_Name",
                table: "CatalogRules",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogRuleTranslations_CatalogRuleId_Culture",
                table: "CatalogRuleTranslations",
                columns: new[] { "CatalogRuleId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutAddressTranslations_CheckoutAddressId",
                table: "CheckoutAddressTranslations",
                column: "CheckoutAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItems_CheckoutId",
                table: "CheckoutItems",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItems_ProductId",
                table: "CheckoutItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutItemTranslations_CheckoutItemId_WebSiteLanguageId",
                table: "CheckoutItemTranslations",
                columns: new[] { "CheckoutItemId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutPayments_CheckoutId",
                table: "CheckoutPayments",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutPaymentTranslations_CheckoutPaymentId",
                table: "CheckoutPaymentTranslations",
                column: "CheckoutPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CreatedById",
                table: "Checkouts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CustomerId",
                table: "Checkouts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutShippings_CheckoutId",
                table: "CheckoutShippings",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutShippingTranslations_CheckoutShippingId",
                table: "CheckoutShippingTranslations",
                column: "CheckoutShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutTranslations_CheckoutId_WebSiteLanguageId",
                table: "CheckoutTranslations",
                columns: new[] { "CheckoutId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompareItems_CompareListId",
                table: "CompareItems",
                column: "CompareListId");

            migrationBuilder.CreateIndex(
                name: "IX_CompareItems_ProductId_CompareListId",
                table: "CompareItems",
                columns: new[] { "ProductId", "CompareListId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompareLists_CustomerId_WebsiteId",
                table: "CompareLists",
                columns: new[] { "CustomerId", "WebsiteId" });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CartRuleId",
                table: "Coupons",
                column: "CartRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponTranslations_CouponId_Culture",
                table: "CouponTranslations",
                columns: new[] { "CouponId", "Culture" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_CouponId_CustomerId",
                table: "CouponUsages",
                columns: new[] { "CouponId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_CustomerId",
                table: "CouponUsages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountTranslations_DiscountId",
                table: "DiscountTranslations",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissionPermissions_PermissionId",
                table: "GroupPermissionPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_WebSiteRoleId",
                table: "GroupPermissions",
                column: "WebSiteRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAttachments_EntityId",
                table: "MediaAttachments",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAttachments_EntityType_EntityId",
                table: "MediaAttachments",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaAttachments_EntityType_EntityId_Purpose",
                table: "MediaAttachments",
                columns: new[] { "EntityType", "EntityId", "Purpose" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaAttachments_MediaFileId",
                table: "MediaAttachments",
                column: "MediaFileId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_Key",
                table: "MediaFiles",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentId",
                table: "MenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemTranslations_MenuItemId_WebSiteLanguageId",
                table: "MenuItemTranslations",
                columns: new[] { "MenuItemId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemTranslations_WebSiteLanguageId",
                table: "MenuItemTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_WebSiteId",
                table: "Menus",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddressTranslations_OrderAddressId",
                table: "OrderAddressTranslations",
                column: "OrderAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAddressTranslations_WebSiteLanguageId",
                table: "OrderAddressTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_CreatedById",
                table: "OrderHistories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderId",
                table: "OrderHistories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryTranslations_OrderHistoryId",
                table: "OrderHistoryTranslations",
                column: "OrderHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryTranslations_WebSiteLanguageId",
                table: "OrderHistoryTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTranslations_OrderItemId",
                table: "OrderItemTranslations",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTranslations_WebSiteLanguageId",
                table: "OrderItemTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedById",
                table: "Orders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LatestUpdatedById",
                table: "Orders",
                column: "LatestUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ParentId",
                table: "Orders",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTranslations_OrderId",
                table: "OrderTranslations",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTranslations_WebSiteLanguageId",
                table: "OrderTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_WebSiteId",
                table: "Page",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTranslation_PageId",
                table: "PageTranslation",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageTranslation_WebSiteLanguageId",
                table: "PageTranslation",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderTranslations_PaymentProviderId",
                table: "PaymentProviderTranslations",
                column: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderTranslations_WebSiteLanguageId",
                table: "PaymentProviderTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTranslations_PaymentId",
                table: "PaymentTranslations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTranslations_WebSiteLanguageId",
                table: "PaymentTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Area_Controller_Action",
                table: "Permissions",
                columns: new[] { "Area", "Controller", "Action" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategory_Mappings_ProductCategoryId",
                table: "Product_ProductCategory_Mappings",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategory_Mappings_ProductId",
                table: "Product_ProductCategory_Mappings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AllowMultiple",
                table: "ProductAttributes",
                column: "AllowMultipleValues");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_WebSiteId",
                table: "ProductAttributes",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_Language_Unique",
                table: "ProductAttributeTranslations",
                columns: new[] { "ProductAttributeId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeTranslations_WebSiteLanguageId",
                table: "ProductAttributeTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Active",
                table: "ProductAttributeValues",
                columns: new[] { "ProductAttributeId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_DisplayOrder",
                table: "ProductAttributeValues",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValue_Language_Unique",
                table: "ProductAttributeValueTranslations",
                columns: new[] { "ProductAttributeValueId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValueTranslations_WebSiteLanguageId",
                table: "ProductAttributeValueTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Id",
                table: "ProductCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentId",
                table: "ProductCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_WebSiteId",
                table: "ProductCategories",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTranslations_ProductCategoryId_WebSiteLanguageId",
                table: "ProductCategoryTranslations",
                columns: new[] { "ProductCategoryId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTranslations_Slug_WebSiteLanguageId",
                table: "ProductCategoryTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategoryTranslations_WebSiteLanguageId",
                table: "ProductCategoryTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValueId",
                table: "ProductProductAttribute_ValueMappings",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_CustomValue",
                table: "ProductProductAttribute_ValueMappings",
                columns: new[] { "ProductProductAttributeId", "CustomValue" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttribute_Value",
                table: "ProductProductAttribute_ValueMappings",
                columns: new[] { "ProductProductAttributeId", "ProductAttributeValueId" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductAttribute_Unique",
                table: "ProductProductAttributes",
                columns: new[] { "ProductId", "ProductAttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductAttributes_ProductAttributeId",
                table: "ProductProductAttributes",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelations_ProductId_RelatedProductId",
                table: "ProductRelations",
                columns: new[] { "ProductId", "RelatedProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelations_RelatedProductId",
                table: "ProductRelations",
                column: "RelatedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId1",
                table: "Products",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Id",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxId",
                table: "Products",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WebSiteId",
                table: "Products",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_ProductId_WebSiteLanguageId",
                table: "ProductTranslations",
                columns: new[] { "ProductId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_Slug_WebSiteLanguageId",
                table: "ProductTranslations",
                columns: new[] { "Slug", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTranslations_WebSiteLanguageId",
                table: "ProductTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItems_ProductId",
                table: "ShipmentItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItems_ShipmentId",
                table: "ShipmentItems",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItemTranslations_ShipmentItemId",
                table: "ShipmentItemTranslations",
                column: "ShipmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItemTranslations_WebSiteLanguageId",
                table: "ShipmentItemTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CreatedById",
                table: "Shipments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTranslations_ShipmentId",
                table: "ShipmentTranslations",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTranslations_WebSiteLanguageId",
                table: "ShipmentTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingFreeRules_ShippingId",
                table: "ShippingFreeRules",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingFreeTranslations_ShippingFreeId",
                table: "ShippingFreeTranslations",
                column: "ShippingFreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingFreeTranslations_WebSiteLanguageId",
                table: "ShippingFreeTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethods_ShippingProviderId",
                table: "ShippingMethods",
                column: "ShippingProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethodTranslations_ShippingMethodId",
                table: "ShippingMethodTranslations",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethodTranslations_WebSiteLanguageId",
                table: "ShippingMethodTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviderTranslations_ShippingProviderId_WebSiteLanguageId",
                table: "ShippingProviderTranslations",
                columns: new[] { "ShippingProviderId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviderTranslations_WebSiteLanguageId",
                table: "ShippingProviderTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRates_ShippingId",
                table: "ShippingRates",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRateTranslations_ShippingRateId_WebSiteLanguageId",
                table: "ShippingRateTranslations",
                columns: new[] { "ShippingRateId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRateTranslations_WebSiteLanguageId",
                table: "ShippingRateTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingTranslations_ShippingId_WebSiteLanguageId",
                table: "ShippingTranslations",
                columns: new[] { "ShippingId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingTranslations_WebSiteLanguageId",
                table: "ShippingTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemTranslations_ShoppingCartItemId_WebSiteLanguageId",
                table: "ShoppingCartItemTranslations",
                columns: new[] { "ShoppingCartItemId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemTranslations_WebSiteLanguageId",
                table: "ShoppingCartItemTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CreatedById",
                table: "ShoppingCarts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerId",
                table: "ShoppingCarts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTranslations_ShoppingCartId_WebSiteLanguageId",
                table: "ShoppingCartTranslations",
                columns: new[] { "ShoppingCartId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartTranslations_WebSiteLanguageId",
                table: "ShoppingCartTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxs_WebSiteId",
                table: "Taxs",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTranslations_TaxId_WebSiteLanguageId",
                table: "TaxTranslations",
                columns: new[] { "TaxId", "WebSiteLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxTranslations_WebSiteLanguageId",
                table: "TaxTranslations",
                column: "WebSiteLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupPermissions_GroupPermissionId",
                table: "UserGroupPermissions",
                column: "GroupPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupPermissions_UserId_GroupPermissionId",
                table: "UserGroupPermissions",
                columns: new[] { "UserId", "GroupPermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteDomains_DomainName_TldId_WebSiteId",
                table: "WebSiteDomains",
                columns: new[] { "DomainName", "TldId", "WebSiteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteDomains_TldId",
                table: "WebSiteDomains",
                column: "TldId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteDomains_WebSiteId",
                table: "WebSiteDomains",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteLanguages_LanguageId",
                table: "WebSiteLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteLanguages_WebSiteId_IsDefault",
                table: "WebSiteLanguages",
                columns: new[] { "WebSiteId", "IsDefault" },
                unique: true,
                filter: "[IsDefault] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteRoles_RoleId",
                table: "WebSiteRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteRoles_WebSiteId",
                table: "WebSiteRoles",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSites_CompanyName",
                table: "WebSites",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_WebSites_OwnerId",
                table: "WebSites",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSite_DefaultTheme",
                table: "WebSiteThemes",
                columns: new[] { "WebSiteId", "IsDefault" });

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteThemes_ThemeId",
                table: "WebSiteThemes",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSiteThemes_WebSiteId",
                table: "WebSiteThemes",
                column: "WebSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId_ProductId",
                table: "WishlistItems",
                columns: new[] { "WishlistId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CustomerId_WebsiteId",
                table: "Wishlists",
                columns: new[] { "CustomerId", "WebsiteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_WebSites_WebSiteId",
                table: "AspNetUsers",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebSites_AspNetUsers_OwnerId",
                table: "WebSites");

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
                name: "BlogCategoryTranslations");

            migrationBuilder.DropTable(
                name: "BlogPostCategories");

            migrationBuilder.DropTable(
                name: "BlogPostTags");

            migrationBuilder.DropTable(
                name: "BlogPostTranslations");

            migrationBuilder.DropTable(
                name: "BlogTagTranslations");

            migrationBuilder.DropTable(
                name: "BrandTranslations");

            migrationBuilder.DropTable(
                name: "CartItemTranslations");

            migrationBuilder.DropTable(
                name: "CartRuleCategories");

            migrationBuilder.DropTable(
                name: "CartRuleCustomerGroups");

            migrationBuilder.DropTable(
                name: "CartRuleProducts");

            migrationBuilder.DropTable(
                name: "CartRuleTranslations");

            migrationBuilder.DropTable(
                name: "CartRuleUsages");

            migrationBuilder.DropTable(
                name: "CartTranslations");

            migrationBuilder.DropTable(
                name: "CatalogRuleCustomerGroups");

            migrationBuilder.DropTable(
                name: "CatalogRuleTranslations");

            migrationBuilder.DropTable(
                name: "CheckoutAddressTranslations");

            migrationBuilder.DropTable(
                name: "CheckoutItemTranslations");

            migrationBuilder.DropTable(
                name: "CheckoutPaymentTranslations");

            migrationBuilder.DropTable(
                name: "CheckoutShippingTranslations");

            migrationBuilder.DropTable(
                name: "CheckoutTranslations");

            migrationBuilder.DropTable(
                name: "CompareItems");

            migrationBuilder.DropTable(
                name: "CouponTranslations");

            migrationBuilder.DropTable(
                name: "CouponUsages");

            migrationBuilder.DropTable(
                name: "DiscountTranslations");

            migrationBuilder.DropTable(
                name: "GroupPermissionPermissions");

            migrationBuilder.DropTable(
                name: "MediaAttachments");

            migrationBuilder.DropTable(
                name: "MenuItemTranslations");

            migrationBuilder.DropTable(
                name: "OrderAddressTranslations");

            migrationBuilder.DropTable(
                name: "OrderHistoryTranslations");

            migrationBuilder.DropTable(
                name: "OrderItemTranslations");

            migrationBuilder.DropTable(
                name: "OrderTranslations");

            migrationBuilder.DropTable(
                name: "PageTranslation");

            migrationBuilder.DropTable(
                name: "PaymentProviderTranslations");

            migrationBuilder.DropTable(
                name: "PaymentTranslations");

            migrationBuilder.DropTable(
                name: "Product_ProductCategory_Mappings");

            migrationBuilder.DropTable(
                name: "ProductAttributeTranslations");

            migrationBuilder.DropTable(
                name: "ProductAttributeValueTranslations");

            migrationBuilder.DropTable(
                name: "ProductCategoryTranslations");

            migrationBuilder.DropTable(
                name: "ProductProductAttribute_ValueMappings");

            migrationBuilder.DropTable(
                name: "ProductRelations");

            migrationBuilder.DropTable(
                name: "ProductTranslations");

            migrationBuilder.DropTable(
                name: "ShipmentItemTranslations");

            migrationBuilder.DropTable(
                name: "ShipmentTranslations");

            migrationBuilder.DropTable(
                name: "ShippingFreeTranslations");

            migrationBuilder.DropTable(
                name: "ShippingMethodTranslations");

            migrationBuilder.DropTable(
                name: "ShippingProviderTranslations");

            migrationBuilder.DropTable(
                name: "ShippingRateTranslations");

            migrationBuilder.DropTable(
                name: "ShippingTranslations");

            migrationBuilder.DropTable(
                name: "ShoppingCartItemTranslations");

            migrationBuilder.DropTable(
                name: "ShoppingCartTranslations");

            migrationBuilder.DropTable(
                name: "TaxTranslations");

            migrationBuilder.DropTable(
                name: "UserGroupPermissions");

            migrationBuilder.DropTable(
                name: "WebSiteDomains");

            migrationBuilder.DropTable(
                name: "WebSiteThemes");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "CatalogRules");

            migrationBuilder.DropTable(
                name: "CheckoutAddresses");

            migrationBuilder.DropTable(
                name: "CheckoutItems");

            migrationBuilder.DropTable(
                name: "CheckoutPayments");

            migrationBuilder.DropTable(
                name: "CheckoutShippings");

            migrationBuilder.DropTable(
                name: "CompareLists");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "ProductProductAttributes");

            migrationBuilder.DropTable(
                name: "ShipmentItems");

            migrationBuilder.DropTable(
                name: "ShippingFreeRules");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "ShippingRates");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "WebSiteLanguages");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "TopLevelDomains");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropTable(
                name: "CartRules");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "ShippingProviders");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "WebSiteRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Taxs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OrderAddresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "WebSites");
        }
    }
}
