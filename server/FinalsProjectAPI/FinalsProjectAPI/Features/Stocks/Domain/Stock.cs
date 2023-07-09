using FinalsProjectAPI.Features.Users.Domain;
using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Features.Stocks.Domain
{
    public class Stock
    {
        public int ID { get; set; }
        public string? Ticker { get; set; }
        public string? Company { get; set; }

        [JsonIgnore]
        public IEnumerable<Watchlist> Watchlist { get; set; } = new List<Watchlist>();
    }
}
