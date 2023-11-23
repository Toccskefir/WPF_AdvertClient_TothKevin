using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WPF_AdvertClient
{
    internal class AdvertService
    {
        private HttpClient client = new HttpClient();
        private string url = "https://retoolapi.dev/D2hiYM/ad_creator";

        public List<Advert> GetAll()
        {
            string json = client.GetStringAsync(this.url).Result;
            return JsonConvert.DeserializeObject<List<Advert>>(json);
        }

        public Advert AddAdvert(Advert newAdvert)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(newAdvert), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(this.url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Advert>(responseContent);
        }

        public bool DeleteAdvert(Advert selected)
        {
            int id = selected.Id;
            HttpResponseMessage response = client.DeleteAsync($"{this.url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public Advert UpdateAdvert(int id, Advert advert)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(advert), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PatchAsync($"{this.url}/{id}", content).Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Advert>(responseContent);
        }
    }
}
