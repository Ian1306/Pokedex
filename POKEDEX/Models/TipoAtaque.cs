using System;
using System.Collections.Generic;

namespace POKEDEX.Models
{
    public partial class TipoAtaque
    {
        public TipoAtaque()
        {
            Pokemon = new HashSet<Pokemon>();
        }

        public int IdTipoAtaque { get; set; }
        public string NombreTipoAtaque { get; set; }

        public virtual ICollection<Pokemon> Pokemon { get; set; }
    }
}
