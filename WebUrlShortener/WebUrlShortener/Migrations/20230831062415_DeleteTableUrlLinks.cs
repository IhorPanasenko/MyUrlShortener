using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTableUrlLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "078793ea-ca1b-437f-b183-c4ed64d62246");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f6fa0d7-3262-4966-9126-0faa38cfae85");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlLinks",
                columns: table => new
                {
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlLinks", x => x.UrlId);
                    table.ForeignKey(
                        name: "FK_UrlLinks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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
        }
    }
}
