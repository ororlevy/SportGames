using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public String Name { get; set; }
        public String Country { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public Coach Coach { get; set; }
        public int CoachId { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<TeamLeague> TeamLeagues { get; set; }
        public String ImgURL { get; set; }

    }
}