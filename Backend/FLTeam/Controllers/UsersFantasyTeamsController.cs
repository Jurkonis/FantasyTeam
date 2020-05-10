using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FLTeam.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersFantasyTeamsController : ControllerBase
	{
		private readonly FantasyTeamContext _context;

		public UsersFantasyTeamsController(FantasyTeamContext context)
		{
			_context = context;
		}

		// GET: api/UsersFantasyTeams/5
		[HttpGet("{userId}")]
		public IEnumerable<UsersFantasyTeam> GetUsersFantasyTeam(int userId)
		{
			var data = _context.UsersFantasyTeam.Where(x => x.UserId == userId).OrderBy(x => x.RolePriority);
			return data;
		}

		// GET: api/UsersFantasyTeams/5
		[HttpGet("Player/{playerId}")]
		public async Task<Player> GetUsersFantasyTeamPlayer(string playerId)
		{
			var data = await _context.Player.FirstOrDefaultAsync(x => x.Id == playerId);
			return data;
		}

		// GET: api/UsersFantasyTeams/5
		[HttpGet("Players/{userId}")]
		public async Task<List<Player>> GetUsersFantasyTeamPlayers(int userId)
		{
			List<Player> players = new List<Player>();
			var team = GetUsersFantasyTeam(userId);

			foreach (UsersFantasyTeam p in team)
			{
				Player player = await this.GetUsersFantasyTeamPlayer(p.PlayerId);
				player.UsersFantasyTeam = null;
				players.Add(player);
			}

			return players;
		}

		// POST: api/UsersFantasyTeams
		[HttpPost("{userId}")]
		public async Task<ActionResult> PostUsersFantasyTeam(int userId, Player player)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var identity = (ClaimsIdentity)User.Identity;
			int idd = Int16.Parse(identity.Name);

			if (idd != userId)
			{
				return NotFound("Don't be sneaky!");
			}

			var user = await _context.User.FindAsync(userId);

			if (user == null)
				return NotFound("User does not exist");

			string playerId = null;

			// searches for player in player table
			var data = await _context.Player.FirstOrDefaultAsync(x => x.Username == player.Username);


			if (data == null)
			{
				_context.Player.Add(player);
				playerId = player.Id;

			}
			else
			{
				playerId = data.Id;
			}


			//searches for player in user fantasy team
			var d = await _context.UsersFantasyTeam.FirstOrDefaultAsync(x => x.PlayerId == playerId && x.UserId == userId);

			if (d != null)
			{
				if (d.PlayerId == playerId)
					return Ok("Player already exist");
			}

			var players = GetUsersFantasyTeam(userId);

			foreach (UsersFantasyTeam p in players)
			{
				if (p.Role == player.Role)
				{
					_context.UsersFantasyTeam.Remove(p);
				}
			}

			UsersFantasyTeam usersFantasyTeam = new UsersFantasyTeam();

			usersFantasyTeam.PlayerId = playerId;
			usersFantasyTeam.UserId = user.Id;
			usersFantasyTeam.Role = player.Role;

			if (player.Role == "top")
			{
				usersFantasyTeam.RolePriority = 1;
			}

			if (player.Role == "jungle")
			{
				usersFantasyTeam.RolePriority = 2;
			}

			if (player.Role == "mid")
			{
				usersFantasyTeam.RolePriority = 3;
			}

			if (player.Role == "bottom")
			{
				usersFantasyTeam.RolePriority = 4;
			}

			if (player.Role == "support")
			{
				usersFantasyTeam.RolePriority = 5;
			}

			_context.UsersFantasyTeam.Add(usersFantasyTeam);
			await _context.SaveChangesAsync();

			return Ok("Added player to the team");
		}

		// DELETE: api/UsersFantasyTeams/5
		[HttpDelete("{userId}")]
		public async Task<ActionResult> DeleteUsersFantasyTeam(int userId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var usersFantasyTeam = await _context.UsersFantasyTeam.FindAsync(userId);
			if (usersFantasyTeam == null)
			{
				return NotFound();
			}

			var identity = (ClaimsIdentity)User.Identity;
			int idd = Int16.Parse(identity.Name);

			if (idd != usersFantasyTeam.UserId)
			{
				return NotFound("Don't be sneaky!");
			}

			_context.UsersFantasyTeam.Remove(usersFantasyTeam);
			await _context.SaveChangesAsync();

			return Ok(usersFantasyTeam);
		}

		private bool UsersFantasyTeamExists(int id)
		{
			return _context.UsersFantasyTeam.Any(e => e.Id == id);
		}
	}
}