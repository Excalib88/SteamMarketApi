using System.Collections.Generic;
using System.Text.Json.Serialization;
using SteamMarketApi.FeedHostedService.Converters;

namespace SteamMarketApi.FeedHostedService.Models
{
	public class RgInventoryResponse
	{
		public bool Success { get; set; }
		
		public Dictionary<string, RgItem> RgInventory { get; set; }
		
		[JsonConverter(typeof(DictionaryConverter))]
		public Dictionary<string, RgDescription> RgDescriptions { get; set; }
	}
}