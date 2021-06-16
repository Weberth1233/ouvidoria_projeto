using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class removeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Resposta",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Resposta",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_AspNetUsers_UsuarioId",
                table: "Resposta",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
