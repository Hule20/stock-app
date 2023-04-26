using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }

        [JsonIgnore]
        public IEnumerable<UserStock> UserStocks { get; set; } = new List<UserStock>();
    }
}
