using BreadOne.Models;
using System.Text.Json;

namespace BreadOne.Services
{
    public class PortfolioServiceJsonFile
    {
        public IEnumerable<Portfolio>? GetPortfolios()
        {
            var jsonFileName = @"C:\study\aspnetcore\RazorPages\BreadOne.RazorPages\BreadOne\wwwroot\Portfolios\portfolio.json";

            using var jsonFileReader = File.OpenText(jsonFileName);

            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var portfolios = JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);

            return portfolios;
        }
    }
}
