using Contentful.Core.Models;

namespace CeramicsPortfolioFinal.Models;

public class Portfolio
{
    public SystemProperties Sys { get; set; }
    public List<PortfolioItem> PortfolioItems { get; set; }
}