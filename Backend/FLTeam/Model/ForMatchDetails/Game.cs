using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Game
	{
		public int number { get; set; }
		public string id { get; set; }
		public string state { get; set; }
		public List<GameTeamDetails> teams { get; set; }
	}
}
