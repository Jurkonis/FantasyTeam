using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FLTeam;
using FLTeam.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FLTeam.Tests
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaguesController : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<List<League>>> GetLeagues()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getLeagues?hl=en-US");
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<LeagueData>>(result);
				return Ok(data.data.leagues.OrderBy(x => x.priority));
			}
		}


		public async Task<List<League>> GetLeaguesIds()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getLeagues?hl=en-US");
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<LeagueData>>(result);
				return data.data.leagues.OrderBy(x => x.priority).ToList();
			}
		}

		[HttpGet("schedule/{id}/{upcoming}")]
		public async Task<ActionResult<List<Event>>> GetSchedule(string id, string upcoming)
		{
			List<Event> events;
			if (upcoming=="true")
				events = await GetUpcomingEvents(id);
			else
				events = await GetEvents(id);
			return Ok(events);

		}


		public async Task<List<Teamm>> ForTournamentGetTeams(string id, DateTime startTime,DateTime endTime)
		{
			List<Event> events = await ForTournamentGetEvents(id,startTime,endTime);

			var slugs = new List<string>();
			foreach (Event e in events)
			{
				foreach (TeamEvent t in e.match.teams)
				{
					var name = t.name.ToLower().Replace(' ', '-');
					if (!slugs.Contains(name) && name != "tbd")
						slugs.Add(name);
				}
			}

			List<Teamm> teams = new List<Teamm>();

			foreach (string slug in slugs)
			{
				var data = await this.GetTeamsList(slug);
				if (data != null)
					teams.Add(data);
			}

			return teams;
		}

		//testing to get teams list
		public async Task<Teamm> GetTeamsList(string id)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getTeams?hl=en-US&id=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Teams>>(result);

				if (data.data != null)
					return data.data.teams[0];
				return null;

			}
		}

		//Gets team data with players
		[HttpGet("team/{id}")]
		public async Task<ActionResult<Teamm>> GetTeam(string id)
		{
			id = id.ToLower().Replace(' ', '-');
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getTeams?hl=en-US&id=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Teams>>(result);
				if (data.data == null)
					return NotFound(null);
				return Ok(data.data.teams[0]);

			}
		}

		[HttpGet("standings/{id}")]
		public async Task<ActionResult<List<Standing>>> GetTournamentStanding(string id)
		{
			string idd = await GetRecentTournamentId(id);
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getStandings?hl=en-US&tournamentId=" + idd);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Standings>>(result);
				return Ok(data.data.standings);
			}
		}

		public async Task<string> GetRecentTournamentId(string id)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getTournamentsForLeague?hl=en-US&leagueId=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<TournamentsLeagues>>(result);
				var orderedTournaments = data.data.leagues[0].tournaments.OrderByDescending(x => x.startDate).ToList<Tournament>();
				return orderedTournaments[0].id;
			}
		}

		public async Task<List<Event>> GetEvents(string id)
		{
			List<Event> events = new List<Event>();
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
				
				events.AddRange(data.data.schedule.events.Where(x => x.startTime < DateTime.Now));

				while (data.data.schedule.pages.newer != null)
				{
					response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id + "&pageToken=" + data.data.schedule.pages.newer);
					result = await response.Content.ReadAsStringAsync();
					data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
					
					events.AddRange(data.data.schedule.events.Where(x => x.startTime < DateTime.Now));
				}
			}
			
			events = Enumerable.Reverse(events).Take(15).ToList();

			return events;
		}

		public async Task<List<Event>> GetUpcomingEvents(string id)
		{
			List<Event> events = new List<Event>();
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
				events.AddRange(data.data.schedule.events.Where(x => x.startTime > DateTime.Now));


				while (data.data.schedule.pages.newer != null)
				{
					response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id + "&pageToken=" + data.data.schedule.pages.newer);
					result = await response.Content.ReadAsStringAsync();
					data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
					events.AddRange(data.data.schedule.events.Where(x => x.startTime > DateTime.Now));

				}
			}
			

			return events;
		}

		public async Task<List<Event>> ForTournamentGetEvents(string id, DateTime startTime, DateTime endTime)
		{
			List<Event> events = new List<Event>();
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
				events.AddRange(data.data.schedule.events.Where(x => x.startTime >startTime.Date && x.startTime< endTime.Date));


				while (data.data.schedule.pages.newer != null)
				{
					response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getSchedule?hl=en-US&leagueId=" + id + "&pageToken=" + data.data.schedule.pages.newer);
					result = await response.Content.ReadAsStringAsync();
					data = JsonConvert.DeserializeObject<Data<Schedu>>(result);
					events.AddRange(data.data.schedule.events.Where(x => x.startTime > startTime.Date && x.startTime < endTime.Date));

				}
			}

			return events;
		}

		[HttpGet("MatchDetails/{id}")]
		public async Task<MatchDetailsEvent> GetMatchDetails(string id)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://esports-api.lolesports.com/persisted/gw/getEventDetails?hl=es-ES&id=" + id);
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Data<EventDetails>>(result);

				return data.data.Event;

			}
		}

		[HttpGet("MatchStats/{id}")]
		public async Task<MatchStats> GetMatchStats(string id)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("x-api-key", Data.Key);
				var response = await client.GetAsync("https://feed.lolesports.com/livestats/v1/window/" + id + "?startingTime=" + DateTime.Now.ToString("yyyy-MM-ddT00:00:00.0Z"));
				var result = await response.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<MatchStats>(result);

				return data;

			}
		}
	}
}