using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pokemon_Api;
using PokeApiNet;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Api.Models
{
    public class Poke
    {
        [JsonProperty("id")]
        [Key]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("height")]
        public long Height { get; set; }
        [JsonProperty("weight")]
        public long Weight { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("abilities")]
        public List<Abilities>  Abilities { get; set; }
        [JsonProperty("type")]
        public List<TypesPokemon> Type { get; set; }

        public Poke(long id, string name, long height, long weight, List<Abilities> abilities, List<TypesPokemon> type,string url)
        {
            Id = id;
            Name = name;
            Height = height;
            Weight = weight;
            Abilities = abilities;
            Type = type;
            Url = url;
        }
        public Poke( string name, string url)
        {
            Name = name;
            Url = url;
        }
        public Poke()
        {
            
        }
    }
}
