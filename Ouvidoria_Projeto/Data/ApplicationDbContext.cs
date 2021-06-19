using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Models;

namespace Ouvidoria_Projeto.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Ouvidoria_Projeto.Models.TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Ouvidoria_Projeto.Models.AcessoTipoUsuario> AcessoTipoUsuario { get; set; }
        public DbSet<Ouvidoria_Projeto.Models.PerfilUsuario> PerfilUsuario { get; set; }

        public DbSet<Manifesto> Manifesto { get; set; }

        public DbSet<ManifestoSolicitante> ManifestosSolicitantes { get; set; }

        public DbSet<ManifestoAnomino> ManifestoAnomino { get; set; }

        public DbSet<ApplicationUser> Usuario { get; set; }

        public DbSet<Resposta> Resposta { get; set; }

        public DbSet<TipoManifesto> TipoManifestos { get; set; }

        public DbSet<Solicitante> Solicitantes { get; set; }
    }
}
