using System;
using System.Collections.Generic;

namespace POKEDEX.Models
{
    public partial class Pokemon
    {
        public int IdPokemon { get; set; }
        public string NombrePokemon { get; set; }
        public string PhotoPokemon { get; set; }
        public int? IdTipoPokemon { get; set; }
        public int? IdRegion { get; set; }
        public int? IdTipoAtaque { get; set; }

        public virtual Region IdRegionNavigation { get; set; }
        public virtual TipoAtaque IdTipoAtaqueNavigation { get; set; }
        public virtual TipoPokemon IdTipoPokemonNavigation { get; set; }
    }
}
