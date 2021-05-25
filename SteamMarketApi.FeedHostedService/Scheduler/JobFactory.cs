﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace SteamMarketApi.FeedHostedService.Scheduler
{
	public class JobFactory : IJobFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public JobFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
		{
			return _serviceProvider.GetRequiredService<QuartzJobRunner>();
		}

		public void ReturnJob(IJob job)
		{
			// we let the DI container handler this
		}
	}
}