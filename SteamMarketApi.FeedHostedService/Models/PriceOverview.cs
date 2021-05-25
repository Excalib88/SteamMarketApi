using Newtonsoft.Json;

namespace SteamMarketApi.FeedHostedService.Models
{
	public class PriceOverview
	{
		public bool Success { get; set; }
		[JsonProperty("lowest_price")]
		public string LowestPrice { get; set; }
		public string Volume { get; set; }
		[JsonProperty("median_price")]
		public string MedianPrice { get; set; }
	}
}