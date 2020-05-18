using FLTeam.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLTeam.Tests
{
	public class FakeDbContext : DbContext
	{
		public virtual DbSet<FantasyTournament> FantasyTournament { get; set; }
		public virtual DbSet<Icon> Icon { get; set; }
		public virtual DbSet<Player> Player { get; set; }
		public virtual DbSet<Team> Team { get; set; }
		public virtual DbSet<TeamsInTournament> TeamsInTournament { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<UsersFantasyTeam> UsersFantasyTeam { get; set; }
		public virtual DbSet<UsersFantasyTeamInTournament> UsersFantasyTeamInTournament { get; set; }
		public virtual DbSet<UsersIcon> UsersIcon { get; set; }
		public virtual DbSet<UsersInTournament> UsersInTournament { get; set; }

		public FakeDbContext(DbContextOptions<FakeDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FakeDbContext).Assembly);
		}
	}
}
