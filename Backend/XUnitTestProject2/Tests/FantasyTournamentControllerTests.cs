using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLTeam.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FLTeam.Tests
{
	public class FantasyTournamentControllerTests : TestWithSqlite
	{
		[Fact]
		public async Task GetTournament()
		{
			var tournament = new FantasyTournament { Id = 12, Name = "tournament" };
			DbContext.FantasyTournament.Add(tournament);
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.GetFantasyTournament(12);

			
			Assert.NotEmpty(res.Name);
		}

		[Fact]
		public async Task GetTournament_ReturnNull()
		{
			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.GetFantasyTournament(12);

			Assert.Null(res);
		}

		[Fact]
		public async Task GetTournaments()
		{

			var tournament = new FantasyTournament { Id = 12, Name = "tournament" };
			DbContext.FantasyTournament.Add(tournament);
			var tournament2 = new FantasyTournament { Id = 122, Name = "tournament2" };
			DbContext.FantasyTournament.Add(tournament2);
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res =  contrl.GetFantasyTournament();


			Assert.Equal(2,res.Count());
		}


		[Fact]
		public async Task deleteTournament()
		{

			var tournament = new FantasyTournament { Id = 12, Name = "tournament" };
			DbContext.FantasyTournament.Add(tournament);
			DbContext.User.Add(new User { Id = 14,Username = "testing",Password = "testing"});
			DbContext.Team.Add(new Team { Id = "55",HomeLeague = "testing",Image = "testing" ,Name = "testing" ,Slug = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "testing", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.TeamsInTournament.Add(new TeamsInTournament{ Id = 12,TeamId = "55",TournamentId = 12});
			DbContext.UsersInTournament.Add(new UsersInTournament { Id = 12, UserId = 14, TournamentId = 12 });
			DbContext.UsersFantasyTeamInTournament.Add(new UsersFantasyTeamInTournament() { Id = 12, UserId = 14, TournamentId = 12,PlayerId = "22"});
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res =await contrl.DeleteFantasyTournament(12) ;


			Assert.IsType<OkObjectResult>( res );
		}

		[Fact]
		public async Task deleteTournament_returnNotFound()
		{

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.DeleteFantasyTournament(12);


			Assert.IsType<NotFoundResult>(res);
		}

		[Fact]
		public async Task addTournament_andTeamsNull()
		{

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.PostFantasyTournament(new FantasyTournament { Id = 12, Name = "tournament" });

			var res2 = contrl.GetFantasyTournamentTeams(12);

			Assert.IsType<OkObjectResult>(res);
			Assert.Equal(0,res2.Count());
		}

		[Fact]
		public async Task PutTournament_BadRequest()
		{

			var contrl = new FantasyTournamentsController(DbContext);

			var res2 = await contrl.PutFantasyTournament(12, new FantasyTournament { Id = 14, Name = "tournament22" });

			Assert.IsType<BadRequestResult>(res2);
		}

		[Fact]
		public async Task PutTournament_BadRequest2()
		{

			var contrl = new FantasyTournamentsController(DbContext);

			var res2 = await contrl.PutFantasyTournament(12, new FantasyTournament { Id = 14 });

			Assert.IsType<BadRequestResult>(res2);
		}

		[Fact]
		public async Task addTournament_andTeamsNotNull()
		{

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.PostFantasyTournament(new FantasyTournament { Id = 12, Name = "tournament" ,StartTime = DateTime.Now,EndTime = DateTime.Today.AddDays(7)});

			var res2 = contrl.GetFantasyTournamentTeams(12);

			Assert.IsType<OkObjectResult>(res);
			Assert.NotEqual(0, res2.Count());
		}

		[Fact]
		public async Task GetTournamentTeams()
		{

			DbContext.Team.Add(new Team { Id = "55", HomeLeague = "testing", Image = "testing", Name = "testing", Slug = "testing" });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.GetFantasyTournamentTeam("55");


			Assert.NotNull(res);
		}

		[Fact]
		public void GetUsersInTournament()
		{
			
			DbContext.FantasyTournament.Add(new FantasyTournament { Id = 12, Name = "tournament" });
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.UsersInTournament.Add(new UsersInTournament { Id = 12, UserId = 14, TournamentId = 12 });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = contrl.GetRegisteredUsers(12);


			Assert.Equal(1,res.Count);
		}

		[Fact]
		public async Task RegisterUserInTournament_BadRequest()
		{
			DbContext.FantasyTournament.Add(new FantasyTournament { Id = 12, Name = "tournament" });
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeamInTournament.Add(new UsersFantasyTeamInTournament() { Id = 12, UserId = 14, TournamentId = 12, PlayerId = "22" });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.RegisterInTournament(14,12);


			Assert.IsType<BadRequestObjectResult>(res);
		}

		[Fact]
		public async Task RegisterUserInTournament_BadRequest2()
		{
			DbContext.FantasyTournament.Add(new FantasyTournament { Id = 12, Name = "tournament" });
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "mid", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "12", Role = "top", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "32", Role = "jungle", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "42", Role = "bottom", TeamId = "testing", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "52", Role = "support", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 1,PlayerId = "12",Role="testing",RolePriority = 1,UserId = 14});
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 2, PlayerId = "22", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id =3, PlayerId = "32", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 4, PlayerId = "42", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 5, PlayerId = "52", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeamInTournament.Add(new UsersFantasyTeamInTournament() { Id = 12, UserId = 14, TournamentId = 12, PlayerId = "22" });
			DbContext.Team.Add(new Team { Id = "55", HomeLeague = "testing", Image = "testing", Name = "testing", Slug = "testing" });
			DbContext.TeamsInTournament.Add(new TeamsInTournament { Id = 12, TeamId = "55", TournamentId = 12 });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.RegisterInTournament(14, 12);


			Assert.IsType<BadRequestObjectResult>(res);
		}

		[Fact]
		public async Task RegisterUserInTournament_Ok()
		{
			DbContext.FantasyTournament.Add(new FantasyTournament { Id = 12, Name = "tournament" });
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "mid", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "12", Role = "top", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "32", Role = "jungle", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "42", Role = "bottom", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "52", Role = "support", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeamInTournament.Add(new UsersFantasyTeamInTournament() { Id = 12, UserId = 14, TournamentId = 12, PlayerId = "22" });
			DbContext.Team.Add(new Team { Id = "55", HomeLeague = "testing", Image = "testing", Name = "testing", Slug = "testing" });
			DbContext.TeamsInTournament.Add(new TeamsInTournament { Id = 12, TeamId = "55", TournamentId = 12 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 1, PlayerId = "12", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 2, PlayerId = "22", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 3, PlayerId = "32", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 4, PlayerId = "42", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 5, PlayerId = "52", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.RegisterInTournament(14, 12);


			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task RegisterUserInTournament_Ok2()
		{
			DbContext.FantasyTournament.Add(new FantasyTournament { Id = 12, Name = "tournament" });
			DbContext.User.Add(new User { Id = 14, Username = "testing", Password = "testing" });
			DbContext.Player.Add(new Player { Id = "22", Role = "mid", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "12", Role = "top", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "32", Role = "jungle", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "42", Role = "bottom", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.Player.Add(new Player { Id = "52", Role = "support", TeamId = "55", FirstName = "testing", SecondName = "testing", Image = "testing", Logo = "testing", Username = "testing" });
			DbContext.UsersFantasyTeamInTournament.Add(new UsersFantasyTeamInTournament() { Id = 12, UserId = 14, TournamentId = 12, PlayerId = "22" });
			DbContext.Team.Add(new Team { Id = "55", HomeLeague = "testing", Image = "testing", Name = "testing", Slug = "testing" });
			DbContext.TeamsInTournament.Add(new TeamsInTournament { Id = 12, TeamId = "55", TournamentId = 12 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 1, PlayerId = "12", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 2, PlayerId = "22", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 3, PlayerId = "32", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 4, PlayerId = "42", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.UsersFantasyTeam.Add(new UsersFantasyTeam { Id = 5, PlayerId = "52", Role = "testing", RolePriority = 1, UserId = 14 });
			DbContext.SaveChanges();

			var contrl = new FantasyTournamentsController(DbContext);
			var res = await contrl.RegisterInTournament(14, 12);
			var res2 = await contrl.RegisterInTournament(14, 12);

			Assert.IsType<OkObjectResult>(res);
		}
	}
}
