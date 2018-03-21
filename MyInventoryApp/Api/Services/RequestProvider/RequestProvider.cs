using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MyInventoryApp.Api.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        const string ApplicationJsonContentType = "application/json";

        readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task DeleteAsync(string uri, string token = "")
        {
            var httpClient = CreateHttpClient(token);
            await httpClient.DeleteAsync(uri);
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            var httpClient = CreateHttpClient(token);
            var response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string responseContent = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseContent, _serializerSettings));

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            var httpClient = CreateHttpClient(token);

            AddHeaderParameter(httpClient, header);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJsonContentType);
            var response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string responseCotent = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(responseCotent, _serializerSettings));

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            var httpClient = CreateHttpClient(token);

            AddHeaderParameter(httpClient, header);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJsonContentType);
            var response = await httpClient.PutAsync(uri, content);

            await HandleResponse(response);
            string responseCotent = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>JsonConvert.DeserializeObject<TResult>(responseCotent, _serializerSettings));            

            return result;
        }


        HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApplicationJsonContentType));

            if(!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token");
            }

            return httpClient;
        }

        void AddHeaderParameter(HttpClient client, string parameter)
        {
            if(client != null && !string.IsNullOrEmpty(parameter))
            {
                client.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
            }
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if(!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestException(response.StatusCode, content);
            }
        }
    }
}
