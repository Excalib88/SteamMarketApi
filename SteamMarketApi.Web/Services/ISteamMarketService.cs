using System.Collections.Generic;
using System.Threading.Tasks;
using GameType = SteamMarketApi.Web.Entities.GameType;
using SteamItem = SteamMarketApi.Web.Entities.SteamItem;

namespace SteamMarketApi.Web.Services
{
	public interface ISteamMarketService
	{
		Task<SteamItem> GetItemById(long id);
		Task<SteamItem> GetItemByName(string name);
		List<SteamItem> GetItemsByGame(GameType gameType);

		List<SteamItem> GetItemsByName(string[] names);
		Task<long> AddItem(SteamItem steamItem);
	}
}