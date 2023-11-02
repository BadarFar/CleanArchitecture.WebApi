using Application.DTOs;
using Application.Enums;
using Application.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class RestClient : IRestClient
    {
        public async Task<ApiResult<TData>> ExecuteGetAsync<TData>(string requestUrl) where TData : class
        {
            TData data = null;
            var requestMessage = CreateMessage("GET", requestUrl);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<TData>(await response?.Content.ReadAsStringAsync()!);
            }

            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        /// <summary>
        /// Create a get message that does not need to send any content to server
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected HttpRequestMessage CreateMessage(string method, string url)
        {
            HttpRequestMessage message = new HttpRequestMessage { Method = new HttpMethod(method), RequestUri = new Uri(url) };
            return message;
        }
    }
}
