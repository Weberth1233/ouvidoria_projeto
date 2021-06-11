using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class manifestos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manifesto",
                columns: table => new
                {
                    ManifestoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    TipoManifesto = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    NumeroGerado = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifesto", x => x.ManifestoId);
                    table.ForeignKey(
                        name: "FK_Manifesto_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    RespostaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespostaManifesto = table.Column<string>(maxLength: 1000, nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    ManifestoId = table.Column<int>(nullable: false),
                    UsuarioId1 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.RespostaId);
                    table.ForeignKey(
                        name: "FK_Resposta_Manifesto_ManifestoId",
                        column: x => x.ManifestoId,
                        principalTable: "Manifesto",
                        principalColumn: "ManifestoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resposta_AspNetUsers_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manifesto_IdentityUserId",
                table: "Manifesto",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_ManifestoId",
                table: "Resposta",
                column: "ManifestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_UsuarioId1",
                table: "Resposta",
                column: "UsuarioId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "Manifesto");
        }
    }
}
