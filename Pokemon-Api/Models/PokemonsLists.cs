using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Models
{
    public class PokemonsLists
    {
        [JsonProperty("results")]
        public List<Species> Pokemons { get; set; }

    }
}
