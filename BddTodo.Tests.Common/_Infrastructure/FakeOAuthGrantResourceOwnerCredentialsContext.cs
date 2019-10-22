using Newtonsoft.Json;

namespace BddTodo.Tests._Infrastructure
{
    public class FakeOAuthGrantResourceOwnerCredentialsContext
    {

        //[JsonProperty(PropertyName = "client_id")]
        public string client_id { get; set; }

        //[JsonProperty(PropertyName = "grant_type")]
        public string grant_type { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

    }
}
