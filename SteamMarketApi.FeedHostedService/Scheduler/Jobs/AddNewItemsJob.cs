using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace SteamMarketApi.FeedHostedService.Scheduler.Jobs
{
	public class AddNewItemsJob : IJob
	{
		private readonly ILogger<AddNewItemsJob> _logger;
		private readonly IInventoryItemsService _inventoryItemsService;

		public AddNewItemsJob(ILogger<AddNewItemsJob> logger, IInventoryItemsService inventoryItemsService)
		{
			_logger = logger;
			_inventoryItemsService = inventoryItemsService;
		}
		
		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				// await _inventoryItemsService.SetItems("76561198881491957");
				// await _inventoryItemsService.SavePrices();

				await _inventoryItemsService.SaveDota2Prices(10);
			}
			catch (Exception ex)
			{
				_logger.LogError(DateTime.Now + ex.Message);
			}
			
			var message = $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)}: Предметы обработаны - {_inventoryItemsService.LastPage}"; 
			_logger.LogInformation(message);
		}
	}
}