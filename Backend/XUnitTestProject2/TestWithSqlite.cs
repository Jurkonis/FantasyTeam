using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FLTeam.DbModels;

namespace FLTeam.Tests
{
	public abstract class TestWithSqlite : IDisposable
	{
		private const string InMemoryConnectionString = "DataSource=:memory:";
		private readonly SqliteConnection _connection;

		protected readonly FakeDbContext DbContext;

		protected TestWithSqlite()
		{
			_connection = new SqliteConnection(InMemoryConnectionString);
			_connection.Open();
			var options = new DbContextOptionsBuilder<FakeDbContext>()
				.UseSqlite(_connection)
				.Options;
			DbContext = new FakeDbContext(options);
			DbContext.Database.EnsureCreated();
		}

		public void Dispose()
		{
			_connection.Close();
		}
	}
}
