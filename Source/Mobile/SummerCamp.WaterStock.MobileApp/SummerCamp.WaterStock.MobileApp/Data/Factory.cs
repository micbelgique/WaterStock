using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.Data
{
    public class Factory
    {

        public async Task<User[]> GetUsers()
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync("http://summercampwaterstock.azurewebsites.net/api/users");
            string receivedJson;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                receivedJson = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<User[]>(receivedJson);
        }

        public async Task<User> GetUser(int id)
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync($"http://summercampwaterstock.azurewebsites.net/api/users/{id}");
            string receivedJson;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                receivedJson = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<User>(receivedJson);
        }
        

    public async Task<Consumption[]> GetConsumption(int userID, int days)
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync($"http://summercampwaterstock.azurewebsites.net/api/consumptions/{userID}/{days}");
            string receivedJson;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                receivedJson = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Consumption[]>(receivedJson);
        }

        public async Task<Group[]> GetGroups()
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync("http://summercampwaterstock.azurewebsites.net/api/Groups");
            string receivedJson;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                receivedJson = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Group[]>(receivedJson);
        }

        public async Task<StorePoint[]> GetStorePoints()
        {
            HttpClient client = new HttpClient();
            var stream = await client.GetStreamAsync("http://summercampwaterstock.azurewebsites.net/api/StorePoints");
            string receivedJson;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                receivedJson = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<StorePoint[]>(receivedJson);
        }

            Random _rand = new Random(5);
        public int GetRandom()
        {
            return _rand.Next(30);
        }
    }
}
