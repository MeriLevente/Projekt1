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
        public List<Match> matches { get; set; }
        public List<Match> newMatches { get; set; }
        public List<Team> teams { get; set; }

        private int matchCount;
        public megold()
        {
            matches = new List<Match>();
            newMatches = new List<Match>();
            teams = new List<Team>();

            callAPI();
            
        }
        public async Task callAPI()
        {
            List<Match> ret = await API.getTeamsFromAPI();
            matches = ret;
        }

        public void  getTeames()
        {
            foreach (var team in teams)
            {
                List<Match> filtered = (List<Match>)matches.Where(x => x.home == team.name);
                team.games += filtered.Count;
                foreach (var match in filtered)
                {
                    
                    team.point += (int.Parse(match.homeGoal) > int.Parse(match.homeGoal) ? 3 : (int.Parse(match.homeGoal) == int.Parse(match.homeGoal)) ? 1 : 0);
                    
                }
            }
        }

        public void makeNewMatch(string home, string homeGoal, string away, string awayGoal)
        {
            if (matchCount == 0)
            {
                matchCount = matches.Count;
            }
            string id = matchCount.ToString();
            matchCount++;
            matches.Add(new Match(id, home, homeGoal, away, awayGoal));
            newMatches.Add(new Match(id, home, homeGoal, away, awayGoal));
        }

        public void runOnClose()
        {
            foreach (Match team in newMatches)
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
