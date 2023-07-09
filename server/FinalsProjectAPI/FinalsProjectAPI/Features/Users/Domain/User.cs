using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Features.Users.Domain
{
    public class User
    {
        [JsonIgnore]
        public int ID { get; set; }

        [DefaultValue("")]
        [Required]
        public string FirstName { get; set; }
        [DefaultValue("")]
        [Required]
        public string LastName { get; set; }

        [DefaultValue("")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [DefaultValue("")]
        [Required]
        public string Password { get; set; }


        [JsonIgnore]
        public ICollection<Watchlist> Watchlist { get; set; } = new List<Watchlist>();
    }
}
