using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FLTeam.DbModels
{
    public partial class Icon
    {
        public Icon()
        {
            UsersIcon = new HashSet<UsersIcon>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        [InverseProperty("Icon")]
        public ICollection<UsersIcon> UsersIcon { get; set; }
    }
}
