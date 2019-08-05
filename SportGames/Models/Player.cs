using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Number { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}