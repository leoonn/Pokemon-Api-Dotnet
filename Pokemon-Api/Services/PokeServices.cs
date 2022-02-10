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

        #region PokeApi
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
        #endregion

        #region Pokemons do usuario
        //GetAll
        public async Task<List<Poke>> GetAllClientPokemonsCreated()
        {
            return await _context.Poke.Include(obj => obj.Abilities).Include(obj => obj.Type).ToListAsync();
        }

        //GetById
        public async Task<Poke> GetByIdClientPokemonsCreated(int id)
        {
            return await _context.Poke.Include(obj => obj.Abilities).Include(obj => obj.Type).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        //GetBystring
        public async Task<Poke> GetByNameClientPokemonsCreated(string name)
        {
            return await _context.Poke.Include(obj => obj.Abilities).Include(obj => obj.Type).FirstOrDefaultAsync(obj => obj.Name == name);
        }
        //Update
        public async Task UpdateClientPokemonsCreated(Poke poke)
        {
            var hasany = await _context.Poke.AnyAsync(obj => obj.Id == poke.Id);
            if (!hasany)
            {
                throw new Exception();
            }
            _context.Update(poke);
            await _context.SaveChangesAsync();
        }
        //Delete pokemon
        public async Task RemoveClientPokemonsCreated(long id)
        {
            var poke = await _context.Poke.FindAsync(id);

            
            _context.Poke.Remove(poke);
           await _context.SaveChangesAsync();
        }
        //Delete PokemonAbillities
        public async Task RemoveAbillitiesClientPokemonsCreated(int id)
        {
            var poke = await _context.Abilities.FindAsync(id);


            _context.Abilities.Remove(poke);
            await _context.SaveChangesAsync();
        }

        //Delete PokemonType
        public async Task RemoveTypeClientPokemonsCreated(int id)
        {
            var poke = await _context.TypesPokemon.FindAsync(id);


            _context.TypesPokemon.Remove(poke);
            await _context.SaveChangesAsync();
        }

        //post
        public async Task InsertClientPokemonsCreated(Poke obj)
        {
           _context.Add(obj);
          await  _context.SaveChangesAsync();
        }
        #endregion
    }
}
