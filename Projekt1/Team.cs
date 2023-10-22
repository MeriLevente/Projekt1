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
        public string point { get; set; }
        public string matches { get; set; }

        public Team(dynamic Line)
        {
            id = Line.id;
            name = Line.name;
            point = Line.point;
            matches = Line.matches;
        }
    }
}
