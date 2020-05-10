using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class TeamEvent
	{
		public string name { get; set; }
		public string code { get; set; }
		public string image { get; set; }
		public Result result { get; set; }
		public Record record { get; set; }
	}
}
