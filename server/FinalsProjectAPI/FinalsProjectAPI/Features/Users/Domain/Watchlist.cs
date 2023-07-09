using FinalsProjectAPI.Features.Stocks.Domain;
using System.ComponentModel.DataAnnotations;

namespace FinalsProjectAPI.Features.Users.Domain
{
    public class Watchlist
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int StockID { get; set; }


        public Stock Stock { get; set; }

        public User User { get; set; }

    }
}
