using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AVI_Data.Services
{
	public class SyncDataService : IHostedService
	{
		Timer timer;
		//private readonly IAgRepository _agRepository;

		public SyncDataService()
		{
			//_agRepository = agRepository;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			TimeSpan interval = TimeSpan.FromHours(24);
			// Next run time is calculated as the next midnight to occur.  DateTime.Today is today's
			// date at midnight.  Adding one day to that makes it midnight tomorrow.
			var nextRunTime = DateTime.Today.AddDays(1);
			var curTime = DateTime.Now;
			var firstInterval = nextRunTime.Subtract(curTime);

			void action()
			{
				var t1 = Task.Delay(firstInterval, cancellationToken);
				t1.Wait(cancellationToken);
				SyncData(null);

				timer = new(SyncData,
										null,
										TimeSpan.Zero,
										interval);
			}

			Task.Run(action, cancellationToken);
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		private async void SyncData(Object stateInfo)
		{
		}



	}
}
