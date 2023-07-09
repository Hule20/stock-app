using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Features.Users.Domain
{
    public class UserPartialUpdateDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }


        [JsonIgnore]
        public ICollection<Watchlist> Watchlist { get; set; } = new List<Watchlist>();
    }
}
