using Contentful.Core.Models;

namespace CeramicsPortfolio.Blazor.Models;

public class Portfolio
{
    public SystemProperties Sys { get; set; }
    public List<PortfolioItem> PortfolioItems { get; set; }
}