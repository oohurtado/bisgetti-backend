using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackChangesId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackChangesId",
                table: "Products");
        }
    }
}
