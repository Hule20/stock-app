using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }

        [JsonIgnore]
        public IEnumerable<WatchList> UserStocks { get; set; } = new List<WatchList>();
    }
}
