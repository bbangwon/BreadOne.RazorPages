using BreadOne.Models;
using BreadOne.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreadOne.Pages.Portfolios
{
	public class IndexModel : PageModel
    {
		private readonly PortfolioServiceJsonFile _portfolioServiceJsonFile;

		public IndexModel(PortfolioServiceJsonFile portfolioServiceJsonFile)
		{
			this._portfolioServiceJsonFile = portfolioServiceJsonFile;
		}

		public IEnumerable<Portfolio>? Portfolios { get; private set; }

		public void OnGet()
        {
			Portfolios = _portfolioServiceJsonFile.GetPortfolios();
		}
    }
}
