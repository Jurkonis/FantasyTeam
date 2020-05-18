using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace FLTeam.Tests
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class IconsController : ControllerBase
	{
		private readonly FakeDbContext _context;

		public IconsController(FakeDbContext context)
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
		public async Task<ActionResult> PostIcon( Icon icon)
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