using Newtonsoft.Json;

namespace BddTodo.Tests._Infrastructure
{
    public class FakeOAuthAuthenticationResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = ".issued")]
        public string Issued { get; set; }
        [JsonProperty(PropertyName = ".expires")]
        public string Expires { get; set; }
    }
}
