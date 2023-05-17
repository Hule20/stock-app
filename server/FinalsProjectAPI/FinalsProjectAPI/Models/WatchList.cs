using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinalsProjectAPI.Models
{
    public class WatchList
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int StockID { get; set; }


        public Stock Stock { get; set; }

        public User User { get; set; }

    }
}
