namespace FinalsProjectAPI.Models
{
    public class StockHistoricPrice
    {
        public int ID { get; set; }
        public int StockID { get; set; }
        public DateTime Date { get; set; }
        public decimal PriceLowest { get; set; }
        public decimal PriceHighest { get; set; }
        public decimal PriceClose { get; set; }
        public int Volume { get; set; }


        public Stock Stock { get; set; }
    }
}
