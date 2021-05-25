using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SteamMarketApi.FeedHostedService
{
    public partial class SteamItems
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string MarketName { get; set; }
        public string MarketHashName { get; set; }
        public string Image { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal MedianPrice { get; set; }
        public int Game { get; set; }
        public int AppId { get; set; }
    }
}
