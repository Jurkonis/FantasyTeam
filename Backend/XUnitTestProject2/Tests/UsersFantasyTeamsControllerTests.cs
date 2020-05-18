using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FLTeam.Tests
{
	public class UsersFantasyTeamsControllerTests : TestWithSqlite
	{
		[Fact]
		public async Task GetUsersTeamPlayer()
		{

			DbContext.Player.Add(new Player { Id = "22", Role = "testing", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersFantasyTeamsController(DbContext);
			var res = await contrl.GetUsersFantasyTeamPlayer("22");


			Assert.NotEmpty(res.Role);
		}

		[Fact]
		public async Task GetUsersTeamPlayers()
		{
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "12", Role = "top", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "32", Role = "jungle", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "42", Role = "bottom", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "52", Role = "support", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 1, PlayerId = "12", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 2, PlayerId = "22", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 3, PlayerId = "32", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 4, PlayerId = "42", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 5, PlayerId = "52", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.SaveChanges();

			var contrl = new UsersFantasyTeamsController(DbContext);
			var res = await contrl.GetUsersFantasyTeamPlayers(14);


			Assert.Equal(5,res.Count);
		}

		[Fact]
		public async Task AddPlayerInUsersTeam_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersFantasyTeamsController(DbContext);
			var res = await contrl.PostUsersFantasyTeam(14, new Player { Id = "22", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });


			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task AddPlayerInUsersTeam_ReturnOk2()
		{
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersFantasyTeamsController(DbContext);
			await contrl.PostUsersFantasyTeam(14, new Player { Id = "22", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			var res = await contrl.PostUsersFantasyTeam(14, new Player { Id = "22", Role = "top", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });


			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task DeleteUsersTeamPlayer_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "12", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 1, PlayerId = "12", Role = "testing", RolePriority = 1, UserId = 14 });

			DbContext.SaveChanges();

			var contrl = new UsersFantasyTeamsController(DbContext);

			var res = await contrl.DeleteUsersFantasyTeam(1);

			Assert.IsType<OkResult>(res);
		}

		[Fact]
		public async Task DeleteUsersTeamPlayer_ReturnNotFound()
		{
			var contrl = new UsersFantasyTeamsController(DbContext);

			var res = await contrl.DeleteUsersFantasyTeam(1);

			Assert.IsType<NotFoundResult>(res);
		}


	}
}
