using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class TeamGameStats
	{
		public int totalGold { get; set; }
		public int inhibitors { get; set; }
		public int towers { get; set; }
		public int barons { get; set; }
		public int totalkills { get; set; }
		public List<string> dragons { get; set; }
		public List<Participants> participants { get; set; }
	}
}
