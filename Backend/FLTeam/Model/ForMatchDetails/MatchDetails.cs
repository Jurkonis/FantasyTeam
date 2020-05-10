using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class MatchDetails
	{
		public Strategy strategy { get; set; }
		public List<Teamm> teams { get; set; }
		public List<Game> games { get; set; }
	}
}
