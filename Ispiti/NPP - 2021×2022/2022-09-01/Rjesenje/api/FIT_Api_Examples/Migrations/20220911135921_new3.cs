using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class new3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datum1_ZimskiOvjera",
                table: "UpisUAkGodinu",
                newName: "datum1_ZimskiUpis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datum1_ZimskiUpis",
                table: "UpisUAkGodinu",
                newName: "datum1_ZimskiOvjera");
        }
    }
}
