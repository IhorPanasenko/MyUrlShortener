using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class FixUrlLinkClass : Migration
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

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UrlLinks",
                newName: "UrlId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UrlLinks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "078793ea-ca1b-437f-b183-c4ed64d62246", "2", "User", "USER" },
                    { "8f6fa0d7-3262-4966-9126-0faa38cfae85", "1", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlLinks_AppUserId",
                table: "UrlLinks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlLinks_AspNetUsers_AppUserId",
                table: "UrlLinks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlLinks_AspNetUsers_AppUserId",
                table: "UrlLinks");

            migrationBuilder.DropIndex(
                name: "IX_UrlLinks_AppUserId",
                table: "UrlLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "078793ea-ca1b-437f-b183-c4ed64d62246");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f6fa0d7-3262-4966-9126-0faa38cfae85");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UrlLinks");

            migrationBuilder.RenameColumn(
                name: "UrlId",
                table: "UrlLinks",
                newName: "Id");

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
