using MyTimer.Data.Contexts;
using MyTimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data
{
	public class TimerRepository : ITimerRepository
	{
		private readonly MtDbContext _context;

		public TimerRepository(MtDbContext context)
		{
			_context = context;
		}

		public async Task<int> Log(string message)
		{
			var log = new SyncLog
			{
				Message = message,
				LogTime = DateTime.Now
			};

			if ((_context != null) && (_context.sync_log != null))
			{
				_context.sync_log.Add(log);
				return _context.SaveChanges();
			}
			else
			{
				return 0;
			}
		}

	}
}
