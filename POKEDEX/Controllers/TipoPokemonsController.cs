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
    public class TipoPokemonsController : Controller
    {
        private readonly POKEDEXContext _context;

        public TipoPokemonsController(POKEDEXContext context)
        {
            _context = context;
        }

        // GET: TipoPokemons
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoPokemon.ToListAsync());
        }

        // GET: TipoPokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPokemon = await _context.TipoPokemon
                .FirstOrDefaultAsync(m => m.IdTipoPokemon == id);
            if (tipoPokemon == null)
            {
                return NotFound();
            }

            return View(tipoPokemon);
        }

        // GET: TipoPokemons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPokemons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoPokemon,NombreTipoPokemon")] TipoPokemon tipoPokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPokemon);
        }

        // GET: TipoPokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPokemon = await _context.TipoPokemon.FindAsync(id);
            if (tipoPokemon == null)
            {
                return NotFound();
            }
            return View(tipoPokemon);
        }

        // POST: TipoPokemons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoPokemon,NombreTipoPokemon")] TipoPokemon tipoPokemon)
        {
            if (id != tipoPokemon.IdTipoPokemon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPokemonExists(tipoPokemon.IdTipoPokemon))
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
            return View(tipoPokemon);
        }

        // GET: TipoPokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPokemon = await _context.TipoPokemon
                .FirstOrDefaultAsync(m => m.IdTipoPokemon == id);
            if (tipoPokemon == null)
            {
                return NotFound();
            }

            return View(tipoPokemon);
        }

        // POST: TipoPokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoPokemon = await _context.TipoPokemon.FindAsync(id);
            _context.TipoPokemon.Remove(tipoPokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPokemonExists(int id)
        {
            return _context.TipoPokemon.Any(e => e.IdTipoPokemon == id);
        }
    }
}
