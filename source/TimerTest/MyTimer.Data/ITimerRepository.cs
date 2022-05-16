using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data
{
	public interface ITimerRepository
	{
		Task<int> Log(string message);
	}
}
