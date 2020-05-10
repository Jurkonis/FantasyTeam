using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Event
	{
		public DateTime startTime { get; set; }
		public string state { get; set; }
		public string type { get; set; }
		public string blockName { get; set; }
		public League league { get; set; }
		public MatchEvent match { get; set; }
	}
}
