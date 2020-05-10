using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class League
	{
		public string name { get; set; }
		public string slug { get; set; }
		public string id { get; set; }
		public string image { get; set; }
		public string region { get; set; }
		public int priority { get; set; }
	}
}
