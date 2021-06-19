using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Data;
using Ouvidoria_Projeto.Models;

namespace Ouvidoria_Projeto.Controllers
{
    public class SolicitantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SolicitantesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Solicitantes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Solicitantes.Include(s => s.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Solicitantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitantes
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.SolicitanteId == id);
            if (solicitante == null)
            {
                return NotFound();
            }

            return View(solicitante);
        }

        // GET: Solicitantes/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }
        

       // POST: Solicitantes/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to, for 
       // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitanteId,Nome,Email,Senha,Status,Sexo,UsuarioId")] Solicitante solicitante)
        {
            ApplicationUser usuario = new ApplicationUser();
            if (ModelState.IsValid)
            {
                usuario.Nome = solicitante.Nome;
                usuario.UserName = solicitante.Email;
                usuario.Email = solicitante.Email;
                usuario.PasswordHash = solicitante.Senha;
                usuario.Numero = "839289392989";
                usuario.ESolicitante = 1;

                var result = await _userManager.CreateAsync(usuario, usuario.PasswordHash);
                if (result.Succeeded)
                {
                    solicitante.Status = 1;
                    solicitante.UsuarioId = usuario.Id;
                    _context.Solicitantes.Add(solicitante);
                    _context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Ocorreu algum erro!");
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", solicitante.UsuarioId);
            return View(solicitante);
        }

        // GET: Solicitantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitantes.FindAsync(id);
            if (solicitante == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", solicitante.UsuarioId);
            return View(solicitante);
        }

        // POST: Solicitantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitanteId,Nome,Email,Senha,Status,Sexo,UsuarioId")] Solicitante solicitante)
        {
            if (id != solicitante.SolicitanteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitanteExists(solicitante.SolicitanteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", solicitante.UsuarioId);
            return View(solicitante);
        }

        // GET: Solicitantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitante = await _context.Solicitantes
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.SolicitanteId == id);
            if (solicitante == null)
            {
                return NotFound();
            }

            return View(solicitante);
        }

        // POST: Solicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitante = await _context.Solicitantes.FindAsync(id);
            _context.Solicitantes.Remove(solicitante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitanteExists(int id)
        {
            return _context.Solicitantes.Any(e => e.SolicitanteId == id);
        }
    }
}
