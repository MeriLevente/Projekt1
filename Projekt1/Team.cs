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


        public Team(dynamic Line)
        {
            id = Line.id;
            name = Line.name;

        }

        public Team(string id,string name)
        {
            this.id = id;
            this.name = name;

        }
    }
}
