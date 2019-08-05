using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public string NameOfLeague { get; set; }
        public string Country { get; set; }
        public ICollection<TeamLeague> TeamLeagues { get; set; }
    }
}