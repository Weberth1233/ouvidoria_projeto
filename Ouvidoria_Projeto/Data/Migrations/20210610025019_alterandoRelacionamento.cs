using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class alterandoRelacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resposta_ManifestoId",
                table: "Resposta");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_ManifestoId",
                table: "Resposta",
                column: "ManifestoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resposta_ManifestoId",
                table: "Resposta");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_ManifestoId",
                table: "Resposta",
                column: "ManifestoId");
        }
    }
}
