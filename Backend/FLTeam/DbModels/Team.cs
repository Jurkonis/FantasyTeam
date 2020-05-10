using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class Team
    {
        public Team()
        {
            TeamsInTournament = new HashSet<TeamsInTournament>();
        }

        [StringLength(255)]
        public string Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Image { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Slug { get; set; }
        [Required]
        [StringLength(255)]
        public string HomeLeague { get; set; }

        [InverseProperty("Team")]
        public ICollection<TeamsInTournament> TeamsInTournament { get; set; }
    }
}
