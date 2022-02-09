using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon_Api.Models
{
    public class Abilities
    {
        
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
