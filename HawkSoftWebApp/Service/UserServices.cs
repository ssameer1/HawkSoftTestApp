using HawkSoftWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HawkSoftWebApp.Service
{
    public class UserServices : IUserServices
    {
        readonly HttpClient client;
        //this should be in appsetting.json file but for now i am doing this in code. 
        readonly string apiUrl = "https://localhost:44370/api";

        public UserServices()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
            //if it was secure api request we could add the authorization key in this too.
        }

        /// <summary>
        /// For this i am only doing UserList since concept is the same 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(int userId)
        {
            var dataString = await client.GetStringAsync(client.BaseAddress + "/Users/"+userId+"");
            var response = JsonConvert.DeserializeObject<User>(dataString);
            return response;
        }

        public async Task<bool> InsertUser(User user)
        {
            var serilizeObject = JsonConvert.SerializeObject(user);
            bool success = false;

            var response = await client.PostAsync(client.BaseAddress + "/Users", new StringContent(serilizeObject, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                success = true; 
            return success;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var serilizeObject = JsonConvert.SerializeObject(user);
            bool success = false;

            var response = await client.PutAsync(client.BaseAddress + "/Users/"+user.UserId+"", new StringContent(serilizeObject, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                success = true;
            return success;
        }

        /// <summary>
        /// For this we can check if the response is 200 or not 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> UserList()
        {
            var dataString = await client.GetStringAsync(client.BaseAddress + "/Users");
            var response = JsonConvert.DeserializeObject<List<User>>(dataString);
            return response;

        }
    }
}
