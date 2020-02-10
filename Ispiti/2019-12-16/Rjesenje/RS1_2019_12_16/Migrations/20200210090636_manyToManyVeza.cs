using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_2019_12_16.Migrations
{
    public partial class manyToManyVeza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Komisija",
                table: "Komisija");

            migrationBuilder.DropIndex(
                name: "IX_Komisija_PopravniIspitId",
                table: "Komisija");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Komisija");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Komisija",
                table: "Komisija",
                columns: new[] { "PopravniIspitId", "NastavnikId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Komisija",
                table: "Komisija");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Komisija",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Komisija",
                table: "Komisija",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Komisija_PopravniIspitId",
                table: "Komisija",
                column: "PopravniIspitId");
        }
    }
}
