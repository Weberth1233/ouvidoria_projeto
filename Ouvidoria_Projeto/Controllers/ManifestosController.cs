using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ouvidoria_Projeto.Data;
using Ouvidoria_Projeto.Models;

namespace Ouvidoria_Projeto.Controllers
{
    public class ManifestosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManifestosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manifestos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Manifesto.Include(m => m.TipoManifesto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Manifestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifesto = await _context.Manifesto
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifesto == null)
            {
                return NotFound();
            }

            return View(manifesto);
        }
        // GET: Manifestos/Details/5
        
        public async Task<IActionResult> Resposta(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifesto = await _context.Manifesto
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifesto == null)
            {
                return NotFound();
            }
            TempData["idManifesto"] = id;

            return RedirectToAction("Create", "Respostas");
        }                
        // GET: Manifestos/Create
        public IActionResult Create()
        {
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao");
            return View();
        }

        // POST: Manifestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] Manifesto manifesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manifesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifesto.TipoManifestoId);
            return View(manifesto);
        }

        // GET: Manifestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifesto = await _context.Manifesto.FindAsync(id);
            if (manifesto == null)
            {
                return NotFound();
            }
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifesto.TipoManifestoId);
            return View(manifesto);
        }

        // POST: Manifestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] Manifesto manifesto)
        {
            if (id != manifesto.ManifestoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manifesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManifestoExists(manifesto.ManifestoId))
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
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifesto.TipoManifestoId);
            return View(manifesto);
        }

        // GET: Manifestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifesto = await _context.Manifesto
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifesto == null)
            {
                return NotFound();
            }

            return View(manifesto);
        }

        // POST: Manifestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manifesto = await _context.Manifesto.FindAsync(id);
            _context.Manifesto.Remove(manifesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManifestoExists(int id)
        {
            return _context.Manifesto.Any(e => e.ManifestoId == id);
        }
    }
}
