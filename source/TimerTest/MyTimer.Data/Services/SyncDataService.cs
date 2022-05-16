using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTimer.Data.Services
{
	public class SyncDataService : IHostedService
	{
		private readonly ITimerRepository _timerRepository;
		Timer timer;

		public SyncDataService(ITimerRepository timerRepository)
		{
			_timerRepository = timerRepository;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			TimeSpan interval = TimeSpan.FromSeconds(23);
			// Next run time is calculated as the next midnight to occur.  DateTime.Today is today's
			// date at midnight.  Adding one day to that makes it midnight tomorrow.
			var firstInterval = TimeSpan.FromSeconds(11);

			void action()
			{
				SyncData(null);
				var t1 = Task.Delay(firstInterval, cancellationToken);
				t1.Wait(cancellationToken);

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
			await _timerRepository.Log($"Log Message: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
		}



	}
}
