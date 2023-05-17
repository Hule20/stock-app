using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinalsProjectAPI.Clients
{
    public class AlphaVantageClient : IAlphaVantageClient
    {
        private readonly HttpClient _httpClient;
        private readonly TestContext _testContext;

        public AlphaVantageClient(HttpClient httpClient, TestContext testContext)
        {
            _httpClient = httpClient;
            _testContext = testContext;
        }

        public async Task<List<StockHistoricPrice>> GetStockHistoricPrice(string ticker)
        {
            List<StockHistoricPrice> historicPriceList = new List<StockHistoricPrice>();

            var response = await _httpClient.GetAsync($"query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={ticker}&interval=1min&apikey={Config.ALPHA_VANTAGE_API_KEY}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponseContentRoot = JsonDocument.Parse(content).RootElement;

                await Console.Out.WriteLineAsync("content" + content);
                var stockInDb = await _testContext.Stocks.FirstOrDefaultAsync(s => s.Ticker == ticker);
                if (stockInDb == null)
                {
                    await _testContext.Stocks.AddAsync(stockInDb = new Stock
                    {
                        Ticker = jsonResponseContentRoot.GetProperty("Meta Data").GetProperty("2. Symbol").GetString(),
                        Company = "CompanyNamePH"
                    });

                    await _testContext.SaveChangesAsync();
                }

                var timeSeries = jsonResponseContentRoot.GetProperty("Time Series (Daily)");
                foreach (JsonProperty atDate in timeSeries.EnumerateObject())
                {
                    JsonElement atDateValues = atDate.Value;

                    historicPriceList.Add(new StockHistoricPrice
                    {
                        StockID = stockInDb.ID,
                        Date = DateTime.Parse(atDate.Name),
                        PriceLowest = decimal.Parse(atDateValues.GetProperty("3. low").GetString(), CultureInfo.InvariantCulture),
                        PriceHighest = decimal.Parse(atDateValues.GetProperty("2. high").GetString(), CultureInfo.InvariantCulture),
                        PriceClose = decimal.Parse(atDateValues.GetProperty("4. close").GetString(), CultureInfo.InvariantCulture),
                        Volume = int.Parse(atDateValues.GetProperty("6. volume").GetString(), CultureInfo.InvariantCulture),
                    });
                }
            }

            return historicPriceList;
        }
    }

    public interface IAlphaVantageClient
    {
        Task<List<StockHistoricPrice>> GetStockHistoricPrice(string ticker);
    }
}
