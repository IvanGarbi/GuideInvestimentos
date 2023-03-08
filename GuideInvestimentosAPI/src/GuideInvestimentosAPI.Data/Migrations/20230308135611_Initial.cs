using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideInvestimentosAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Symbol = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Currency = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset");
        }
    }
}
