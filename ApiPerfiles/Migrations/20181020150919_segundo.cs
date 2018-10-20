using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPerfiles.Migrations
{
    public partial class segundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Aplicaciones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Aplicaciones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Aplicaciones");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Aplicaciones");
        }
    }
}
