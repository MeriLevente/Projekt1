using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    public class Team
    {
        public string id { get; set; }
        public string home { get; set; }
        public string homeGoal { get; set; }
        public string away { get; set; }
        public string awayGoal { get; set; }

        public Team(dynamic Line)
        {
            id = Line.id;
            home = Line.home;
            homeGoal =Line.homeGoal;
            away = Line.away;
            awayGoal = Line.awayGoal;
        }

        public Team(string id,string home, string homeGoal, string away, string awayGoal)
        {
            this.id = id;
            this.home = home;
            this.homeGoal = homeGoal;
            this.away = away;
            this.awayGoal = awayGoal;
        }
    }
}
