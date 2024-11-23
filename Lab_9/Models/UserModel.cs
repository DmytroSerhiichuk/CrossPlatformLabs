using System.Text.Json.Serialization;

namespace Lab_9.Models
{
    public class UserModel
    {
        [JsonPropertyName("nickname")]
        public string UserName { get; set; } = "";
        [JsonPropertyName("name")]
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        [JsonPropertyName("phone_number")]
        public string Phone { get; set; } = "";
        public string Picture { get; set; } = "";
    }
}
