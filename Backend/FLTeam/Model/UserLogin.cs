using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class UserLogin
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Icon { get; set; }
		public int Coins { get; set; }
		public string Token { get; set; }
		public bool TFA { get; set; }
		public bool Admin { get; set; }
	}
}
