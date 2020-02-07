using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_2019_06_25.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AkademskaGodina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AkademskaGodina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ECTS = table.Column<int>(nullable: false),
                    Godina = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Sifra = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Angazovan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AkademskaGodinaId = table.Column<int>(nullable: false),
                    NastavnikId = table.Column<int>(nullable: false),
                    PredmetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angazovan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angazovan_AkademskaGodina_AkademskaGodinaId",
                        column: x => x.AkademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Angazovan_Nastavnik_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Angazovan_Predmet_PredmetId",
                        column: x => x.PredmetId,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UpisGodine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AkademskaGodinaId = table.Column<int>(nullable: false),
                    DatumUpisa = table.Column<DateTime>(nullable: false),
                    GodinaStudija = table.Column<int>(nullable: false),
                    PolozioECTS = table.Column<int>(nullable: false),
                    SlusaECTS = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisGodine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisGodine_AkademskaGodina_AkademskaGodinaId",
                        column: x => x.AkademskaGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UpisGodine_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ispit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovanId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Napomena = table.Column<string>(nullable: true),
                    Zakljucano = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ispit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ispit_Angazovan_AngazovanId",
                        column: x => x.AngazovanId,
                        principalTable: "Angazovan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OdrzaniCas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovaniId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzaniCas_Angazovan_AngazovaniId",
                        column: x => x.AngazovaniId,
                        principalTable: "Angazovan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlusaPredmet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovanId = table.Column<int>(nullable: false),
                    DatumOcjene = table.Column<DateTime>(nullable: true),
                    Ocjena = table.Column<int>(nullable: true),
                    UpisGodineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlusaPredmet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SlusaPredmet_Angazovan_AngazovanId",
                        column: x => x.AngazovanId,
                        principalTable: "Angazovan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SlusaPredmet_UpisGodine_UpisGodineId",
                        column: x => x.UpisGodineId,
                        principalTable: "UpisGodine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IspitStavka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<int>(nullable: false),
                    IsPristupio = table.Column<bool>(nullable: false),
                    IspitId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IspitStavka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IspitStavka_Ispit_IspitId",
                        column: x => x.IspitId,
                        principalTable: "Ispit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IspitStavka_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OdrzaniCasDetalji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BodoviNaCasu = table.Column<int>(nullable: false),
                    OdrzaniCasoviId = table.Column<int>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false),
                    SlusaPredmeteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCasDetalji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasDetalji_OdrzaniCas_OdrzaniCasoviId",
                        column: x => x.OdrzaniCasoviId,
                        principalTable: "OdrzaniCas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasDetalji_SlusaPredmet_SlusaPredmeteId",
                        column: x => x.SlusaPredmeteId,
                        principalTable: "SlusaPredmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_AkademskaGodinaId",
                table: "Angazovan",
                column: "AkademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_NastavnikId",
                table: "Angazovan",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_PredmetId",
                table: "Angazovan",
                column: "PredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ispit_AngazovanId",
                table: "Ispit",
                column: "AngazovanId");

            migrationBuilder.CreateIndex(
                name: "IX_IspitStavka_IspitId",
                table: "IspitStavka",
                column: "IspitId");

            migrationBuilder.CreateIndex(
                name: "IX_IspitStavka_StudentId",
                table: "IspitStavka",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCas_AngazovaniId",
                table: "OdrzaniCas",
                column: "AngazovaniId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalji_OdrzaniCasoviId",
                table: "OdrzaniCasDetalji",
                column: "OdrzaniCasoviId");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalji_SlusaPredmeteId",
                table: "OdrzaniCasDetalji",
                column: "SlusaPredmeteId");

            migrationBuilder.CreateIndex(
                name: "IX_SlusaPredmet_AngazovanId",
                table: "SlusaPredmet",
                column: "AngazovanId");

            migrationBuilder.CreateIndex(
                name: "IX_SlusaPredmet_UpisGodineId",
                table: "SlusaPredmet",
                column: "UpisGodineId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisGodine_AkademskaGodinaId",
                table: "UpisGodine",
                column: "AkademskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisGodine_StudentId",
                table: "UpisGodine",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IspitStavka");

            migrationBuilder.DropTable(
                name: "OdrzaniCasDetalji");

            migrationBuilder.DropTable(
                name: "Ispit");

            migrationBuilder.DropTable(
                name: "OdrzaniCas");

            migrationBuilder.DropTable(
                name: "SlusaPredmet");

            migrationBuilder.DropTable(
                name: "Angazovan");

            migrationBuilder.DropTable(
                name: "UpisGodine");

            migrationBuilder.DropTable(
                name: "Nastavnik");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "AkademskaGodina");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
