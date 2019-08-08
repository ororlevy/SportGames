using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class Coach
    {
        
        public int CoachId { get; set; }
        public String Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public String ImgURL { get; set; }
        
    }
}