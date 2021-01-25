using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_People_Person_Id",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_Person_Id",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Person_Id",
                table: "Settings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Person_Id",
                table: "Settings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Person_Id",
                table: "Settings",
                column: "Person_Id",
                unique: true,
                filter: "[Person_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_People_Person_Id",
                table: "Settings",
                column: "Person_Id",
                principalTable: "People",
                principalColumn: "Person_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
