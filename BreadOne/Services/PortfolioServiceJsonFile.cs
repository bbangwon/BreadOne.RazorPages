using BreadOne.Models;
using System.Text.Json;

namespace BreadOne.Services;

public class PortfolioServiceJsonFile
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PortfolioServiceJsonFile(IWebHostEnvironment webHostEnvironment)
    {
        this._webHostEnvironment = webHostEnvironment;
    }

    private string JsonFileName
    {
        get
        {
            return Path.Combine(_webHostEnvironment.WebRootPath, "Portfolios", "portfolio.json");
        }
    }

    public IEnumerable<Portfolio>? GetPortfolios()
    {
        using var jsonFileReader = File.OpenText(JsonFileName);

        var options = new JsonSerializerOptions()
        {
            //대소문자 구분하지 않음
            PropertyNameCaseInsensitive = true
        };
        var portfolios = JsonSerializer.Deserialize<Portfolio[]>(jsonFileReader.ReadToEnd(), options);

        return portfolios;
    }

    public void AddRating(int portfolioId, int rating)
    {
        var portfolios = GetPortfolios();
        var portfolio = portfolios?.FirstOrDefault(p => p.Id == portfolioId);
        if (portfolio == null)
            return;       

        if (portfolio.Ratings == null)
        {
            portfolio.Ratings = new List<int> { rating };
        }
        else
        {
            portfolio.Ratings.Add(rating);
        }

        using var outputStream = File.OpenWrite(JsonFileName);
        JsonSerializer.Serialize(new Utf8JsonWriter(outputStream,
                new JsonWriterOptions
                {
                    SkipValidation = true,
                    Indented = true,
                }), portfolios!);
    }
}

