using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class UsersInTournament
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public int TournamentId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("TournamentId")]
        [InverseProperty("UsersInTournament")]
        public FantasyTournament Tournament { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UsersInTournament")]
        public User User { get; set; }
    }
}
