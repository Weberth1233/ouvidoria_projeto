using Microsoft.EntityFrameworkCore.Migrations;

namespace Ouvidoria_Projeto.Data.Migrations
{
    public partial class alterandoPerfilUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilUsuario_AspNetUsers_IdentityUserId",
                table: "PerfilUsuario");

            migrationBuilder.DropIndex(
                name: "IX_PerfilUsuario_IdentityUserId",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PerfilUsuario");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "PerfilUsuario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuario_UsuarioId",
                table: "PerfilUsuario",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilUsuario_AspNetUsers_UsuarioId",
                table: "PerfilUsuario",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilUsuario_AspNetUsers_UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.DropIndex(
                name: "IX_PerfilUsuario_UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "PerfilUsuario");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "PerfilUsuario",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PerfilUsuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuario_IdentityUserId",
                table: "PerfilUsuario",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilUsuario_AspNetUsers_IdentityUserId",
                table: "PerfilUsuario",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
