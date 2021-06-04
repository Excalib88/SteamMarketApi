using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SteamMarketApi.Web.Scheduler;
using SteamMarketApi.Web.Scheduler.Jobs;
using SteamMarketApi.Web.Services;

namespace SteamMarketApi.Web
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
			services.AddDbContext<DataContext>(o => o.UseSqlServer(_configuration.GetConnectionString("Db")));
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "SteamMarket API", Version = "v1" });
			});
			services.AddScoped<ISteamMarketService, SteamMarketService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "SteamGames API v1");
				x.RoutePrefix = "swagger";
			});
			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}