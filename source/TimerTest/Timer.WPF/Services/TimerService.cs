using Microsoft.Extensions.Hosting;
using MyTimer.WPF.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyTimer.WPF.Services
{
	public class TimerService : IHostedService
	{
		Timer timer;
		private ViewModelMain modelMain;
		private int invokeCount = 0;

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var autoEvent = new AutoResetEvent(false);
			//var modelMain = new ViewModelMain();

			string msg = $"{DateTime.Now:h:mm:ss.ff} Creating timer. \n";
			Debug.WriteLine(msg);
			//MessageBox.Show(msg);
			modelMain.StatusMessage += msg;
			var interval = TimeSpan.FromSeconds(2);

			void action()
			{
				autoEvent.WaitOne();

				timer = new(Check,
										null,
										TimeSpan.Zero,
										interval);
			}

			Task.Run(action, cancellationToken);
			return Task.CompletedTask;

		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			if (timer != null)
			{
				timer.Dispose();
				string msg = $"{DateTime.Now:h:mm:ss.ff} Destroyed timer. \n";
				Debug.WriteLine(msg);
				//MessageBox.Show(msg);
				modelMain.StatusMessage += msg;
			}
			return Task.CompletedTask;
		}

		public void Check(Object stateInfo)
		{
			AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
			string msg = $"{DateTime.Now:h:mm:ss.ff} Checking status {++invokeCount}\n";
			Debug.WriteLine(msg);
			//MessageBox.Show(msg);
			modelMain.StatusMessage += msg;

			//if (invokeCount == maxCount)
			//{
			//	invokeCount = 0;
			//	autoEvent.Set();
			//}
		}

		public void StartTimer(ViewModelMain vmm)
		{
			modelMain = vmm;
			CancellationTokenSource source = new();
			source.CancelAfter(Timeout.Infinite);
			CancellationToken token = source.Token;
			StartAsync(token);
		}

	}
}
