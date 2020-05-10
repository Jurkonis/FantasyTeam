using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Stage
	{
		public string name { get; set; }
		public string type { get; set; }
		public string slug { get; set; }
		public List<Section> sections { get; set; }
	}
}
