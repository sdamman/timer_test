using Microsoft.EntityFrameworkCore;
using MyTimer.Data.Models;
using MyTimer.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data.Contexts
{
	public class MtDbContext : DbContext
	{
		public DbSet<SyncLog>? sync_log { get; set; }

		public MtDbContext()
		{
		}

		public MtDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Connection.Text);
		}

	}
}
