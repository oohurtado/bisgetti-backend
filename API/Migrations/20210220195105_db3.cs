using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuJson",
                table: "Settings",
                newName: "MenuProductsJson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuProductsJson",
                table: "Settings",
                newName: "MenuJson");
        }
    }
}
