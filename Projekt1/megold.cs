using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Projekt1
{
    public class megold: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string tulajdonsagNev)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));
        }

        private ObservableCollection<Match> _matches;
        public ObservableCollection<Match> matches
        {
            get { return _matches; }
            set { _matches = value; OnPropertyChanged("matches"); }
        }

        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged("teams"); }
        }


        private ObservableCollection<string> _teamsNames;
        public ObservableCollection<string> teamsNames
        {
            get { return _teamsNames; }
            set { _teamsNames = value; OnPropertyChanged("teamsNames"); }
        }


        public List<Match> newMatches { get; set; }

        private int matchCount;
        public megold()
        {
            _matches = new ObservableCollection<Match>();
            newMatches = new List<Match>();
            _teams = new ObservableCollection<Team>();
            _teamsNames = new ObservableCollection<string>();

            callMatchAPI();
            callTeamAPI();
        }
        public async Task callMatchAPI()
        {
            foreach (var item in await API.getMatchesFromAPI())
                _matches.Add(item);
            getTeamesData();
        }

        public async Task callTeamAPI()
        {

            foreach (var item in await API.getTeamsFromAPI())
                _teams.Add(item);

            getTeamesData();
            getTeamsName();

        }


        public void getTeamsName()
        {
            foreach (var team in _teams)
            {
                _teamsNames.Add(team.name);
            }
        }



        public void getTeamesData()
        {
            foreach (var team in _teams)
            {
                // If the team is at home
                team.point = 0;
                team.games = 0;
                team.goalsConceided = 0;
                team.goalsScored = 0;

                List<Match> filteredHome = _matches.ToList().Where(x => x.home == team.name).ToList();
                team.games += filteredHome.Count;
                foreach (var match in filteredHome)
                {
                    team.point += (int.Parse(match.homeGoal) > int.Parse(match.awayGoal)) ? 3 : (int.Parse(match.homeGoal) == int.Parse(match.awayGoal)) ? 1 : 0;
                    team.goalsScored += int.Parse(match.homeGoal);
                    team.goalsConceided += int.Parse(match.awayGoal);
                }

                // If the team is away
                List<Match> filteredAway = _matches.ToList().Where(x => x.away == team.name).ToList();
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
                matchCount = _matches.Count;
            }
            string id = matchCount.ToString();
            matchCount++;
            _matches.Add(new Match(id, home, homeGoal, away, awayGoal));
            newMatches.Add(new Match(id, home, homeGoal, away, awayGoal));
            getTeamesData();
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
