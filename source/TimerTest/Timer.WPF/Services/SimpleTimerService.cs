using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTimer.WPF.Services
{
	public class SimpleTimerService
	{
		private AutoResetEvent autoEvent = new(false);
		private int invokeCount;
		private ViewModelMain modelMain;

		public Task StartSimple(ViewModelMain vmm)
		{
			invokeCount = 0;
			modelMain = vmm;
			modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} Creating timer. \n";
			void action()
			{
				Timer timer = new(Check, autoEvent,
													TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
			}
			//timer.Dispose();
			//modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} \nDestroying timer.";
			Task.Run(action);
			return Task.CompletedTask;
		}

		private void Check(Object stateInfo)
		{
			autoEvent.Set();
			modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} Checking status {++invokeCount}\n";

			//if (invokeCount == maxCount)
			//{
			//	invokeCount = 0;
			//	autoEvent.Set();
			//}
		}



	}
}
