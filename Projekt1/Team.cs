using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt1
{
    class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public string point { get; set; }
        public string games { get; set; }

        public Team(string Line)
        {
            Line.Split();
        }
    }
}
