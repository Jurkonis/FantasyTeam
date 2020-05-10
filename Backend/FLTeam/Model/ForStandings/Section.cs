using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Section
	{
		public string name { get; set; }
		public List<Match> matches { get; set; }
	}
}
