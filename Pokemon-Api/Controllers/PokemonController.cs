using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeApiNet;
using Pokemon_Api.Models;
using Pokemon_Api.Services;

namespace Pokemon_Api.Controllers
{
    public class PokemonController : Controller
    {
       private PokeApiClient pokeClient = new PokeApiClient();
       private readonly PokeServices pokeservices;

        public PokemonController( PokeServices pokeservices)
        {
            this.pokeservices = pokeservices;
        }

        #region Consumindo a PokeApi
        [HttpGet] //Get Poke For ID https://localhost:44393/pokemon/id/324
        [Route("Pokemon/Id/{id?}")]
        public async Task<Poke> IdAsync(int Id )
        {
            Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(Id);
            string url = "https://pokeapi.co/api/v2/pokemon/" + Id;
            Poke pokemon = new Poke(poke.Id, poke.Name, poke.Height, poke.Weight, pokeservices.GetPokemonsAbility(poke.Abilities), pokeservices.GetPokemonsTypes(poke.Types), url);            
            return pokemon;
        }
        [HttpGet]  //Get Poke For Name https://localhost:44393/pokemon/name/lapras
        [Route("Pokemon/Name/{name?}")]
        public async Task<Poke> NameAsync(string name )
        {
            Pokemon poke = await pokeClient.GetResourceAsync<Pokemon>(name);
            string url = "https://pokeapi.co/api/v2/pokemon/"+name;
            Poke pokemon = new Poke(poke.Id, poke.Name, poke.Height, poke.Weight, pokeservices.GetPokemonsAbility(poke.Abilities), pokeservices.GetPokemonsTypes(poke.Types), url);
            return pokemon;
        }

        [HttpGet]  //Get Poke with limit and offset https://localhost:44393/pokemon/GetAll/30/30
        [Route("Pokemon/GetAll/{limit}/{offset}")]
        public  PokemonsLists GetAll(int limit, int offset)
        {
            //******Forma para fazer uma requisição sem a biblioteca do pokemon*******
            string url = "https://pokeapi.co/api/v2/pokemon?limit=" + limit +  "&offset=" + offset;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream);
            string responsePokemons = readStream.ReadToEnd();
            PokemonsLists pokemons = JsonConvert.DeserializeObject<PokemonsLists>(responsePokemons);
            return pokemons;
        }
        #endregion

        #region Crud Pokemons do usuario
        [HttpPost] //Desenvolvendo uma criação de pokemons personalizados
        [Route("Pokemon/Create")]
        public async Task<Poke> CreateAsync(Poke pokemon)
        {
          await pokeservices.InsertClientPokemonsCreated(pokemon);
            return pokemon;
        }

        [HttpGet] 
        [Route("Pokemon/GetMyPokes")]
        public async Task<List<Poke>> GetMyPokesAsync()
        {
            return await pokeservices.GetAllClientPokemonsCreated();
        }
        [HttpGet]
        [Route("Pokemon/GetMyPokes/{id}")]
        public async Task<Poke> GetMyPokesIdAsync(int id)
        {
            return await pokeservices.GetByIdClientPokemonsCreated(id);
        }
        [HttpGet]
        [Route("Pokemon/GetMyPokes/poke:{name}")]
        public async Task<Poke> GetMyPokesNameAsync(string name)
        {
            return await pokeservices.GetByNameClientPokemonsCreated(name);
        }
        [HttpPut]
        [Route("Pokemon/MyPokeEdit")]
        public async Task<Poke> MyPokeEditAsync(Poke poke)
        {
             await pokeservices.UpdateClientPokemonsCreated(poke);
            return poke;
        }
        [HttpPost]
        [Route("Pokemon/MyPokeDelete/{id}")]
        public async Task MyPokeDeleteAsync(long id)
        {
            await pokeservices.RemoveClientPokemonsCreated(id);
        }
        [HttpPost]
        [Route("Pokemon/MyPokeDeleteAbillitie/{id}")]
        public async Task MyPokeDeleteAbillitieAsync(int id)
        {
            await pokeservices.RemoveAbillitiesClientPokemonsCreated(id);
        }
        [HttpPost]
        [Route("Pokemon/MyPokeDeleteType/{id}")]
        public async Task MyPokeDeleteTypeAsync(int id)
        {
            await pokeservices.RemoveTypeClientPokemonsCreated(id);
        }
        #endregion
    }
}