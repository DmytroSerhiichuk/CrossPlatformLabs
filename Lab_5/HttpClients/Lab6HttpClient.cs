﻿using Lab_5.Models;
namespace Lab_5.HttpClients
{
	public class Lab6HttpClient
	{
		private readonly HttpClient _httpClient;

		private readonly string _lab6Url;
		private readonly string _apiUrl;

		public Lab6HttpClient(HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;

			_lab6Url = configuration["Lab6:Url"];
		}

		public async Task<List<Vehicle>> GetVehicles(string token)
		{
			var requestUrl = $"{_lab6Url}/api/vehicles";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<List<Vehicle>>();
		}
	}
}