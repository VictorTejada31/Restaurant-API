using System.Text.Json.Serialization;

namespace Restaurant.Core.Application.Dtos.Account
{
    public class AuthenticationResponse : Commons
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public IList<string> Roles { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }


    }
}
