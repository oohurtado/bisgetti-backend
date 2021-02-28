using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuMsgDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "MenuMsgTitle",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "MenuMessagesJson",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuMessagesJson",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "MenuMsgDescription",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuMsgTitle",
                table: "Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
