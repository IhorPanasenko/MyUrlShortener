using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlLinkClass2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlLinks_AspNetUsers_UserId1",
                table: "UrlLinks");

            migrationBuilder.DropIndex(
                name: "IX_UrlLinks_UserId1",
                table: "UrlLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4584688a-6f4d-4eff-b5ec-d69194f168f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de8d3e7f-e275-4054-88b6-922d29636b1f");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UrlLinks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66d5522c-7bb6-4ed5-be57-f256ce2e5414", "1", "Admin", "ADMIN" },
                    { "aaf9a20f-c44b-4e12-8c03-871c694769b6", "2", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66d5522c-7bb6-4ed5-be57-f256ce2e5414");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aaf9a20f-c44b-4e12-8c03-871c694769b6");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UrlLinks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4584688a-6f4d-4eff-b5ec-d69194f168f0", "1", "Admin", "ADMIN" },
                    { "de8d3e7f-e275-4054-88b6-922d29636b1f", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlLinks_UserId1",
                table: "UrlLinks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlLinks_AspNetUsers_UserId1",
                table: "UrlLinks",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
