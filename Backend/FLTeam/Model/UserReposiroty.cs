using FLTeam.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public class UserReposiroty : IUserRepository
	{
		private readonly FantasyTeamContext _context;
		private readonly AppSettings _appsettings;

		public UserReposiroty(IOptions<AppSettings> appSettings, FantasyTeamContext context)
		{
			_context = context;
			_appsettings = appSettings.Value;
		}

		public async Task<UserLogin> Authenticate(string username, string password)
		{
			var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);

			if (user == null)
				return null;

			var identity = new ClaimsIdentity();
			identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));

			if (user.Admin)
				identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = identity,
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new UserLogin { Id = user.Id, Username = user.Username,  Coins = user.Coins, Token = tokenHandler.WriteToken(token), Icon = user.Icon, TFA = user.Tfa, Admin = user.Admin };
		}
	}
}