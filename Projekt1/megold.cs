using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Projekt1
{
    public class megold : INotifyPropertyChanged
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
            _teamsNames.Clear();
            foreach (var team in _teams)
            {
                _teamsNames.Add(team.name);
            }
        }



        public void delMatchById(string id)
        {
            _matches.Remove(_matches.Where(x => x.id == id).First());
            API.deletByIdAPI(id, "matches");
            getTeamesData();
        }


        public void delTeamById(string id)
        {
            _teams.Remove(_teams.Where(x => x.id == id).First());
            API.deletByIdAPI(id, "teams");
            getTeamsName();
            getTeamesData();
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
            sortData("");
        }



        public void makeNewMatch(string home, string homeGoal, string away, string awayGoal)
        {
            List<Match> matchesw = (List<Match>)matches.ToList();
            List<int> ids = new List<int>();
            foreach (var item in matchesw)
            {
                ids.Add(int.Parse(item.id));
            }
            if (ids.Count == 0)
            {
                ids.Add(0);
            }
            int a = ids.Max() + 1 ;
            string id = a.ToString();
            _matches.Add(new Match(id, home, homeGoal, away, awayGoal));
            sendAPI($"{id};{home};{homeGoal};{away};{awayGoal}");
            getTeamsName();
            getTeamesData();
        }




        public void makeNewTeam(string name, string logoUrl)
        {

            List<Team> teamsL = (List<Team>)teams.ToList();
            List<int> ids = new List<int>();
            foreach (var item in teamsL)
            {
                ids.Add(int.Parse(item.id));
            }
            if (ids.Count == 0)
            {
                ids.Add(0);
            }
            int a = ids.Max() + 1;
            string id = a.ToString();

            _teams.Add(new Team(id, name, 0, 0, 0, 0, logoUrl));

            var _ = API.appendTeamsAPI($"{id};{name};{logoUrl}");

            getTeamesData();
        }




        public async Task sendAPI(string line)
        {
            string ret = await API.sendMatchChangeAPI(line);
            if (ret == "Failed")
            {
                MessageBox.Show("Hiba az API hívásban", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void sortData(string sender)
        {

            ObservableCollection<Team> teams2 = new ObservableCollection<Team>(teams.OrderBy(i => i.goalsScored - i.goalsConceided));

            teams = new ObservableCollection<Team>(teams2.OrderBy(i => i.point).Reverse());
            teams.Reverse();
 
            //"Csapatok", "Lejátszott mecsek száma", "Lőtt gólok száma", "Kapott gólok száma", "Pontok száma"
            //switch (sender)
            //{
            //case "Csapatok":
            //    teams = new ObservableCollection<Team>(teams.OrderBy(i => i.name));

            //    break;
            //case "Lejátszott mecsek száma":
            //    teams = new ObservableCollection<Team>(teams.OrderBy(i => i.games).Reverse());
            //    break;
            //case "Lőtt gólok száma":
            //    teams = new ObservableCollection<Team>(teams.OrderBy(i => i.goalsScored).Reverse());
            //    teams.Reverse();
            //    break;
            //case "Kapott gólok száma":
            //    teams = new ObservableCollection<Team>(teams.OrderBy(i => i.goalsConceided).Reverse());
            //    teams.Reverse();
            //    break;
                //case "Pontok száma":
                //    teams = new ObservableCollection<Team>(teams.OrderBy(i => i.point).Reverse());
                //    teams.Reverse();
                //    break;
            //}


            //teams = new ObservableCollection<Team>(teams.OrderBy(i => i.games));
            //teams.Reverse();

        }

    }
}
