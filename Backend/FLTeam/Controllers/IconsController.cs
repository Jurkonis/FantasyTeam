using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace FLTeam.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class IconsController : ControllerBase
	{
		private readonly FantasyTeamContext _context;

		public IconsController(FantasyTeamContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IEnumerable<Icon> GetIcon()
		{
			return _context.Icon;
		}

		[HttpGet("{id}")]
		public async Task<Icon> GetIcon(int id)
		{
			var data = await _context.Icon.FindAsync(id);
			return data;
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> PostIcon( Icon icon)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Entry(icon).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}