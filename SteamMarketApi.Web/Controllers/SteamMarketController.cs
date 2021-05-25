using Microsoft.AspNetCore.Mvc;
using SteamMarketApi.Web.Services;

namespace SteamMarketApi.Web.Controllers
{
	[ApiController]
	[Route("steam-market")]
	public class SteamMarketController : ControllerBase
	{
		private readonly ISteamMarketService _steamMarketService;

		public SteamMarketController(ISteamMarketService steamMarketService)
		{
			_steamMarketService = steamMarketService;
		}

		[HttpGet("item/{id}")]
		public IActionResult GetItemById(long id)
		{
			return Ok();
		}

		[HttpPost("items")]
		public IActionResult GetItemsByName(string[] names)
		{
			var result = _steamMarketService.GetItemsByName(names);

			return Ok(result);
		}
	}
	
}