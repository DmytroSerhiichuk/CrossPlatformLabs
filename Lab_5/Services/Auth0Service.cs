using Lab_5.Models;
using System.Text.Json;

namespace Lab_5.Services
{
	public class Auth0Service
    {
        private readonly HttpClient _httpClient;

        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;
        private readonly string _connection;

        public Auth0Service(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _domain = configuration["Auth0:Domain"];
            _clientId = configuration["Auth0:ClientId"];
            _clientSecret = configuration["Auth0:ClientSecret"];
            _audience = configuration["Auth0:Audience"];
            _connection = configuration["Auth0:Connection"];
        }

        public async Task<string> GetManagementApiTokenAsync()
        {
            var tokenEndpoint = $"https://{_domain}/oauth/token";
            var tokenRequest = new
            {
                client_id = _clientId,
                client_secret = _clientSecret,
                audience = _audience,
                grant_type = "client_credentials"
            };

            var response = await _httpClient.PostAsJsonAsync(tokenEndpoint, tokenRequest);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseContent.GetProperty("access_token").GetString();
        }

        public async Task CreateUserAsync(SignUpViewModel model)
        {
            var token = await GetManagementApiTokenAsync();
            var requestUrl = $"https://{_domain}/api/v2/users";

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            request.Content = JsonContent.Create(new
            {
                nickname = model.UserName,
                name = model.FullName,
                password = model.Password,
                connection = _connection,
                email = model.Email,
                email_verified = false,
                phone_number = model.PhoneNumber,
                phone_verified = false
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            var requestUrl = $"https://{_domain}/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            request.Content = JsonContent.Create(new
            {
                grant_type = "password",
                username = email,
                password = password,
                audience = _audience,
                client_id = _clientId,
                client_secret = _clientSecret,
                scope = "openid profile email phone"
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseContent.GetProperty("access_token").GetString();
        }

        public async Task<JsonElement> GetUserInfoAsync(string token)
        {
            var requestUrl = $"https://{_domain}/userinfo";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }
    }
}
