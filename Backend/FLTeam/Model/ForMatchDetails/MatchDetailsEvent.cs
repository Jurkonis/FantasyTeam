using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class MatchDetailsEvent
	{
		public string id { get; set; }
		public string type { get; set; }
		public Tournament tournament { get; set; }
		public League league { get; set; }
		public MatchDetails match { get; set; }
	}
}
