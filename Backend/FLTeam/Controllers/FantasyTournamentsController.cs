using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FLTeam.DbModels;
using FLTeam.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FLTeam.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FantasyTournamentsController : ControllerBase
	{
		private readonly FantasyTeamContext _context;

		public FantasyTournamentsController(FantasyTeamContext context)
		{
			_context = context;
		}

		// GET: api/FantasyTournaments
		[HttpGet]
		public IEnumerable<FantasyTournament> GetFantasyTournament()
		{
			return _context.FantasyTournament;
		}

		[HttpGet("{id}")]
		public async Task<FantasyTournament> GetFantasyTournament(int id)
		{
			var data = await _context.FantasyTournament.FirstOrDefaultAsync(x => x.Id == id);
			return data;
		}

		[HttpGet("{id}/RegisteredUsers")]
		public List<UsersInTournament> GetRegisteredUsers(int id)
		{
			var data = _context.UsersInTournament.Where(x => x.TournamentId == id).OrderByDescending(x => x.Points).ToList();
			return data;
		}

		[HttpGet("{id}/Teams")]
		public IEnumerable<TeamsInTournament> GetFantasyTournamentTeams(int id)
		{
			var data = _context.TeamsInTournament.Where(x => x.TournamentId == id);
			return data;
		}

		[HttpGet("Team/{id}")]
		public async Task<Team> GetFantasyTournamentTeam(string id)
		{
			var data = await _context.Team.FindAsync(id);
			return data;
		}

		[Authorize]
		[HttpPost("Register/{userId}/{tournamentId}")]
		public async Task<ActionResult> RegisterInTournament(int userId, int tournamentId)
		{
			var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			var tournament = await _context.FantasyTournament.FirstOrDefaultAsync(x => x.Id == tournamentId);

			if (user != null && tournament != null)
			{

				UsersFantasyTeamsController fantasyTeam = new UsersFantasyTeamsController(_context);

				var playerTeam = fantasyTeam.GetUsersFantasyTeam(userId);

				if (playerTeam.Count() != 5)
				{
					return BadRequest("Users team has to be full");
				}

				var teams = this.GetFantasyTournamentTeams(tournamentId);

				foreach (var player in playerTeam)
				{
					bool contains = false;
					foreach (var team in teams)
					{
						var d = await fantasyTeam.GetUsersFantasyTeamPlayer(player.PlayerId);
						if (team.TeamId == d.TeamId)
						{
							contains = true;
						}
					}
					if (!contains)
					{
						return BadRequest("Your " + player.Role + " player do not compete in this tournament. Change it!");
					}
				}


				var hmm = await _context.UsersInTournament.FirstOrDefaultAsync(x => x.TournamentId == tournamentId && x.UserId == userId);

				if (hmm == null)
				{
					foreach (var player in playerTeam)
					{
						UsersFantasyTeamInTournament plaer = new UsersFantasyTeamInTournament();
						plaer.PlayerId = player.PlayerId;
						plaer.UserId = player.UserId;
						plaer.TournamentId = tournamentId;
						_context.UsersFantasyTeamInTournament.Add(plaer);
					}

					UsersInTournament data = new UsersInTournament();
					data.TournamentId = tournamentId;
					data.UserId = userId;

					_context.UsersInTournament.Add(data);
					await _context.SaveChangesAsync();
					return Ok("Users has been registered");
				}
				else
					return Ok("User is already registered");
			}
			return NotFound();
		}

		// PUT: api/FantasyTournaments/5
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<ActionResult> PutFantasyTournament(int id, FantasyTournament fantasyTournament)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != fantasyTournament.Id)
			{
				return BadRequest();
			}

			var teamss = _context.TeamsInTournament.Where(x => x.TournamentId == fantasyTournament.Id).ToList();

			foreach (var team in teamss)
			{
				_context.TeamsInTournament.Remove(team);
			}

			LeaguesController leagues = new LeaguesController();

			var leag = await leagues.GetLeaguesIds();

			foreach (var l in leag)
			{
				List<Teamm> teams = await leagues.ForTournamentGetTeams(l.id, fantasyTournament.StartTime, fantasyTournament.EndTime);

				if (teams.Count > 0)
					foreach (Teamm team in teams)
					{

						Team t = new Team();
						t.Id = team.id;
						t.Image = team.image;
						t.Slug = team.slug;
						t.Name = team.name;
						t.HomeLeague = team.homeLeague.name;
						if (!_context.Team.Contains(t))
							_context.Team.Add(t);

						TeamsInTournament data = new TeamsInTournament();
						data.TeamId = team.id;
						data.TournamentId = fantasyTournament.Id;
						_context.TeamsInTournament.Add(data);
					}
			}

			_context.Entry(fantasyTournament).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FantasyTournamentExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// PUT: api/FantasyTournaments/5
		[Authorize(Roles = "Admin")]
		[HttpPut("End/{id}")]
		public async Task<ActionResult> EndFantasyTournament(int id)
		{
			var tournament = await _context.FantasyTournament.FindAsync(id);

			if (tournament == null)
			{
				return BadRequest();
			}

			var tournamentUsers = _context.UsersInTournament.Where(x => x.TournamentId == id).ToList();

			List<string> teamIds = new List<string>();

			foreach (var user in tournamentUsers)
			{
				var team = _context.UsersFantasyTeamInTournament.Where(x => x.UserId == user.UserId && x.TournamentId == id).ToList();
				foreach (var pla in team)
				{
					var player = await _context.Player.FindAsync(pla.PlayerId);
					if (!teamIds.Contains(player.TeamId))
						teamIds.Add(player.TeamId);
				}
			}

			LeaguesController leagues = new LeaguesController();

			var leag = await leagues.GetLeaguesIds();

			List<MatchStats> matches = new List<MatchStats>();


			foreach (var l in leag)
			{
				List<Event> events = await leagues.ForTournamentGetEvents(l.id, tournament.StartTime, tournament.EndTime);

				if (events.Count > 0)
					foreach (var evnt in events)
					{
						if (evnt.match.teams[0].code != "TBD" && evnt.match.teams[1].code != "TBD")
						{
							var matchDetails = await leagues.GetMatchDetails(evnt.match.id);
							if (matchDetails != null)
								if (matchDetails.match.teams[0] != null && matchDetails.match.teams[1] != null)
								{
									if (teamIds.Contains(matchDetails.match.teams[0].id) || teamIds.Contains(matchDetails.match.teams[1].id))
										foreach (var match in matchDetails.match.games)
										{
											if (match.state == "completed")
											{
												var matchStats = await leagues.GetMatchStats(match.id);
												if (matchStats != null)
													matches.Add(matchStats);
											}

										}
								}
						}
					}
			}

			foreach (var user in tournamentUsers)
			{
				List<Participants> teamData = new List<Participants>();
				int sss = user.UserId;
				var usersTeam = _context.UsersFantasyTeamInTournament.Where(x => x.UserId == sss && x.TournamentId == id);
				foreach (var pla in usersTeam)
				{
					var player = await _context.Player.FindAsync(pla.PlayerId);
					foreach (var match in matches)
					{
						if (match.gameMetadata.blueTeamMetadata.esportsTeamId == player.TeamId)
						{
							foreach (var plData in match.gameMetadata.blueTeamMetadata.participantMetadata)
							{
								if (plData.esportsPlayerId == player.Id)
								{
									var data = match.frames[9].blueTeam.participants[plData.participantId - 1];
									teamData.Add(data);
								}
							}
						}
						else if (match.gameMetadata.redTeamMetadata.esportsTeamId == player.TeamId)
						{
							foreach (var plData in match.gameMetadata.blueTeamMetadata.participantMetadata)
							{
								if (plData.esportsPlayerId == player.Id)
								{
									var data = match.frames[9].redTeam.participants[plData.participantId - 1];
									teamData.Add(data);
								}
							}
						}
					}
				}

				int points = 0;

				foreach (var player in teamData)
				{
					points += (3 * player.totalGold / 1000);
					points += (10 * player.kills / 3);
					points += (2 * player.assists / 5);
					points += (5 * player.creepScore / 50);
					points += (4 * player.maxHealth / 500);
				}

				user.Points = points;
			}

			tournamentUsers = tournamentUsers.OrderByDescending(x => x.Points).ToList();

			for (int i = 0; i < tournamentUsers.Count; i++)
			{
				if (i >= 50)
				{
					break;
				}
				var user = _context.User.FirstOrDefault(x => x.Id == tournamentUsers[i].UserId);
				if (i == 0)
					user.Coins += 1000;
				if (i == 1)
					user.Coins += 500;
				if (i == 2)
					user.Coins += 400;
				if (3 <= i && i < 10)
					user.Coins += 200;
				if (10 <= i && i < 25)
					user.Coins += 150;
				if (25 <= i && i < 50)
					user.Coins += 100;

				_context.Entry(user).State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();

			return Ok("System calculated everything!!");
		}

		// POST: api/FantasyTournaments

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult> PostFantasyTournament(FantasyTournament fantasyTournament)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			LeaguesController leagues = new LeaguesController();

			var leag = await leagues.GetLeaguesIds();

			_context.FantasyTournament.Add(fantasyTournament);

			foreach (var l in leag)
			{
				List<Teamm> teams = await leagues.ForTournamentGetTeams(l.id, fantasyTournament.StartTime, fantasyTournament.EndTime);

				if (teams.Count > 0)
					foreach (Teamm team in teams)
					{

						Team t = new Team();
						t.Id = team.id;
						t.Image = team.image;
						t.Slug = team.slug;
						t.Name = team.name;
						t.HomeLeague = team.homeLeague.name;
						if (!_context.Team.Contains(t))
							_context.Team.Add(t);

						TeamsInTournament data = new TeamsInTournament();
						data.TeamId = team.id;
						data.TournamentId = fantasyTournament.Id;
						_context.TeamsInTournament.Add(data);
					}
			}

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}

			return Ok(fantasyTournament.Name + " has been created");
		}

		// DELETE: api/FantasyTournaments/5
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteFantasyTournament(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var fantasyTournament = await _context.FantasyTournament.FindAsync(id);
			if (fantasyTournament == null)
			{
				return NotFound();
			}

			var teams = _context.TeamsInTournament.Where(x => x.TournamentId == id);

			foreach (var team in teams)
			{
				_context.TeamsInTournament.Remove(team);
			}

			var users = _context.UsersInTournament.Where(x => x.TournamentId == id);

			foreach (var user in users)
			{
				_context.UsersInTournament.Remove(user);
			}

			var usersTeam = _context.UsersFantasyTeamInTournament.Where(x => x.TournamentId == id);

			foreach (var team in usersTeam)
			{
				_context.UsersFantasyTeamInTournament.Remove(team);
			}

			_context.FantasyTournament.Remove(fantasyTournament);
			await _context.SaveChangesAsync();

			return Ok("Tournament removed");
		}

		private bool FantasyTournamentExists(int id)
		{
			return _context.FantasyTournament.Any(e => e.Id == id);
		}
	}
}