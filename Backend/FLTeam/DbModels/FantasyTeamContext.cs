using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FLTeam.DbModels
{
    public partial class FantasyTeamContext : DbContext
    {
        public FantasyTeamContext()
        {
        }

        public FantasyTeamContext(DbContextOptions<FantasyTeamContext> options)
            : base(options)
        {
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FantasyTeam;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FantasyTournament>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Icon>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Price).IsUnicode(false);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Player__536C85E42BEEB3A3")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Role).IsUnicode(false);

                entity.Property(e => e.SecondName).IsUnicode(false);

                entity.Property(e => e.TeamId).IsUnicode(false);

                entity.Property(e => e.Username).IsUnicode(false);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.HomeLeague).IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Slug).IsUnicode(false);
            });

            modelBuilder.Entity<TeamsInTournament>(entity =>
            {
                entity.Property(e => e.TeamId).IsUnicode(false);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamsInTournament)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamsInTo__TeamI__569ECEE8");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TeamsInTournament)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TeamsInTo__Tourn__5792F321");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__User__536C85E46EFEB2B9")
                    .IsUnique();

                entity.Property(e => e.Coins).HasDefaultValueSql("((100))");

                entity.Property(e => e.FirstName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.Icon)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.SecondName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.TeamName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('MyTeam')");

                entity.Property(e => e.Username).IsUnicode(false);
            });

            modelBuilder.Entity<UsersFantasyTeam>(entity =>
            {
                entity.Property(e => e.PlayerId).IsUnicode(false);

                entity.Property(e => e.Role).IsUnicode(false);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.UsersFantasyTeam)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersFant__Playe__4C214075");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersFantasyTeam)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersFant__UserI__4D1564AE");
            });

            modelBuilder.Entity<UsersFantasyTeamInTournament>(entity =>
            {
                entity.Property(e => e.PlayerId).IsUnicode(false);

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.UsersFantasyTeamInTournament)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersFant__Playe__5A6F5FCC");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.UsersFantasyTeamInTournament)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersFant__Tourn__5C57A83E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersFantasyTeamInTournament)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersFant__UserI__5B638405");
            });

            modelBuilder.Entity<UsersIcon>(entity =>
            {
                entity.HasOne(d => d.Icon)
                    .WithMany(p => p.UsersIcon)
                    .HasForeignKey(d => d.IconId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersIcon__IconI__4850AF91");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersIcon)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersIcon__UserI__4944D3CA");
            });

            modelBuilder.Entity<UsersInTournament>(entity =>
            {
                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.UsersInTournament)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersInTo__Tourn__52CE3E04");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersInTournament)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersInTo__UserI__53C2623D");
            });
        }
    }
}
