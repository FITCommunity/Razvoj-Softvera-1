using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_2019_06_25.Migrations
{
    public partial class renameBodoviToOcjena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bodovi",
                table: "IspitStavka",
                newName: "Ocjena");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ocjena",
                table: "IspitStavka",
                newName: "Bodovi");
        }
    }
}
