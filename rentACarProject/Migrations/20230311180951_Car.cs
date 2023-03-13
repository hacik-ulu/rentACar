using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACarProject.Migrations
{
    /// <inheritdoc />
    public partial class Car : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    carYear = table.Column<int>(type: "int", nullable: false),
                    carModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rentPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
