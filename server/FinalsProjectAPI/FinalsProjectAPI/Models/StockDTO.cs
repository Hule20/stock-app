using System.Diagnostics;

namespace FinalsProjectAPI.Models
{
    public class StockDTO
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }

        public static StockDTO MapFrom(Stock stock)
        {
            var stockDto = new StockDTO();

            stockDto.ID = stock.ID;
            stockDto.Ticker = stock.Ticker;
            stockDto.Company = stock.Company;

            return stockDto;
        }
    }

    public class StockDTODetailed
    {
        public int ID { get; set; }
        public string Ticker { get; set; }
        public string Company { get; set; }
    }
}
