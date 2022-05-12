using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Timer.WPF.Classes
{
	public class Checker
	{
		private int invokeCount;
		private int maxCount;

		public Checker(int count)
		{
			invokeCount = 0;
			maxCount = count;
		}

		public async void Check(Object stateInfo)
		{
			AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
			Console.WriteLine($"{DateTime.Now:h:mm:ss.ff} Checking status {++invokeCount}");

			if (invokeCount == maxCount)
			{
				invokeCount = 0;
				autoEvent.Set();
			}
		}

	}
}
