using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SteamMarketApi.FeedHostedService.Controllers
{
	[ApiController]
	[Route("test")]
	public class TestController : Controller
	{
		private readonly IInventoryItemsService _inventoryItemsService;

		public TestController(IInventoryItemsService inventoryItemsService)
		{
			_inventoryItemsService = inventoryItemsService;
		}

		[HttpGet]
		public async Task<IActionResult> Test()
		{
			await _inventoryItemsService.SaveDota2Prices(10);
			return Ok();
		}
	}
}