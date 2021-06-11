using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Data;
using Ouvidoria_Projeto.Models;

namespace Ouvidoria_Projeto.Controllers
{
    public class TipoManifestoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoManifestoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoManifestoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoManifestos.ToListAsync());
        }

        // GET: TipoManifestoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoManifesto = await _context.TipoManifestos
                .FirstOrDefaultAsync(m => m.TipoManifestoId == id);
            if (tipoManifesto == null)
            {
                return NotFound();
            }

            return View(tipoManifesto);
        }

        // GET: TipoManifestoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoManifestoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoManifestoId,Descricao")] TipoManifesto tipoManifesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoManifesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoManifesto);
        }

        // GET: TipoManifestoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoManifesto = await _context.TipoManifestos.FindAsync(id);
            if (tipoManifesto == null)
            {
                return NotFound();
            }
            return View(tipoManifesto);
        }

        // POST: TipoManifestoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoManifestoId,Descricao")] TipoManifesto tipoManifesto)
        {
            if (id != tipoManifesto.TipoManifestoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoManifesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoManifestoExists(tipoManifesto.TipoManifestoId))
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
            return View(tipoManifesto);
        }

        // GET: TipoManifestoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoManifesto = await _context.TipoManifestos
                .FirstOrDefaultAsync(m => m.TipoManifestoId == id);
            if (tipoManifesto == null)
            {
                return NotFound();
            }

            return View(tipoManifesto);
        }

        // POST: TipoManifestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoManifesto = await _context.TipoManifestos.FindAsync(id);
            _context.TipoManifestos.Remove(tipoManifesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoManifestoExists(int id)
        {
            return _context.TipoManifestos.Any(e => e.TipoManifestoId == id);
        }
    }
}
