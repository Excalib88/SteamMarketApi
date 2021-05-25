using Newtonsoft.Json;

namespace SteamMarketApi.FeedHostedService.Models
{
	[JsonObject]
	public class RgDescription
	{
		public string AppId { get; set; }
		public string ClassId { get; set; }
		public string InstanceId { get; set; }

		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }

		public string Name { get; set; }
		[JsonProperty("market_hash_name")]
		public string MarketHashName { get; set; }
		
		[JsonProperty("market_name")]
		public string MarketName { get; set; }
		public int Tradable { get; set; }
		
		public int Marketable { get; set; }
	}
}