using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public ICollection<UserStock> UserStocks { get; set; } = new List<UserStock>();
    }
}
