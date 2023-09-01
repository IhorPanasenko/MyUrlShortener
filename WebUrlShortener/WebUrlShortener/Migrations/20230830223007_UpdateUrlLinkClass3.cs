using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlLinkClass3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66d5522c-7bb6-4ed5-be57-f256ce2e5414");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aaf9a20f-c44b-4e12-8c03-871c694769b6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59a56c6f-9859-407a-a9f7-7d4e1c69fab8", "1", "Admin", "ADMIN" },
                    { "de9387e2-5290-430d-9b19-6b3b82c3f89a", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59a56c6f-9859-407a-a9f7-7d4e1c69fab8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de9387e2-5290-430d-9b19-6b3b82c3f89a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66d5522c-7bb6-4ed5-be57-f256ce2e5414", "1", "Admin", "ADMIN" },
                    { "aaf9a20f-c44b-4e12-8c03-871c694769b6", "2", "User", "USER" }
                });
        }
    }
}
