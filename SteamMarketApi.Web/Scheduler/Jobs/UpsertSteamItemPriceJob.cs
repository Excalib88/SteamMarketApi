using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using SchedulerConstants = SteamMarketApi.Web.Constants.SchedulerConstants;

namespace SteamMarketApi.Web.Scheduler.Jobs
{
	public class UpsertSteamItemPriceJob : IJob
	{
		private readonly DataContext _context;
		private readonly IConfiguration _configuration;
		private readonly ILogger<UpsertSteamItemPriceJob> _logger;

		public UpsertSteamItemPriceJob(ILogger<UpsertSteamItemPriceJob> logger)
		{
			_logger = logger;
			_context = null;
			_configuration = null;
		}

		public Task Execute(IJobExecutionContext context)
		{
			if(!int.TryParse(_configuration[SchedulerConstants.ItemsPerJobConfiguration], out var itemsPerJob))
			{
				throw new Exception("Change ItemsPerJob param in configuration");
			}
			
			//todo: реализовать очередь
			var items = _context.SteamItems.TakeLast(itemsPerJob).Skip(90);
			_logger.LogInformation("Hello world!");
			return Task.CompletedTask;
		}
	}
}