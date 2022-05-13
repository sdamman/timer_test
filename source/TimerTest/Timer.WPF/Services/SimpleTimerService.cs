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
		private int invokeCount = 0;
		private ViewModelMain modelMain;

		public void StartSimple(ViewModelMain vmm)
		{
			modelMain = vmm;
			modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} Creating timer. \n";

			Timer timer = new(Check,
													autoEvent,
													TimeSpan.FromSeconds(1),
													TimeSpan.FromSeconds(0.5));

			autoEvent.WaitOne();
			timer.Dispose();
			modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} \nDestroying timer.";
		}

		public void Check(Object stateInfo)
		{
			AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
			modelMain.StatusMessage += $"{DateTime.Now:h:mm:ss.ff} Checking status {++invokeCount}\n";

			//if (invokeCount == maxCount)
			//{
			//	invokeCount = 0;
			//	autoEvent.Set();
			//}
		}


	}
}
