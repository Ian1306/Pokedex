using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POKEDEX.Models;

namespace POKEDEX.Controllers
{
    public class TipoAtaquesController : Controller
    {
        private readonly POKEDEXContext _context;

        public TipoAtaquesController(POKEDEXContext context)
        {
            _context = context;
        }

        // GET: TipoAtaques
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoAtaque.ToListAsync());
        }

        // GET: TipoAtaques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAtaque = await _context.TipoAtaque
                .FirstOrDefaultAsync(m => m.IdTipoAtaque == id);
            if (tipoAtaque == null)
            {
                return NotFound();
            }

            return View(tipoAtaque);
        }

        // GET: TipoAtaques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoAtaques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoAtaque,NombreTipoAtaque")] TipoAtaque tipoAtaque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoAtaque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAtaque);
        }

        // GET: TipoAtaques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAtaque = await _context.TipoAtaque.FindAsync(id);
            if (tipoAtaque == null)
            {
                return NotFound();
            }
            return View(tipoAtaque);
        }

        // POST: TipoAtaques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoAtaque,NombreTipoAtaque")] TipoAtaque tipoAtaque)
        {
            if (id != tipoAtaque.IdTipoAtaque)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAtaque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAtaqueExists(tipoAtaque.IdTipoAtaque))
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
            return View(tipoAtaque);
        }

        // GET: TipoAtaques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAtaque = await _context.TipoAtaque
                .FirstOrDefaultAsync(m => m.IdTipoAtaque == id);
            if (tipoAtaque == null)
            {
                return NotFound();
            }

            return View(tipoAtaque);
        }

        // POST: TipoAtaques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoAtaque = await _context.TipoAtaque.FindAsync(id);
            _context.TipoAtaque.Remove(tipoAtaque);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAtaqueExists(int id)
        {
            return _context.TipoAtaque.Any(e => e.IdTipoAtaque == id);
        }
    }
}
