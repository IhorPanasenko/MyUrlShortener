using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebUrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlLinkClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlLinks_AspNetUsers_UserId",
                table: "UrlLinks");

            migrationBuilder.DropIndex(
                name: "IX_UrlLinks_UserId",
                table: "UrlLinks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f438e4e-8695-4f0e-a076-ecaf998cc5b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8b6a4b0-60c1-4237-9f61-28166813232c");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UrlLinks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UrlLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UrlLinks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "UrlLinks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UrlLinks_AspNetUsers_UserId",
                table: "UrlLinks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
