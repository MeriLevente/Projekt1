using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekt1
{
    public class megold
    {
        public List<Team> teams { get; set; }
        public megold()
        {
            teams = new List<Team>();
            callAPI();
            sendAPI();
        }
        public async Task callAPI()
        {
            List<Team> ret = await API.getTeamsFromAPI();
            teams = ret;
        }
        public async Task sendAPI()
        {
            int count = teams.Count;
            string ret = await API.sendTeamChangeAPI("10;Szeged;20;XOXX");
            if (ret == "Failed")
            {
                MessageBox.Show("Hiba az API hívásban", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            callAPI();
        }

    }
}
