using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Data
{
    public class PokemonApiContext : DbContext
    {
        public PokemonApiContext (DbContextOptions<PokemonApiContext> options) : base(options)
        {

        }

        public DbSet<Pokemon_Api.Models.Poke> Poke { get; set; }
        public DbSet<Pokemon_Api.Models.Abilities> Abilities { get; set; }
        public DbSet<Pokemon_Api.Models.TypesPokemon> TypesPokemon { get; set; }
    }
}
