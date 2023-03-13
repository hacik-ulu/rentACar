using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACarProject.Migrations
{
    /// <inheritdoc />
    public partial class news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "request",
                table: "Müşteriler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "request",
                table: "Müşteriler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
