using Microsoft.EntityFrameworkCore;
using PokeApiNet;
using Pokemon_Api.Data;
using Pokemon_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Services
{
    public class PokeServices
    {
        private readonly PokemonApiContext _context;

        public PokeServices(PokemonApiContext context)
        {
            _context = context;
        }

        public List<Abilities> GetPokemonsAbility(List<PokemonAbility> poke)
        {
            List<Abilities> namesAbilities = new List<Abilities>();
            foreach (PokemonAbility item in poke)
            {
                Abilities abilities = new Abilities();
                string url = item.Ability.Url.ToString();
                url = url.Substring(0, url.Length - 1);
                url = url.Split("/").Last();
                abilities.Id = int.Parse(url);
                abilities.Name = item.Ability.Name;
                namesAbilities.Add(abilities);
            }
            return namesAbilities;
        }
       public List<TypesPokemon> GetPokemonsTypes(List<PokemonType> poke)
        {
            List<TypesPokemon> namesTypes = new List<TypesPokemon>();
            foreach (PokemonType item in poke)
            {
                TypesPokemon Types = new TypesPokemon();
                string url = item.Type.Url.ToString();
                url = url.Substring(0, url.Length - 1);
                url = url.Split("/").Last();
                Types.Id = int.Parse(url);
                Types.Type = item.Type.Name;
                namesTypes.Add(Types);
            }
            return namesTypes;
        }

        //GetAll
        public async Task<List<Poke>> GetAllClientPokemonsCreated()
        {
            return await _context.Poke.ToListAsync();
        }

        //post
        public async Task InsertClientPokemonsCreated(Poke obj)
        {
           _context.Add(obj);
          await  _context.SaveChangesAsync();
        }
    }
}
