using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

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

            callMatchAPI();
            callTeamAPI();


        }
        public async Task callMatchAPI()
        { 
            matches = await API.getMatchesFromAPI();

        }

        public async Task callTeamAPI()
        {
            teams = await API.getTeamsFromAPI();
            getTeamesData();
        }

        public void getTeamesData()
        {
            foreach (var team in teams)
            {
                // If the team is at home
                List<Match> filteredHome = matches.Where(x => x.home == team.name).ToList();
                team.games += filteredHome.Count;
                foreach (var match in filteredHome)
                {
                    team.point += (int.Parse(match.homeGoal) > int.Parse(match.awayGoal)) ? 3 : (int.Parse(match.homeGoal) == int.Parse(match.awayGoal)) ? 1 : 0;
                    team.goalsScored += int.Parse(match.homeGoal);
                    team.goalsConceided += int.Parse(match.awayGoal);
                }

                // If the team is away
                List<Match> filteredAway = matches.Where(x => x.away == team.name).ToList();
                team.games += filteredAway.Count;
                foreach (var match in filteredAway)
                {
                    team.point += (int.Parse(match.awayGoal) > int.Parse(match.homeGoal)) ? 3 : (int.Parse(match.homeGoal) == int.Parse(match.awayGoal)) ? 1 : 0;
                    team.goalsScored += int.Parse(match.awayGoal);
                    team.goalsConceided += int.Parse(match.homeGoal);
                }
            }
            saa();

        }

        private void saa()
        {
            Console.WriteLine($"DVXCV{teams}");
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
