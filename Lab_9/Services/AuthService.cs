using Lab_9.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Lab_9.Services
{
    public static class AuthService
    {
        private static readonly HttpClient _httpClient;
        public const string TOKEN_KEY = "access_token";

        private const string _DOMAIN = "dev-uvnmpcrfbz07kkku.us.auth0.com";
        private const string _CLIENT_ID = "RSY0UONcs0tcGeD4sRwPILijvgLbcIDo";
        private const string _CLIENT_SECRET = "U4MH_xSWczLKd8J15rGGLLk1UdTxB60mmeUaDBv6AaVgz8n5QfMQdQnW1ewB9AcM";
        private const string _AUDIENCE = "https://dev-uvnmpcrfbz07kkku.us.auth0.com/api/v2/";
        private const string _CONNECTION = "Username-Password-Authentication";
        private const string _LAB_6_API_URL = "http://192.168.31.82:3001";

        static AuthService()
        {
            _httpClient = new HttpClient();
        }

        public static async Task SaveTokenAsync(string token)
        {
            await SecureStorage.SetAsync(TOKEN_KEY, token);
        }
        public static async Task<string> GetTokenAsync()
        {
            return await SecureStorage.GetAsync(TOKEN_KEY) ?? "";
        }
        public static void RemoveToken()
        {
            SecureStorage.Remove(TOKEN_KEY);
        }

        public static async Task<string> AuthenticateUserAsync(LoginModel loginModel)
        {
            var requestUrl = $"https://{_DOMAIN}/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            request.Content = JsonContent.Create(new
            {
                grant_type = "password",
                username = loginModel.Email,
                password = loginModel.Password,
                audience = _AUDIENCE,
                client_id = _CLIENT_ID,
                client_secret = _CLIENT_SECRET,
                scope = "openid profile email phone"
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseContent.GetProperty("access_token").GetString();
        }

        public static async Task<UserModel> GetUserInfoAsync(string token)
        {
            var requestUrl = $"https://{_DOMAIN}/userinfo";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserModel>();
        }
    }
}
