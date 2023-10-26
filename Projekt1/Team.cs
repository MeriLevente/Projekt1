using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    public class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public int point { get; set; }
        public int games { get; set; }
        public int goalsScored { get; set; }
        public int goalsConceided { get; set; }
        public string logoSource { get; set; }

        public Team(dynamic data)
        {
            this.id = data.id;
            this.name = data.name;
            this.point = 0;
            this.games = 0;
            this.goalsScored = 0;
            this.goalsConceided = 0;
            this.logoSource = data.url;
        }

        public Team(string id, string name, int point, int games, int goalsScored, int goalsConceided, string logoSource)
        {
            this.id = id;
            this.name = name;
            this.point = point;
            this.games = games;
            this.goalsScored = goalsScored;
            this.goalsConceided = goalsConceided;
            this.logoSource = logoSource;
        }
    }
}
