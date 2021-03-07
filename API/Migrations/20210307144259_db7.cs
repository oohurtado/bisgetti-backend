using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuVersion",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuVersion",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
