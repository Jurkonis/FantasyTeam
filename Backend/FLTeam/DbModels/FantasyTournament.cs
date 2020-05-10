using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class FantasyTournament
    {
        public FantasyTournament()
        {
            TeamsInTournament = new HashSet<TeamsInTournament>();
            UsersFantasyTeamInTournament = new HashSet<UsersFantasyTeamInTournament>();
            UsersInTournament = new HashSet<UsersInTournament>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndTime { get; set; }

        [InverseProperty("Tournament")]
        public ICollection<TeamsInTournament> TeamsInTournament { get; set; }
        [InverseProperty("Tournament")]
        public ICollection<UsersFantasyTeamInTournament> UsersFantasyTeamInTournament { get; set; }
        [InverseProperty("Tournament")]
        public ICollection<UsersInTournament> UsersInTournament { get; set; }
    }
}
