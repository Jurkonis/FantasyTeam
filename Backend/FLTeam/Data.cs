using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam
{
	public static class Data
	{
		private static string _token = "0TvQnueqKa5mxJntVWt0w4LpLfEkrV1Ta8rQBb9Z";

		public static string Key { get { return _token; } }

		public static string URL(string region)
		{
			return "https://" + region + ".api.riotgames.com/lol/";
		}
	}
}
