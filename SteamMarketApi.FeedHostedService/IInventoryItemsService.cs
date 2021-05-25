using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamMarketApi.FeedHostedService.Models;

namespace SteamMarketApi.FeedHostedService
{
	public interface IInventoryItemsService
	{
		int LastPage { get; set; }
		public List<RgDescription> RgDescriptions { get; set; }

		Task<List<RgDescription>> GetItemDetails(List<string> itemIds, string steamId);
		Task<List<string>> GetItemIdsBySteamId(string steamId);
		Task SetItems(string steamId);
		Task<PriceOverview> GetPrice(int appId, string marketHashName);
		Task SavePrices(int take = 30);
		Task SaveDota2Prices(int take);
	}
}