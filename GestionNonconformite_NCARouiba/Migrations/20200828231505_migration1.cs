using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionNonconformite_NCARouiba.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateControlTech",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExpirationPermis",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OrigineNC",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OriginName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrigineNC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointDetectionNC",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PointDetectionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointDetectionNC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Matricule = table.Column<string>(nullable: true),
                    DateAssurance = table.Column<DateTime>(nullable: false),
                    DateControlTech = table.Column<DateTime>(nullable: false),
                    DateScannaire = table.Column<DateTime>(nullable: false),
                    marque = table.Column<string>(nullable: true),
                    Categorie = table.Column<string>(nullable: true),
                    Kilometrage = table.Column<string>(nullable: true),
                    Carburant = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Statue = table.Column<string>(nullable: true),
                    EnMission = table.Column<string>(nullable: true),
                    NumChassis = table.Column<string>(nullable: true),
                    Couleur = table.Column<string>(nullable: true),
                    model = table.Column<string>(nullable: true),
                    Nature = table.Column<int>(nullable: false),
                    OrigineId = table.Column<string>(nullable: true),
                    PointDetectionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_OrigineNC_OrigineId",
                        column: x => x.OrigineId,
                        principalTable: "OrigineNC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cars_PointDetectionNC_PointDetectionId",
                        column: x => x.PointDetectionId,
                        principalTable: "PointDetectionNC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_cars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "demandeCarburants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NouveauKM = table.Column<long>(nullable: false),
                    Commentaire = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demandeCarburants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demandeCarburants_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_demandeCarburants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "demandeEntretiens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Urgence = table.Column<string>(nullable: true),
                    Commentaire = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demandeEntretiens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_demandeEntretiens_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_demandeEntretiens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_OrigineId",
                table: "cars",
                column: "OrigineId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_PointDetectionId",
                table: "cars",
                column: "PointDetectionId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_UserId",
                table: "cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_demandeCarburants_CarId",
                table: "demandeCarburants",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_demandeCarburants_UserId",
                table: "demandeCarburants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_demandeEntretiens_CarId",
                table: "demandeEntretiens",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_demandeEntretiens_UserId",
                table: "demandeEntretiens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "demandeCarburants");

            migrationBuilder.DropTable(
                name: "demandeEntretiens");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "OrigineNC");

            migrationBuilder.DropTable(
                name: "PointDetectionNC");

            migrationBuilder.DropColumn(
                name: "DateControlTech",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateExpirationPermis",
                table: "AspNetUsers");
        }
    }
}
