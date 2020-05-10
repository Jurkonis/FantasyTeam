using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class Participants
	{
		public int praticipantId { get; set; }
		public int totalGold { get; set; }
		public int level { get; set; }
		public int kills { get; set; }
		public int deaths { get; set; }
		public int assists { get; set; }
		public int creepScore { get; set; }
		public int currentHealth { get; set; }
		public int maxHealth { get; set; }
	}
}
