using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class tabelaResposta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId1",
                table: "Resposta");

            migrationBuilder.DropIndex(
                name: "IX_Resposta_UsuarioId1",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Resposta");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Resposta",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_UsuarioId",
                table: "Resposta",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta");

            migrationBuilder.DropIndex(
                name: "IX_Resposta_UsuarioId",
                table: "Resposta");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Resposta",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Resposta",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_UsuarioId1",
                table: "Resposta",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId1",
                table: "Resposta",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
