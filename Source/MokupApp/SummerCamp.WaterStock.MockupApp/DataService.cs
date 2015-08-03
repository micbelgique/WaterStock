using SummerCamp.WaterStock.MockupApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SummerCamp.WaterStock.MockupApp
{
    public class DataService
    {
        private const string SERVICE_URL = "http://summercampwaterstock.azurewebsites.net/";

        System.Net.WebClient _client = new System.Net.WebClient();

        private static volatile DataService _instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Returns all user informations for this UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<User> GetUserInfo(int userID)
        {
            return await this.GetData<User>($"api/users/{userID}");
        }

        /// <summary>
        /// Returns all store point informations for this ID
        /// </summary>
        /// <param name="storePointID"></param>
        /// <returns></returns>
        public async Task<StorePoint> GetStorePoint(int storePointID)
        {
            StorePoint[] storepoints = await this.GetData<StorePoint[]>($"api/StorePoints");
            return storepoints.FirstOrDefault(s => s.StorePointID == storePointID);
        }

        /// <summary>
        /// Returns all store point informations
        /// </summary>
        /// <returns></returns>
        public async Task<StorePoint[]> GetStorePoints()
        {
            return await this.GetData<StorePoint[]>($"api/StorePoints");
        }

        /// <summary>
        /// Returns all users informations
        /// </summary>
        /// <returns></returns>
        public async Task<User[]> GetAllUsers()
        {
            return await this.GetData<User[]>($"api/users");
        }

        public async Task<StorePoint> AddConsumption(int userID, string storePointReference, int quantityConsumed)
        {
            return await GetData<StorePoint>($"api/consumptions/{userID}/add/{storePointReference}/{quantityConsumed}");
        }

        /// <summary>
        /// Returns the data object associated to specified url (api/users/1)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T> GetData<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SERVICE_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonText = await response.Content.ReadAsStringAsync();
                    var jss = new JavaScriptSerializer();
                    return jss.Deserialize<T>(jsonText);
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Get a unique reference of DataService
        /// </summary>
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new DataService();
                    }
                }

                return _instance;
            }
        }

    }
}
