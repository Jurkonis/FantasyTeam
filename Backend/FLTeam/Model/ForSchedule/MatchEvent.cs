using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class MatchEvent
	{
		public string id { get; set; }
		public Strategy strategy { get; set; }
		public List<TeamEvent> teams { get; set; }
	}
}
