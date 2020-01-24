using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_2019_12_02.Migrations
{
    public partial class popravniIspit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopravniIspit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumPopravnog = table.Column<DateTime>(nullable: false),
                    OdjeljenjeId = table.Column<int>(nullable: false),
                    PredmetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Odjeljenje_OdjeljenjeId",
                        column: x => x.OdjeljenjeId,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PopravniIspitStavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<int>(nullable: true),
                    OdjeljenjeStavkaId = table.Column<int>(nullable: false),
                    PopravniIspitId = table.Column<int>(nullable: false),
                    Pristupio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspitStavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopravniIspitStavke_OdjeljenjeStavka_OdjeljenjeStavkaId",
                        column: x => x.OdjeljenjeStavkaId,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspitStavke_PopravniIspit_PopravniIspitId",
                        column: x => x.PopravniIspitId,
                        principalTable: "PopravniIspit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_OdjeljenjeId",
                table: "PopravniIspit",
                column: "OdjeljenjeId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_PredmetId",
                table: "PopravniIspit",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitStavke_OdjeljenjeStavkaId",
                table: "PopravniIspitStavke",
                column: "OdjeljenjeStavkaId");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitStavke_PopravniIspitId",
                table: "PopravniIspitStavke",
                column: "PopravniIspitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopravniIspitStavke");

            migrationBuilder.DropTable(
                name: "PopravniIspit");
        }
    }
}
