﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Specflow.Services
{
    public class Requests
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<User[]> GetUsers(string url)
        {
            var resp = await GetResponse(url);
            User[] users = User.FromJson(resp);
            return users;
        }

        public static async Task<HttpStatusCode> GetStatusCode(string url)
        {
            return HttpClient.GetAsync(url).Result.StatusCode;
        }

        public static async Task<string> GetResponse(string url)
        {
            var resp = await HttpClient.GetStringAsync(url);
            return resp;
        }
    }
}