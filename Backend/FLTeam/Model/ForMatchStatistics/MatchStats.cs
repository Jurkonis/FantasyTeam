using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class MatchStats
	{
		public string esportsGameId { get; set; }
		public string esportsMatchId { get; set; }
		public GameData gameMetadata { get; set; }
		public List<GameFrames> frames { get; set; }
	}
}
