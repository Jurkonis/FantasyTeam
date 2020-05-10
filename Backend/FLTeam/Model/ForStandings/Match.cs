using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Match
	{
		public string id { get; set; }
		public string state { get; set; }
		public List<string> previousMatchIds { get; set; }
		public List<Teamm> teams { get; set; }
	}
}
