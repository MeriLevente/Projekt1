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
        public List<Team> newTeams { get; set; }
        private int matchCount;
        public megold()
        {
            teams = new List<Team>();
            newTeams = new List<Team>();
            callAPI();
            
        }
        public async Task callAPI()
        {
            List<Team> ret = await API.getTeamsFromAPI();
            teams = ret;
        }


        public void makeNewMatch(string home, string homeGoal, string away, string awayGoal)
        {
            if (matchCount == 0)
            {
                matchCount = teams.Count;
            }
            string id = matchCount.ToString();
            matchCount++;
            teams.Add(new Team(id, home, homeGoal, away, awayGoal));
            newTeams.Add(new Team(id, home, homeGoal, away, awayGoal));
        }

        public void runOnClose()
        {
            foreach (Team team in newTeams)
            {
                _ = sendAPI($"{team.id};{team.home};{team.homeGoal};{team.away};{team.awayGoal}");
            }
        }

        public async Task sendAPI(string line)
        {
            string ret = await API.sendTeamChangeAPI(line);
            if (ret == "Failed")
            {
                MessageBox.Show("Hiba az API hívásban", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
