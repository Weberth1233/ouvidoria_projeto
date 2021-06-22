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
    public class RespostasController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public RespostasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Respostas
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso(4, _context);

            if (!temAcesso)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await UsuarioLog(_context);
            var applicationDbContext = _context.Resposta.
                Include(r => r.Manifesto).
                Include(r => r.Usuario)
               .Where(r => r.UsuarioId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Respostas/Details/5
        public async Task<IActionResult> Details()
        {
            var idMani = TempData["ManiId"];
            var resposta = await _context.Resposta
                .Include(r => r.Manifesto)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ManifestoId == (int)idMani);
            if (resposta == null)
            {
                return NotFound();
            }
            return View(resposta);
        }

        public async Task<IActionResult> DetailsResposta(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resposta = await _context.Resposta
                .Include(r => r.Manifesto).
                 Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.RespostaId == id);
            if (resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }


        public async Task<IActionResult> AtualizarStatus(int id)
        {
            var manifestoSolicitante = await _context.ManifestosSolicitantes
                .Include(m => m.TipoManifesto)
                .FirstOrDefaultAsync(m => m.ManifestoId == id);
            if (manifestoSolicitante == null)
            {
                return NotFound();
            }
            manifestoSolicitante.Status = 1;
            try{
                _context.Update(manifestoSolicitante);
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return View(manifestoSolicitante);
        }

        // GET: Respostas/Create
        public IActionResult Create()
        {
            ViewData["ManifestoId"] = new SelectList(_context.Manifesto, "ManifestoId", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        // POST: Respostas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RespostaId,RespostaManifesto,Data,UsuarioId,ManifestoId")] Resposta resposta)
        {
            var user = await UsuarioLog(_context);
            var id = TempData["idManifesto"];
            if (ModelState.IsValid)
            {
                resposta.Data = DateTime.Now;
                resposta.ManifestoId = (int)id;
                resposta.UsuarioId = user.Id;
                _context.Add(resposta);
                await _context.SaveChangesAsync();

                await AtualizarStatus((int)id);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ManifestoId"] = new SelectList(_context.Manifesto, "ManifestoId", "Descricao", resposta.ManifestoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", resposta.UsuarioId);
            
            

            return View(resposta);
        }

        // GET: Respostas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resposta = await _context.Resposta.FindAsync(id);
            if (resposta == null)
            {
                return NotFound();
            }
            ViewData["ManifestoId"] = new SelectList(_context.Manifesto, "ManifestoId", "Descricao", resposta.ManifestoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", resposta.UsuarioId);
            return View(resposta);
        }

        // POST: Respostas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RespostaId,RespostaManifesto,Data,UsuarioId,ManifestoId")] Resposta resposta)
        {
            if (id != resposta.RespostaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resposta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespostaExists(resposta.RespostaId))
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
            ViewData["ManifestoId"] = new SelectList(_context.Manifesto, "ManifestoId", "Descricao", resposta.ManifestoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", resposta.UsuarioId);
            return View(resposta);
        }

        // GET: Respostas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resposta = await _context.Resposta
                .Include(r => r.Manifesto)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.RespostaId == id);
            if (resposta == null)
            {
                return NotFound();
            }

            return View(resposta);
        }

        // POST: Respostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resposta = await _context.Resposta.FindAsync(id);
            _context.Resposta.Remove(resposta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespostaExists(int id)
        {
            return _context.Resposta.Any(e => e.RespostaId == id);
        }
    }
}
