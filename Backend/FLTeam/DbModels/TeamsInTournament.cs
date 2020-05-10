using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class TeamsInTournament
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string TeamId { get; set; }
        public int TournamentId { get; set; }

        [ForeignKey("TeamId")]
        [InverseProperty("TeamsInTournament")]
        public Team Team { get; set; }
        [ForeignKey("TournamentId")]
        [InverseProperty("TeamsInTournament")]
        public FantasyTournament Tournament { get; set; }
    }
}
