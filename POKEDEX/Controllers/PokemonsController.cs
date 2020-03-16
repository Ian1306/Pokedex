using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POKEDEX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace POKEDEX.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly POKEDEXContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper _mapper;


        public PokemonsController(POKEDEXContext context,IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this._mapper = mapper;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
           

            var pokemons = await _context.Pokemon.ToListAsync();
            return View(pokemons); 

            //lock que me da risa es uqe esto es auto generado, pero manana lo pongo en una lista el modelo mejor y ya
            //    que opinas

        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.IdRegionNavigation)
                .Include(p => p.IdTipoAtaqueNavigation)
                .Include(p => p.IdTipoPokemonNavigation)
                .FirstOrDefaultAsync(m => m.IdPokemon == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public async Task<IActionResult > Create()
        {
            using (POKEDEXContext Context = new POKEDEXContext())
            {
                // Buscador del tipo de persona
                var ListTipoAtaque = Context.TipoAtaque.Select(Tpa => new
                {
                    Id = Tpa.IdTipoAtaque,
                    Nombre = Tpa.NombreTipoAtaque
                });
                ViewBag.TipoAtaque = new SelectList(ListTipoAtaque.ToList(), "Id", "Nombre");

                var ListTipoPokemon = Context.TipoPokemon.Select(Tpp => new 
                {
                    Id = Tpp.IdTipoPokemon,
                    Nombre = Tpp.NombreTipoPokemon
                });
                ViewBag.TipoPokemon = new SelectList(ListTipoPokemon.ToList(), "Id","Nombre");

                var ListRegion = Context.Region.Select(Re => new
                {
                    Id = Re.IdRegion,
                    Nombre = Re.NombreRegion
                });
                ViewBag.Region = new SelectList(ListRegion.ToList(), "Id", "Nombre");

            }


            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewPokemon VPokemon)
        {
            var MPokemon = new Pokemon();
            if (ModelState.IsValid)
            {
                string uniqueName = null;
                var filePath = string.Empty;
                if (VPokemon.PhotoPokemon != null)
                {
                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + VPokemon.PhotoPokemon;
                    filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null) VPokemon.PhotoPokemon.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                }
                VPokemon.IdPokemon = MPokemon.IdPokemon;
                VPokemon.IdRegion = MPokemon.IdRegion;
                VPokemon.IdTipoAtaque = MPokemon.IdTipoAtaque;
                VPokemon.NombrePokemon = MPokemon.NombrePokemon;
                VPokemon.PhotoPokemon = MPokemon.PhotoPokemon;
                VPokemon.IdTipoPokemon = MPokemon.IdTipoPokemon;

                MPokemon.PhotoPokemon = uniqueName;

                _context.Add(MPokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                

            }

            return View(VPokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            using (POKEDEXContext Context = new POKEDEXContext())
            {
                // Buscador del tipo de persona
                var ListTipoAtaque = Context.TipoAtaque.Select(Tpa => new
                {
                    Id = Tpa.IdTipoAtaque,
                    Nombre = Tpa.NombreTipoAtaque
                });
                ViewBag.TipoAtaque = new SelectList(ListTipoAtaque.ToList(), "Id", "Nombre");

                var ListTipoPokemon = Context.TipoPokemon.Select(Tpp => new 
                {
                    Id = Tpp.IdTipoPokemon,
                    Nombre = Tpp.NombreTipoPokemon
                });
                ViewBag.TipoPokemon = new SelectList(ListTipoPokemon.ToList(), "Id","Nombre");

                var ListRegion = Context.Region.Select(Re => new
                {
                    Id = Re.IdRegion,
                    Nombre = Re.NombreRegion
                });
                ViewBag.Region = new SelectList(ListRegion.ToList(), "Id", "Nombre");

            }
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPokemon,NombrePokemon,IdTipoPokemon,IdRegion,IdTipoAtaque")] Pokemon pokemon)
        {
            if (id != pokemon.IdPokemon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.IdPokemon))
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
            ViewData["IdRegion"] = new SelectList(_context.Region, "IdRegion", "NombreRegion", pokemon.IdRegion);
            ViewData["IdTipoAtaque"] = new SelectList(_context.TipoAtaque, "IdTipoAtaque", "IdTipoAtaque", pokemon.IdTipoAtaque);
            ViewData["IdTipoPokemon"] = new SelectList(_context.TipoPokemon, "IdTipoPokemon", "IdTipoPokemon", pokemon.IdTipoPokemon);
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.IdRegionNavigation)
                .Include(p => p.IdTipoAtaqueNavigation)
                .Include(p => p.IdTipoPokemonNavigation)
                .FirstOrDefaultAsync(m => m.IdPokemon == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.IdPokemon == id);
        }
    }
}
