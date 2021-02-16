using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Person_Id",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Person_Id_Name",
                table: "Addresses",
                columns: new[] { "Person_Id", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_Person_Id_Name",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Person_Id",
                table: "Addresses",
                column: "Person_Id");
        }
    }
}
