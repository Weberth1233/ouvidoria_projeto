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
    public class ManifestoAnominoesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ManifestoAnominoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManifestoAnominoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ManifestoAnomino.Include(m => m.TipoManifesto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ManifestoAnominoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoAnomino = await _context.ManifestoAnomino
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoAnomino == null)
            {
                return NotFound();
            }

            return View(manifestoAnomino);
        }

        // GET: ManifestoAnominoes/Create
        public IActionResult Create()
        {
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao");
            return View();
        }

        // POST: ManifestoAnominoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroGerado,ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] ManifestoAnomino manifestoAnomino)
        {
            Random randNum = new Random();

            if (ModelState.IsValid)
            {
                manifestoAnomino.Status = 0;
                manifestoAnomino.Data = DateTime.Now;
                manifestoAnomino.NumeroGerado = randNum.Next(1000, 10001);
                _context.Add(manifestoAnomino);
                await _context.SaveChangesAsync();
                ViewBag.Status = "O codigo gerado foi:"+manifestoAnomino.NumeroGerado;
                return RedirectToAction(nameof(Index));
            }

            

            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifestoAnomino.TipoManifestoId);
            return View(manifestoAnomino);
        }
        // GET: ManifestoAnominoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoAnomino = await _context.ManifestoAnomino.FindAsync(id);
            if (manifestoAnomino == null)
            {
                return NotFound();
            }
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifestoAnomino.TipoManifestoId);
            return View(manifestoAnomino);
        }

        // POST: ManifestoAnominoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroGerado,ManifestoId,Titulo,Descricao,Status,Data,TipoManifestoId")] ManifestoAnomino manifestoAnomino)
        {
            if (id != manifestoAnomino.ManifestoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manifestoAnomino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManifestoAnominoExists(manifestoAnomino.ManifestoId))
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
            ViewData["TipoManifestoId"] = new SelectList(_context.TipoManifestos, "TipoManifestoId", "Descricao", manifestoAnomino.TipoManifestoId);
            return View(manifestoAnomino);
        }

        // GET: ManifestoAnominoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manifestoAnomino = await _context.ManifestoAnomino
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoAnomino == null)
            {
                return NotFound();
            }

            return View(manifestoAnomino);
        }

        // POST: ManifestoAnominoes/Delete/5
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

        private bool ManifestoAnominoExists(int id)
        {
            return _context.ManifestoAnomino.Any(e => e.ManifestoId == id);
        }
    }
}
