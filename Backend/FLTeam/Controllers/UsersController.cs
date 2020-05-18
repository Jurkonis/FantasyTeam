using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Authorization;
using FLTeam.Model;
using System.Security.Claims;
using Google.Authenticator;

namespace FLTeam.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly FantasyTeamContext _context;
		private IUserRepository _userRepository;

		public UsersController(FantasyTeamContext context, IUserRepository userRepository)
		{
			_context = context;
			_userRepository = userRepository;
		}

		[HttpPost("Authenticate")]
		public async Task<ActionResult> Authenticate(Login u)
		{

			if (u.Username.Length <= requiredLenght)
			{
				return BadRequest("Username too short");
			}

			if (u.Password.Length <= requiredLenght)
			{
				return BadRequest("Password too short");
			}

			var user = await _userRepository.Authenticate(u.Username, u.Password);


			if (user == null)
			{
				return NotFound("Wrong username or password!");
			}

			return Ok(user);
		}

		int requiredLenght = 4;

		[HttpGet("{userId}")]
		[Authorize]
		public async Task<ActionResult<User>> GetUser(int userId)
		{
			var data = await _context.User.FindAsync(userId);
			if (data == null)
			{
				return NotFound();
			}

			data.Password = null;
			data.Admin = false;
			return Ok(data);
		}

		[Authorize]
		[HttpPut("{userId}")]
		public async Task<ActionResult> PutUser(int userId, UserDetails details)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (details.FirstName == null || details.SecondName == null)
				return NotFound();

			var user = await _context.User.FindAsync(userId);

			user.FirstName = details.FirstName;
			user.SecondName = details.SecondName;

			_context.Entry(user).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return Ok(details);
		}


		[HttpPost("Register")]
		public async Task<ActionResult> PostUser(Login user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (user.Username.Length <= requiredLenght)
			{
				return BadRequest("Username too short");
			}

			if (user.Password.Length <= requiredLenght)
			{
				return BadRequest("Password too short");
			}

			var u = await _context.User.FirstOrDefaultAsync(x => x.Username == user.Username);

			if (u != null)
			{
				return BadRequest("Username already exist");
			}

			_context.User.Add(new User
				{Username = user.Username, Password = PasswordHandler.CreatePasswordHash(user.Password)});
			await _context.SaveChangesAsync();

			return Ok("Registration successfull");
		}

		[Authorize]
		[HttpPut("ChangePassword/{userId}")]
		public async Task<ActionResult> ChangePassword(int userId, UserPassword pass)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _context.User.FindAsync(userId);

			if (user.Password != pass.OldPassword)
			{
				return NotFound();
			}

			if (pass.NewPassword != pass.NewPassword2)
			{
				return NotFound();
			}

			user.Password = pass.NewPassword;

			_context.Entry(user).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		string key = "$#%5678#%_V_E_R_Y_L_O_N_G_%^&%45657^&%_F_A_N_T_A_S_Y__T_E_A_M_%$*^%@#235465";

		[Authorize]
		[HttpPut("DisableAuth/{userId}")]
		public async Task<ActionResult> DisableAuth(int userId, Auth auth)
		{
			User user = _context.User.FirstOrDefault(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
			string userUniqueKey = userId + key;
			bool isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, auth.Passcode);
			if (isValid)
			{
				user.Tfa = false;

				_context.Entry(user).State = EntityState.Modified;

				await _context.SaveChangesAsync();
			}

			return NoContent();
		}

		[Authorize]
		[HttpGet("EnableAuth/{userId}")]
		public async Task<ActionResult> EnableAuth(int userId)
		{
			User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
			string userUniqueKey = userId + key;
			var setupInfo = tfa.GenerateSetupCode("FantasyTeam", user.Username, userUniqueKey, false, 2);
			return Ok(setupInfo.QrCodeSetupImageUrl);
		}

		[Authorize]
		[HttpPut("EnableAuth/{userId}")]
		public async Task<ActionResult> EnableAuth(int userId, Auth auth)
		{
			User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
			string userUniqueKey = userId + key;
			bool isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, auth.Passcode);
			if (isValid)
			{
				user.Tfa = true;
				_context.Entry(user).State = EntityState.Modified;

				await _context.SaveChangesAsync();

				return NoContent();
			}

			return BadRequest();
		}

		[HttpPost("VerifyAuth/{userId}")]
		public async Task<ActionResult> VerifyAuth(int userId, Auth auth)
		{
			User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
			string userUniqueKey = userId + key;
			bool isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, auth.Passcode);
			if (isValid)
			{
				return NoContent();
			}

			return BadRequest();
		}

		[Authorize]
		[HttpPost("BuyIcon/{userId}/{iconId}")]
		public async Task<ActionResult> BuyIcon(int userId, int iconId)
		{
			User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			Icon icon = await _context.Icon.FirstOrDefaultAsync(x => x.Id == iconId);
			if (icon == null)
				return BadRequest();

			if (icon.Price > user.Coins)
				return BadRequest();

			user.Coins -= icon.Price;

			var icn = await _context.UsersIcon.FirstOrDefaultAsync(x => x.UserId == userId && x.IconId == iconId);

			if (icn != null)
				return NotFound("Icon already owned");

			UsersIcon usersIcon = new UsersIcon {UserId = user.Id, IconId = iconId};

			_context.Entry(user).State = EntityState.Modified;
			_context.UsersIcon.Add(usersIcon);
			await _context.SaveChangesAsync();

			return Ok(user.Coins);

		}

		[Authorize]
		[HttpGet("Icons/{userId}")]
		public IEnumerable<UsersIcon> GetIcons(int userId)
		{
			var data = _context.UsersIcon.Where(x => x.User.Id == userId);
			return data;
		}

		[Authorize]
		[HttpPut("Icons/{userId}/{name}")]
		public async Task<ActionResult> ChangeIcon(int userId, string name)
		{
			User user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				return BadRequest();

			Icon icon = await _context.Icon.FirstOrDefaultAsync(x => x.Name == name);
			if (icon == null)
				return BadRequest();

			user.Icon = name;

			_context.Entry(user).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}