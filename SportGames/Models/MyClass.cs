using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGames.Models
{
    public class MyClass
    {
        public class Odds
        {
            public double? X2 { get; set; }
            public double _invalid_name_2 { get; set; }
            public double? _invalid_name_1X { get; set; }
            public double? _invalid_name_12 { get; set; }
            public double _invalid_name_1 { get; set; }
            public double X { get; set; }
        }

        public class Datum
        {
            public string market { get; set; }
            public string season { get; set; }
            public string home_team { get; set; }
            public Odds odds { get; set; }
            public string competition_name { get; set; }
            public bool is_expired { get; set; }
            public string federation { get; set; }
            public string status { get; set; }
            public string away_team { get; set; }
            public DateTime start_date { get; set; }
            public int id { get; set; }
            public string prediction { get; set; }
            public string result { get; set; }
            public DateTime last_update_at { get; set; }
            public string competition_cluster { get; set; }
        }

        public class RootObject
        {
            public List<Datum> data { get; set; }
        }
        
    }
    
}