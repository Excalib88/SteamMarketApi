using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SteamMarketApi.Web.Exceptions;
using GameType = SteamMarketApi.Web.Entities.GameType;
using SteamItem = SteamMarketApi.Web.Entities.SteamItem;

namespace SteamMarketApi.Web.Services
{
	public class SteamMarketService : ISteamMarketService
	{
		#region DI, ctor

		private readonly DataContext _context;

		public SteamMarketService(DataContext context)
		{
			_context = context;
		}

		#endregion DI, ctor

		public async Task<SteamItem> GetItemById(long id)
		{
			var item = await _context.SteamItems.FirstOrDefaultAsync(x => x.Id == id);

			if (item == null)
			{
				throw new ItemNotFoundException("Steam item not found");
			}

			return item;
		}

		public async Task<SteamItem> GetItemByName(string name)
		{
			var item = await _context.SteamItems.FirstOrDefaultAsync(x => x.Name == name);

			if (item == null)
			{
				throw new ItemNotFoundException("Steam item not found");
			}

			return item;
		}

		public List<SteamItem> GetItemsByGame(GameType gameType)
		{
			var items = _context.SteamItems.Where(x => x.Game == gameType).ToList();

			if (items == null || !items.Any())
			{
				throw new ItemNotFoundException("Steam items not found");
			}

			return items;
		}

		public List<SteamItem> GetItemsByName(string[] names)
		{
			var items = _context.SteamItems.Where(x => names.Contains(x.Name)).ToList();

			return items;
		}

		public async Task<long> AddItem(SteamItem steamItem)
		{
			var result = await _context.SteamItems.AddAsync(steamItem);

			return result.Entity.Id;
		}
	}
}