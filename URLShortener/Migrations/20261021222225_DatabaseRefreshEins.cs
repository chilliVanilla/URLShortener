using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRefreshEins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortLinks",
                table: "ShortLinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShortLinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortLinks",
                table: "ShortLinks",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortLinks",
                table: "ShortLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ShortLinks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortLinks",
                table: "ShortLinks",
                column: "Id");
        }
    }
}
