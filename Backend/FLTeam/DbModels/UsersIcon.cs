using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class UsersIcon
    {
        public int Id { get; set; }
        public int IconId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("IconId")]
        [InverseProperty("UsersIcon")]
        public Icon Icon { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("UsersIcon")]
        public User User { get; set; }
    }
}
