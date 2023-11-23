using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WPF_AdvertClient
{
    public class Advert
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("seller")]
        public string Seller { get; set; }
        
        [JsonProperty("termsandconditions")]
        public bool TermsAndConditions { get; set; }
    }
}
