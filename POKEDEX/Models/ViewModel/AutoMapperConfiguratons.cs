using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace POKEDEX.Models.ViewModel
{
    public class AutoMapperConfiguratons : Profile
    {
        public AutoMapperConfiguratons()
        {


            ConfigurePokedex();

        }


        private void ConfigurePokedex()
        {
            CreateMap<ViewPokemon, Pokemon>().ForMember(x=>x.PhotoPokemon, x=>x.MapFrom(y=>y.PhotoPokemon.FileName));
            CreateMap<Pokemon, ViewPokemon>().ForMember(dest => dest.PhotoPokemon, opt => opt.Ignore()); ;
        }
    }
}
