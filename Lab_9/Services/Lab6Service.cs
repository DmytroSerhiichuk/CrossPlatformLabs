using System.Net.Http.Json;

namespace Lab_9.Services
{
    public static class Lab6Service
    {
        private static readonly HttpClient _httpClient;

        private const string _LAB_6_URL = "http://192.168.31.82:3001";

        static Lab6Service()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<T> GetData<T>(string token, string url)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException();
            }

            var requestUrl = $"{_LAB_6_URL}/api/{url}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task PostData<T>(string token, string url, T data)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException();
            }

            var requestUrl = $"{_LAB_6_URL}/api/{url}";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            request.Content = JsonContent.Create(data);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
