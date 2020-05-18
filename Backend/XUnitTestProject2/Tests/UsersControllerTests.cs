using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FLTeam.DbModels;
using FLTeam.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FLTeam;

namespace FLTeam.Tests
{
	public class UsersControllerTests : TestWithSqlite
	{
		private IUserRepository _userRepository ;
		[Fact]
		public async Task GetUser_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);
			var res = await contrl.GetUser(12);

			Assert.IsType<OkObjectResult>(res.Result);
		}

		[Fact]
		public async Task PostUser_ReturnOk()
		{


			var contrl = new UsersController(DbContext, _userRepository);
			var res = await contrl.PostUser(new Login{ Username = "testing",Password = "testing"});

			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task PostUser_ReturnBadRequest()
		{


			var contrl = new UsersController(DbContext, _userRepository);
			var res = await contrl.PostUser(new Login { Username = "te", Password = "testing" });

			Assert.IsType<BadRequestObjectResult>(res);
		}

		[Fact]
		public async Task PostUser_ReturnBadRequest2()
		{
			var contrl = new UsersController(DbContext, _userRepository);
			var res = await contrl.PostUser(new Login { Username = "testing", Password = "test" });
			Assert.IsType<BadRequestObjectResult>(res);
		}

		[Fact]
		public async Task PostUser_ReturnBadRequest3()
		{
			var contrl = new UsersController(DbContext, _userRepository);
			await contrl.PostUser(new Login { Username = "testing", Password = "testtttt" });
			var res = await contrl.PostUser(new Login { Username = "testing", Password = "testtttt" });
			Assert.IsType<BadRequestObjectResult>(res);
		}

		[Fact]
		public async Task PutUser_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);
			
			var res = await contrl.PutUser(12,new UserDetails { FirstName = "testing", SecondName = "testtttt" });
			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task PutUser_ReturnBadRequest()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.PutUser(12, new UserDetails {  SecondName = "testtttt" });
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task UserChangePassword_ReturnBadRequest()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangePassword(12, new UserPassword { NewPassword = "testtttt" ,NewPassword2 = "tessst",OldPassword = "test"});
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task UserChangePassword_ReturnBadRequest2()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangePassword(12, new UserPassword { NewPassword = "testtttt", NewPassword2 = "tessst", OldPassword = "testing" });
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task UserChangePassword_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangePassword(12, new UserPassword { NewPassword = "testtttt", NewPassword2 = "testtttt", OldPassword = "testing" });
			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public async Task BuyIcon_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing" ,Coins = 5000});
			DbContext.Icon.Add(new Icon { Price = 100,Id = 1,Name = "testing"});
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.BuyIcon(12,1);
			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public async Task BuyIcon_ReturnBadRequest()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10 });
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.BuyIcon(12, 1);
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task BuyIcon_ReturnBadRequest2()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10 });
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.BuyIcon(13, 1);
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task BuyIcon_ReturnBadRequest3()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10 });
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.BuyIcon(12, 12);
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task UserChangeIcon_ReturnOk()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10,Icon = "new"});
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangeIcon(12, "testing");
			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public async Task UserChangeIcon_ReturnBadRequest()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10, Icon = "new" });
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangeIcon(12, "great");
			Assert.IsType<BadRequestResult>(res);
		}

		[Fact]
		public async Task UserChangeIcon_ReturnBadRequest2()
		{
			DbContext.User.Add(new User { Id = 12, Username = "testing", Password = "testing", Coins = 10, Icon = "new" });
			DbContext.Icon.Add(new Icon { Price = 100, Id = 1, Name = "testing" });
			DbContext.SaveChanges();

			var contrl = new UsersController(DbContext, _userRepository);

			var res = await contrl.ChangeIcon(15, "testing");
			Assert.IsType<BadRequestResult>(res);
		}
	}
}
