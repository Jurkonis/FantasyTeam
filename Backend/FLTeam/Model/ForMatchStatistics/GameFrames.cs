using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class GameFrames
	{
		public string rfc460Timestamp { get; set; }
		public string gameState { get; set; }
		public TeamGameStats blueTeam { get; set; }
		public TeamGameStats redTeam { get; set; }
	}
}
