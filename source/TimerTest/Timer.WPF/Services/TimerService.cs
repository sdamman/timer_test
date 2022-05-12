using Microsoft.Extensions.Hosting;
using MyTimer.WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTimer.WPF.Services
{
	public class TimerService : IHostedService
	{
		Timer? timer;

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var autoEvent = new AutoResetEvent(false);
			var checker = new Checker(10);

			//Console.WriteLine($"{DateTime.Now:h:mm:ss.ff} Creating timer. \n");
			var interval = TimeSpan.FromSeconds(2);

			void action()
			{
				autoEvent.WaitOne();
				//var t1 = Task.Delay(firstInterval, cancellationToken);
				//t1.Wait(cancellationToken);
				//checker.Check(null);

				timer = new(checker.Check,
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


	}
}
