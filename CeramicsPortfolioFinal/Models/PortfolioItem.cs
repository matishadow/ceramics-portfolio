using Contentful.Core.Models;

namespace CeramicsPortfolioFinal.Models
{
    public partial class PortfolioItem
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public Asset FullSizeImage { get; set; }
        public Asset ThumbnailImage { get; set; }
    }
}