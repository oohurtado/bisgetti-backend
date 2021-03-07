using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuMessagesJson",
                table: "Settings",
                newName: "MenuTitlesJson");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuTitlesJson",
                table: "Settings",
                newName: "MenuMessagesJson");
        }
    }
}
