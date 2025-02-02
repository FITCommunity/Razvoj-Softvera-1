using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_25.API.Migrations
{
    /// <inheritdoc />
    public partial class Semester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Professors_ProfesorId",
                table: "Semesters");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_MyAppUsers_ProfesorId",
                table: "Semesters",
                column: "ProfesorId",
                principalTable: "MyAppUsers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_MyAppUsers_ProfesorId",
                table: "Semesters");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Professors_ProfesorId",
                table: "Semesters",
                column: "ProfesorId",
                principalTable: "Professors",
                principalColumn: "ID");
        }
    }
}
