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
	public class IconsControllerTests : TestWithSqlite
	{

		[Fact]
		public async Task GetIcon()
		{

			var icon = new Icon { Id = 12, Name = "ha", Price = 25, UsersIcon = null };
			DbContext.Icon.Add(icon);
			DbContext.SaveChanges();

			var controller = new IconsController(DbContext);
			var gg = await controller.GetIcon(12);

			Assert.NotEmpty(gg.Name);
		}

		[Fact]
		public async Task GetIcons()
		{

			var icon = new Icon { Id = 12, Name = "ha", Price = 25, UsersIcon = null };
			var icon2 = new Icon { Id = 122, Name = "ha", Price = 25, UsersIcon = null };
			DbContext.Icon.Add(icon);
			DbContext.Icon.Add(icon2);
			DbContext.SaveChanges();

			var controller = new IconsController(DbContext);
			var gg =  controller.GetIcon();

			Assert.Equal(2,gg.Count());
		}

		[Fact]
		public async Task AddIcon()
		{

			var icon = new Icon { Id = 12, Name = "ha", Price = 25, UsersIcon = null };

			var controller = new IconsController(DbContext);
			var gg = controller.PostIcon(icon).Result;

			Assert.IsType<NoContentResult>(gg);
		}


	}
}
