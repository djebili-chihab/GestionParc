using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionNonconformite_NCARouiba.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreuveKM",
                table: "demandeCarburants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreuveKM",
                table: "demandeCarburants");
        }
    }
}
