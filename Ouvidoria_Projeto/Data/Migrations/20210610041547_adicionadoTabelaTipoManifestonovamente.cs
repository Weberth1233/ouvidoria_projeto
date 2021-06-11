using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class adicionadoTabelaTipoManifestonovamente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manifesto_TipoManifesto_TipoManifestoId",
                table: "Manifesto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoManifesto",
                table: "TipoManifesto");

            migrationBuilder.RenameTable(
                name: "TipoManifesto",
                newName: "TipoManifestos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoManifestos",
                table: "TipoManifestos",
                column: "TipoManifestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manifesto_TipoManifestos_TipoManifestoId",
                table: "Manifesto",
                column: "TipoManifestoId",
                principalTable: "TipoManifestos",
                principalColumn: "TipoManifestoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manifesto_TipoManifestos_TipoManifestoId",
                table: "Manifesto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoManifestos",
                table: "TipoManifestos");

            migrationBuilder.RenameTable(
                name: "TipoManifestos",
                newName: "TipoManifesto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoManifesto",
                table: "TipoManifesto",
                column: "TipoManifestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manifesto_TipoManifesto_TipoManifestoId",
                table: "Manifesto",
                column: "TipoManifestoId",
                principalTable: "TipoManifesto",
                principalColumn: "TipoManifestoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
