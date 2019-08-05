using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class TeamLeague
    {
        [Key]
        [Column(Order=0)]
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("League")]
        public int LeagueId { get; set; }
        public League League { get; set; }
        public Team Team { get; set; }

    }
}