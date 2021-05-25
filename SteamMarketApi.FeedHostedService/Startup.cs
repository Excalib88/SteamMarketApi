using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SteamMarketApi.FeedHostedService.Scheduler;
using SteamMarketApi.FeedHostedService.Scheduler.Jobs;

namespace SteamMarketApi.FeedHostedService
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<SteamMarketApiContext>(o => o.UseNpgsql(_configuration.GetConnectionString("Db")));
			services.AddSingleton<IJobFactory, JobFactory>();
			services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
			services.AddSingleton<QuartzJobRunner>();
			services.AddSingleton<IInventoryItemsService, InventoryItemsService>();
			services.AddHostedService<SchedulerService>();

			services.AddSingleton<AddNewItemsJob>();

			services.AddSingleton(new JobSchedule(typeof(AddNewItemsJob), "0 0/5 * ? * * *"));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
				endpoints.MapControllers());
		}
	}
}