using Lab_5.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Lab_5.HttpClients
{
	public class Auth0HttpClient
    {
        private readonly HttpClient _httpClient;

        private readonly AuthConfig _authConfig;

        public Auth0HttpClient(HttpClient httpClient, IOptions<AuthConfig> options)
        {
            _httpClient = httpClient;

			_authConfig = options.Value;
        }

        public async Task<string> GetManagementApiTokenAsync()
        {
            var tokenEndpoint = $"https://{_authConfig.Domain}/oauth/token";
            var tokenRequest = new
            {
                client_id = _authConfig.ClientId,
                client_secret = _authConfig.ClientSecret,
                audience = _authConfig.Audience,
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
            var requestUrl = $"https://{_authConfig.Domain}/api/v2/users";

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            request.Content = JsonContent.Create(new
            {
                nickname = model.UserName,
                name = model.FullName,
                password = model.Password,
                connection = _authConfig.Connection,
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
            var requestUrl = $"https://{_authConfig.Domain}/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            request.Content = JsonContent.Create(new
            {
                grant_type = "password",
                username = email,
                password = password,
                audience = _authConfig.Audience,
                client_id = _authConfig.ClientId,
                client_secret = _authConfig.ClientSecret,
                scope = "openid profile email phone"
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseContent.GetProperty("access_token").GetString();
        }

        public async Task<JsonElement> GetUserInfoAsync(string token)
        {
            var requestUrl = $"https://{_authConfig.Domain}/userinfo";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<JsonElement>();
        }
    }
}
