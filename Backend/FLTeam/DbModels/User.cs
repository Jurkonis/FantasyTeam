using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class User
    {
        public User()
        {
            UsersFantasyTeam = new HashSet<UsersFantasyTeam>();
            UsersFantasyTeamInTournament = new HashSet<UsersFantasyTeamInTournament>();
            UsersIcon = new HashSet<UsersIcon>();
            UsersInTournament = new HashSet<UsersInTournament>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string SecondName { get; set; }
        [StringLength(255)]
        public string TeamName { get; set; }
        [StringLength(255)]
        public string Icon { get; set; }
        public int Coins { get; set; }
        public bool Admin { get; set; }
        [Column("TFA")]
        public bool Tfa { get; set; }

        [InverseProperty("User")]
        public ICollection<UsersFantasyTeam> UsersFantasyTeam { get; set; }
        [InverseProperty("User")]
        public ICollection<UsersFantasyTeamInTournament> UsersFantasyTeamInTournament { get; set; }
        [InverseProperty("User")]
        public ICollection<UsersIcon> UsersIcon { get; set; }
        [InverseProperty("User")]
        public ICollection<UsersInTournament> UsersInTournament { get; set; }
    }
}
