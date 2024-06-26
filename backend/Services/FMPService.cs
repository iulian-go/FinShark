using backend.Models;
using Newtonsoft.Json;

namespace backend;

public class FMPService : IFMPService
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration config;

    public FMPService(HttpClient httpClient, IConfiguration config)
    {
        this.httpClient = httpClient;
        this.config = config;
    }

    public async Task<Stock> FindStockBySymbolAsync(string symbol)
    {
        try
        {
            var result = await this.httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={config["FMPKey"]}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                var stock = tasks[0];
                if (stock != null) return stock.ToStock();

                return null;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}
