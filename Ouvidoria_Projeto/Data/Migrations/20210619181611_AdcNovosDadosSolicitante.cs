using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class AdcNovosDadosSolicitante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Solicitantes");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Solicitantes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Solicitantes");

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "Solicitantes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
