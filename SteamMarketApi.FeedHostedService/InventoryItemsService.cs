using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteamMarketApi.FeedHostedService.Extensions;
using SteamMarketApi.FeedHostedService.Models;
using SteamMarketApi.Web.Entities;

namespace SteamMarketApi.FeedHostedService
{
	public class InventoryItemsService : IInventoryItemsService
	{
		public int LastPage { get; set; } = 17970;
		public List<RgDescription> RgDescriptions { get; set; } = new List<RgDescription>();
		public List<RgDescription> DeletedRgDescriptions { get; set; } = new List<RgDescription>();

		private readonly ILogger<InventoryItemsService> _logger;
		private readonly HttpClient _client;

		public InventoryItemsService(ILogger<InventoryItemsService> logger)
		{
			_logger = logger;
			_client = new HttpClient
			{
				BaseAddress = new Uri(UrlConstants.SteamCommunityUrl)
			};
		}
		
		public async Task<List<RgDescription>> GetItemDetails(List<string> itemIds, string steamId)
		{
			var response = await _client.GetAsync($"/profiles/{steamId}/inventory/json/570/2");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent) 
				             ?? throw new Exception("RgInventory not found");

				var resultCollection = new List<RgDescription>();

				foreach (var itemId in itemIds)
				{
					var item = result.RgInventory[itemId] ?? throw new Exception("Item not found");
					var key = item.ClassId + "_" + item.InstanceId;
					var rgDescription = result.RgDescriptions[key] ?? throw new Exception("RgDescription not found");
					rgDescription.IconUrl = UrlConstants.ImageServiceUrl + rgDescription.IconUrl;
					
					resultCollection.Add(rgDescription);
				}

				return resultCollection;
			}

			return null;
		}

		public async Task<List<string>> GetItemIdsBySteamId(string steamId)
		{
			var response = await _client.GetAsync($"/profiles/{steamId}/inventory/json/570/2");

			if (response.IsSuccessStatusCode)
			{
				var stringContent = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<RgInventoryResponse>(stringContent) ?? 
				             throw new Exception("RgInventory not found");
				return result.RgInventory.Keys.ToList();
			}

			return null;
		}

		public async Task SetItems(string steamId)
		{
			var itemIds = await GetItemIdsBySteamId(steamId);

			if (itemIds == null || !itemIds.Any())
			{
				throw new Exception("Item ids null");
			}
			
			var items = await GetItemDetails(itemIds, steamId);
			
			if (items == null || !items.Any())
			{
				throw new Exception("Items null");
			}

			RgDescriptions = items;
		}
		
		public async Task<PriceOverview> GetPrice(int appId, string marketHashName)
		{
			var response = await _client.GetAsync(
				$"https://steamcommunity.com/market/priceoverview/?appid={appId}&currency=1&market_hash_name={marketHashName}");

			if(response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<PriceOverview>(content) ?? 
				             throw new Exception("Price not found");

				return result;
			}

			return null;
		}

		public async Task SavePrices(int take = 30)
		{
			await using var dataContext = new SteamMarketApiContext();
			
			foreach (var rgDescription in RgDescriptions)
			{
				var item = await dataContext.SteamItems.FirstOrDefaultAsync(x => x.Name == rgDescription.Name);
				if (item != null) continue;
			
				var appId = Convert.ToInt32(rgDescription.AppId);
				var marketHashNameWithoutSpaces = rgDescription.Name.Replace(" ", "%20");
				var price = await GetPrice(appId, marketHashNameWithoutSpaces);
			
				if(price == null) break;
			
				var newItem = new SteamItems
				{
					Game = rgDescription.AppId == "570" ? (int)GameType.Dota2 : (int)GameType.Unknown,
					Name = rgDescription.Name,
					AppId = appId,
					MarketName = rgDescription.MarketName,
					MarketHashName = rgDescription.MarketHashName,
					NormalizedName = rgDescription.Name.Replace(" ", "%20"),
					LowestPrice = price.LowestPrice.ParsePrice(),
					MedianPrice = price.MedianPrice.ParsePrice(),
					Image = rgDescription.IconUrl
				};
			
				await dataContext.SteamItems.AddAsync(newItem);
				DeletedRgDescriptions.Add(rgDescription);
			}
			
			foreach (var deletedRgDescription in DeletedRgDescriptions)
			{
				RgDescriptions.Remove(deletedRgDescription);
			}
			
			await dataContext.SaveChangesAsync();
		}

		public async Task SaveDota2Prices(int take)
		{
			if (LastPage >= 33700) LastPage = 0;

			var maxPage = take * 100 + LastPage;
			
			for (int page = LastPage; page <= maxPage; page+=100)
			{
				var url =
					$"https://steamcommunity.com/market/search/render/?start={page}&count=100&search_descriptions=0&sort_column=default&sort_dir=desc&appid=570&norender=1&count=33700";

				var get = await _client.GetStringAsync(url);
				var result = JsonConvert.DeserializeObject<MarketPrices>(get);

				using (var context = new SteamMarketApiContext())
				{
					foreach (var item in result?.results)
					{
						var itemEntity = await context.SteamItems.FirstOrDefaultAsync(x => x.MarketHashName == item.hash_name);

						if (itemEntity == null)
						{
							var entity = new SteamItems
							{
								Game = (int)GameType.Dota2,
								Name = item.name,
								Image = "https://community.akamai.steamstatic.com/economy/image/" + item.asset_description.icon_url,
								AppId = 570,
								LowestPrice = item.sell_price_text.ParsePrice(),
								MarketName = item.hash_name,
								MedianPrice = item.sell_price_text.ParsePrice(),
								NormalizedName = item.hash_name.Replace(" ", "%20"),
								MarketHashName = item.hash_name
							};
							await context.SteamItems.AddAsync(entity);
						}
						else
						{
							itemEntity.LowestPrice = item.sell_price_text.ParsePrice();
							itemEntity.MedianPrice = item.sell_price_text.ParsePrice();
						}
					}

					await context.SaveChangesAsync();
				}

				LastPage += 100;
			}
		}
	}
}