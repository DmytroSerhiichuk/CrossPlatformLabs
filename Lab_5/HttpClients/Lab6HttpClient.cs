using Lab_5.ResponseModels;
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

		public async Task<List<BookingResponse>> GetBookings(string token)
		{
			var requestUrl = $"{_lab6Url}/api/bookings";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<List<BookingResponse>>();
		}
		public async Task<BookingResponseDetailed> GetBooking(string token, int id)
		{
			var requestUrl = $"{_lab6Url}/api/booking/{id}";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<BookingResponseDetailed>();
		}

        public async Task<List<ModelResponse>> GetModels(string token)
        {
            var requestUrl = $"{_lab6Url}/api/models";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ModelResponse>>();
        }
		public async Task<ModelResponseDetailed> GetModel(string token, string code)
		{
			var requestUrl = $"{_lab6Url}/api/model/{code}";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<ModelResponseDetailed>();
		}

		public async Task<List<BookingStatusResponse>> GetBookingStatuses(string token)
		{
			var requestUrl = $"{_lab6Url}/api/booking-statuses";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<List<BookingStatusResponse>>();
		}
		public async Task<BookingStatusResponseDetailed> GetBookingStatus(string token, string code)
		{
			var requestUrl = $"{_lab6Url}/api/booking-status/{code}";
			var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<BookingStatusResponseDetailed>();
		}
	}
}
