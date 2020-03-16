using System;
using System.Collections.Generic;

namespace POKEDEX.Models
{
    public partial class Region
    {
        public Region()
        {
            Pokemon = new HashSet<Pokemon>();
        }

        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Pokemon> Pokemon { get; set; }
    }
}
