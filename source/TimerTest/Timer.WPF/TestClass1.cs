using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.WPF
{
	public class TestClass1
	{
		public string GetSomethingBack(ViewModelMain vmm)
		{
			return vmm.StatusMessage + "\n This seemed to work";
		}

	}
}
