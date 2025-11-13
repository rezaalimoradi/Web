using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductCategoryChildParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENIPTsxoMSAsvjveay6PKV2i4XNXJIXWR7E8IiEQ3n3Gj9F86aVUhPWfeAqGh6gFgw==");

            migrationBuilder.UpdateData(
                table: "WebSiteThemes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 7, 6, 8, 18, 239, DateTimeKind.Utc).AddTicks(2927));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBVdxFugIr9Mk7NEqWRzjER3CNX1mZ2/+cGK9rifM/qnH4jaukCUX16iEUH+80kbiQ==");

            migrationBuilder.UpdateData(
                table: "WebSiteThemes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2025, 11, 5, 19, 9, 40, 72, DateTimeKind.Utc).AddTicks(1182));
        }
    }
}
