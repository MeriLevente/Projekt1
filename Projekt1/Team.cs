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
        public string name { get; set; }
        public int? point { get; set; }
        public int? games { get; set; }
        public string? goalsScored { get; set; }
        public string? goalsConceided { get; set; }
        public string? logoSource { get; set; }

        public Team(string id, string name)
        {
            this.id = id;
            this.name = name;
            this.point = 0;
            this.games = 0;
            this.goalsScored = null;
            this.goalsConceided = null;
            this.logoSource = null;
        }

        public Team(string id, string name, int? point, int? games, string? goalsScored, string? goalsConceided, string? logoSource) : this(id, name)
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
