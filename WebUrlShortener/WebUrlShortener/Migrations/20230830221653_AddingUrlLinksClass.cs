using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class AddingUrlLinksClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "668f6d6d-cf14-45b3-ac4f-1b1f63a1fbec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c07c3715-674c-4359-a695-fbd1340b6a02");

            migrationBuilder.CreateTable(
                name: "UrlLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LongUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlLinks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9f438e4e-8695-4f0e-a076-ecaf998cc5b3", "1", "Admin", "ADMIN" },
                    { "d8b6a4b0-60c1-4237-9f61-28166813232c", "2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlLinks_UserId",
                table: "UrlLinks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f438e4e-8695-4f0e-a076-ecaf998cc5b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8b6a4b0-60c1-4237-9f61-28166813232c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "668f6d6d-cf14-45b3-ac4f-1b1f63a1fbec", "1", "Admin", "ADMIN" },
                    { "c07c3715-674c-4359-a695-fbd1340b6a02", "2", "User", "USER" }
                });
        }
    }
}
