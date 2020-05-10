using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Tournament
	{
		public string id { get; set; }
		public string slug { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
	}
}
