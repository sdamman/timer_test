using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data.Models
{
	public class SyncLog
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(400)]
		public string? Message { get; set; }
		public DateTime LogTime { get; set; }
	}
}
