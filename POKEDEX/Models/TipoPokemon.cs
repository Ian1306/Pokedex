using System;
using System.Collections.Generic;

namespace POKEDEX.Models
{
    public partial class TipoPokemon
    {
        public TipoPokemon()
        {
            Pokemon = new HashSet<Pokemon>();
        }

        public int IdTipoPokemon { get; set; }
        public string NombreTipoPokemon { get; set; }

        public virtual ICollection<Pokemon> Pokemon { get; set; }
    }
}
