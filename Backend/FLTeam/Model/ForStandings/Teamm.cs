using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Teamm
	{
		public string id { get; set; }
		public string slug { get; set; }
		public string name { get; set; }
		public string code { get; set; }
		public string image { get; set; }
		public string alternativeImage { get; set; }
		public Result result { get; set; }
		public HomeLeague homeLeague { get; set; }
		public List<Player> players { get; set; }
	}
}
