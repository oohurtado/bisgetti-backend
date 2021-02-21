using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuMsgExtra",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuMsgExtra",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
