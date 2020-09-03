using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionNonconformite_NCARouiba.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_OrigineNC_OrigineId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_PointDetectionNC_PointDetectionId",
                table: "cars");

            migrationBuilder.DropTable(
                name: "OrigineNC");

            migrationBuilder.DropTable(
                name: "PointDetectionNC");

            migrationBuilder.DropIndex(
                name: "IX_cars_OrigineId",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_PointDetectionId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "Nature",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "OrigineId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "PointDetectionId",
                table: "cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nature",
                table: "cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrigineId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointDetectionId",
                table: "cars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrigineNC",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrigineNC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointDetectionNC",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PointDetectionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointDetectionNC", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_OrigineId",
                table: "cars",
                column: "OrigineId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_PointDetectionId",
                table: "cars",
                column: "PointDetectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_OrigineNC_OrigineId",
                table: "cars",
                column: "OrigineId",
                principalTable: "OrigineNC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_PointDetectionNC_PointDetectionId",
                table: "cars",
                column: "PointDetectionId",
                principalTable: "PointDetectionNC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
