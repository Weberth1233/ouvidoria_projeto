using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Data;
using Ouvidoria_Projeto.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Ouvidoria_Projeto.Controllers
{
    [Authorize]
    public class ManifestoSolicitantesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ManifestoSolicitantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManifestoSolicitantes
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso(1, _context);

            if (!temAcesso)
            {
                return RedirectToAction("Error", "Shared");
            }

            var user = await UsuarioLog(_context);
            var applicationDbContext = _context.ManifestosSolicitantes.
                Include(m => m.TipoManifesto)
               .Include(m => m.IdentityUser)
               .Where(m => m.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ManifestoSolicitantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoSolicitante = await _context.ManifestosSolicitantes
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoSolicitante == null)
            {
                return NotFound();
            }

            return View(manifestoSolicitante);
        }
        // GET: ManifestoSolicitantes/Details/5
        public async Task<IActionResult> Visualizar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoSolicitante = await _context.ManifestosSolicitantes
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoSolicitante == null)
            {
                return NotFound();
            }
            TempData["ManiId"] = id;

            return RedirectToAction("Details", "Respostas");
        }

        // GET: ManifestoSolicitantes/Create
        public IActionResult Create()
        {
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao");
            return View();
        }

        // POST: ManifestoSolicitantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] ManifestoSolicitante manifestoSolicitante)
        {
            var temAcesso = await Usuario_Tem_Acesso(5, _context);
            if (!temAcesso)
            {
                return RedirectToAction("Error", "Shared");
            }
            var user = await UsuarioLog(_context);
            if (ModelState.IsValid)
            {
                manifestoSolicitante.Data = DateTime.Now;
                manifestoSolicitante.Status = 0;
                manifestoSolicitante.UserId = user.Id;
                _context.Add(manifestoSolicitante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifestoSolicitante.TipoManifestoId);
            return View(manifestoSolicitante);
        }

        // GET: ManifestoSolicitantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoSolicitante = await _context.ManifestosSolicitantes.FindAsync(id);
            if (manifestoSolicitante == null)
            {
                return NotFound();
            }
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifestoSolicitante.TipoManifestoId);
            return View(manifestoSolicitante);
        }

        // POST: ManifestoSolicitantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] ManifestoSolicitante manifestoSolicitante)
        {
            if (id != manifestoSolicitante.ManifestoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manifestoSolicitante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManifestoSolicitanteExists(manifestoSolicitante.ManifestoId))
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
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "TipoManifestoId", manifestoSolicitante.TipoManifestoId);
            return View(manifestoSolicitante);
        }

        // GET: ManifestoSolicitantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoSolicitante = await _context.ManifestosSolicitantes
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoSolicitante == null)
            {
                return NotFound();
            }

            return View(manifestoSolicitante);
        }

        // POST: ManifestoSolicitantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verificacao = AtualizarStatus(id, _context, 2);
            if (await verificacao)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool ManifestoSolicitanteExists(int id)
        {
            return _context.ManifestosSolicitantes.Any(e => e.ManifestoId == id);
        }
    }
}
