using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class adicionadoTabelaTipoManifesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoManifesto",
                table: "Manifesto");

            migrationBuilder.AddColumn<int>(
                name: "TipoManifestoId",
                table: "Manifesto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoManifesto",
                columns: table => new
                {
                    TipoManifestoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoManifesto", x => x.TipoManifestoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manifesto_TipoManifestoId",
                table: "Manifesto",
                column: "TipoManifestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manifesto_TipoManifesto_TipoManifestoId",
                table: "Manifesto",
                column: "TipoManifestoId",
                principalTable: "TipoManifesto",
                principalColumn: "TipoManifestoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manifesto_TipoManifesto_TipoManifestoId",
                table: "Manifesto");

            migrationBuilder.DropTable(
                name: "TipoManifesto");

            migrationBuilder.DropIndex(
                name: "IX_Manifesto_TipoManifestoId",
                table: "Manifesto");

            migrationBuilder.DropColumn(
                name: "TipoManifestoId",
                table: "Manifesto");

            migrationBuilder.AddColumn<int>(
                name: "TipoManifesto",
                table: "Manifesto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
