using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class GameData
	{
		public string patchVersion { get; set; }
		public TeamData blueTeamMetadata { get; set; }
		public TeamData redTeamMetadata { get; set; }
	}
}
