using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarDetector.DataModels;
using Newtonsoft.Json;

namespace CarDetector.Services
{
    public class DataProvider
    {
        private HttpClient _client = new HttpClient();
        private const string _host = "http://real.by:50052";

        public async Task<AiResponse> GetAiResponse(byte[] photoBytes)
        {
            string uri = $"{_host}/detect_model";

            var stringFromBytes = Convert.ToBase64String(photoBytes);
            var propertyDict = new Dictionary<string, string>()
            {
                {"image", stringFromBytes}
            };
            var contentString = JsonConvert.SerializeObject(propertyDict);
            var content = new StringContent(contentString);
            var response = await PostAsync<AiResponse>(uri, content);
            return response;
        }

        private async Task<T> PostAsync<T>(string uri, HttpContent content)
        {
            var response = await _client.PostAsync(uri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<T>(responseString);
            return responseContent;
        }
    }
}