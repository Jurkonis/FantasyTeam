using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class UsersFantasyTeam
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Role { get; set; }
        public int RolePriority { get; set; }
        [Required]
        [StringLength(255)]
        public string PlayerId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("PlayerId")]
        [InverseProperty("UsersFantasyTeam")]
        public Player Player { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UsersFantasyTeam")]
        public User User { get; set; }
    }
}
