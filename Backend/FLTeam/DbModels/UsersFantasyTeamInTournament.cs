using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class UsersFantasyTeamInTournament
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string PlayerId { get; set; }
        public int UserId { get; set; }
        public int TournamentId { get; set; }

        [ForeignKey("PlayerId")]
        [InverseProperty("UsersFantasyTeamInTournament")]
        public Player Player { get; set; }
        [ForeignKey("TournamentId")]
        [InverseProperty("UsersFantasyTeamInTournament")]
        public FantasyTournament Tournament { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UsersFantasyTeamInTournament")]
        public User User { get; set; }
    }
}
