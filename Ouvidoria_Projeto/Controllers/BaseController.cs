using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Data;
using Ouvidoria_Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ouvidoria_Projeto.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public async Task<bool> Usuario_Tem_Acesso(int codigoPagina, ApplicationDbContext _context)
        {

            var usuario = User.Identity.Name;


            var temAcesso = await (from TP in _context.TipoUsuario
                                   join AT in _context.AcessoTipoUsuario on TP.Id equals AT.IdTipoUsuario
                                   join PF in _context.PerfilUsuario on TP.Id equals PF.IdTipoUsuario
                                   join US in _context.Usuario on PF.UsuarioId equals US.Id
                                   where AT.Id == codigoPagina && US.Email == usuario
                                   select new
                                   {
                                       TP.Id
                                   }).AnyAsync();


            return temAcesso;

        }
        public async Task<ApplicationUser> UsuarioLog(ApplicationDbContext _context)
        {

            var usuario = User.Identity.Name;

            var userId = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Email == usuario);

            return userId;

        }
        public async Task<Boolean> AtualizarStatusUsu(String id, ApplicationDbContext _context, int novoStatus)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return false;
            }
            //usuario.st = novoStatus;
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }

        public String Perfil(ApplicationDbContext _context, String email)
        {
            String resultado = "";
            var queryUser = from PE in _context.PerfilUsuario
                                       join US in _context.Usuario on PE.UsuarioId equals US.Id 
                                       where US.Email == email
                                       select new { IdTipo = PE.IdTipoUsuario};

            foreach (var item in queryUser)
            {
                Console.WriteLine($"{item.IdTipo}");
                resultado = item.IdTipo.ToString();
            }
            return resultado;
        }
        public async Task<Boolean> AtualizarStatus(int id, ApplicationDbContext _context, int novoStatus)
        {
            var manifesto = await _context.ManifestosSolicitantes
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifesto == null)
            {
                return false;
            }
            manifesto.Status = novoStatus;
            try
            {
                _context.Update(manifesto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }
    }
}
