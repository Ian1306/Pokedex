using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POKEDEX.Models
{
    public class ViewPokemon
    {
        public int IdPokemon { get; set; }
        public string NombrePokemon { get; set; }
        public IFormFile PhotoPokemon { get; set; }
        public int? IdTipoPokemon { get; set; }
        public int? IdRegion { get; set; }
        public int? IdTipoAtaque { get; set; }

    }
}
