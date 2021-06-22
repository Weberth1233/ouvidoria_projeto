using Ouvidoria_Projeto.Controllers;
using Ouvidoria_Projeto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Models
{
    public class GestaoAcesso:BaseController
    {
        private readonly ApplicationDbContext _context;

        public GestaoAcesso(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public String AcessoPerfil(String email)
        {
            var usu = Perfil(_context, email);
            
            return usu;
        }
        public async Task<ApplicationUser> UsuarioLogado(String email)
        {
            var usu = await UsuarioLogado(_context, email);

            return usu;
        }
    }
}
