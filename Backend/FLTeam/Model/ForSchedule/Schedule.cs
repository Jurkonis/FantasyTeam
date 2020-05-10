using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Schedule
	{
		public DateTime updated { get; set; }
		public Page pages { get; set; }
		public List<Event> events { get; set; }
	}
}
