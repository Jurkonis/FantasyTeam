using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class Player
    {
        public Player()
        {
            UsersFantasyTeam = new HashSet<UsersFantasyTeam>();
            UsersFantasyTeamInTournament = new HashSet<UsersFantasyTeamInTournament>();
        }

        [StringLength(255)]
        public string Id { get; set; }
        [Required]
        [StringLength(255)]
        public string TeamId { get; set; }
        [Required]
        [StringLength(255)]
        public string Role { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string SecondName { get; set; }
        [Required]
        [StringLength(255)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Image { get; set; }
        [Required]
        [StringLength(255)]
        public string Logo { get; set; }

        [InverseProperty("Player")]
        public ICollection<UsersFantasyTeam> UsersFantasyTeam { get; set; }
        [InverseProperty("Player")]
        public ICollection<UsersFantasyTeamInTournament> UsersFantasyTeamInTournament { get; set; }
    }
}
