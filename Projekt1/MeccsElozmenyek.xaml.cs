using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekt1
{

    public partial class MeccsElozmenyek : Window
    {
        ObservableCollection<Match> matches;
        ObservableCollection<string> teamNames;
        bool windowFullyLoaded = false;
        public MeccsElozmenyek(ObservableCollection<Match> matches, ObservableCollection<string> teamNames)
        {
            InitializeComponent();
            windowFullyLoaded = true;
            this.matches = matches;
            this.teamNames = teamNames;
            teamsCB.DataContext = teamNames;
            meccsekLB.DataContext = matches;
            meccsekLB.Items.Refresh();
            GetMostWinsandLoses();
        }

        private ObservableCollection<Match> GetFilteredMatches()
        {
            if(teamsCB.SelectedIndex != -1)
            {
                ObservableCollection<Match> filteredMatches = new ObservableCollection<Match>();
                matches.ToList().ForEach(x =>
                {
                    if (x.home == teamsCB.SelectedItem.ToString() || x.away == teamsCB.SelectedItem.ToString())
                        filteredMatches.Add(x);
                });
                return filteredMatches;
            }
            allTeamsRB.IsChecked = true;
            return matches;
        }
        private void teamsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            allTeamsRB.IsChecked = false;
            hazaiMeccsekCB.IsEnabled = vendegMeccsekCB.IsEnabled = true;
            hazaiMeccsekCB.IsChecked = false;
            vendegMeccsekCB.IsChecked = false;
            meccsekLB.DataContext = GetFilteredMatches();
            meccsekLB.Items.Refresh();
        }

        private void allTeamsRB_Checked(object sender, RoutedEventArgs e)
        {
            meccsekLB.DataContext = matches;
            meccsekLB.Items.Refresh();
            if (windowFullyLoaded) {
                hazaiMeccsekCB.IsEnabled = vendegMeccsekCB.IsEnabled = false;
                hazaiMeccsekCB.IsChecked = false;
                vendegMeccsekCB.IsChecked = false;
                teamsCB.SelectedIndex = -1;
            }
        }

        private ObservableCollection<Match> GetHomeMatches()
        {
            ObservableCollection<Match> filteredHomeMatches = new ObservableCollection<Match>();
            matches.ToList().ForEach(x => { if (x.home == teamsCB.SelectedItem.ToString()) { filteredHomeMatches.Add(x); }; });
            return filteredHomeMatches;
        }

        private ObservableCollection<Match> GetAwayMatches()
        {
            ObservableCollection<Match> filteredAwayMatches = new ObservableCollection<Match>();
            matches.ToList().ForEach(x => { if (x.away == teamsCB.SelectedItem.ToString()) { filteredAwayMatches.Add(x); }; });
            return filteredAwayMatches;
        }


        private void hazaiMeccsekCB_Checked(object sender, RoutedEventArgs e)
        {
            if(windowFullyLoaded && !(bool)vendegMeccsekCB.IsChecked && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext = GetHomeMatches();
                meccsekLB.Items.Refresh();
            }
            else if(windowFullyLoaded)
            {
                meccsekLB.DataContext = GetFilteredMatches();
                meccsekLB.Items.Refresh();
            }
        }

        private void vendegMeccsekCB_Checked(object sender, RoutedEventArgs e)
        {
            if (windowFullyLoaded && !(bool)hazaiMeccsekCB.IsChecked && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext = GetAwayMatches();
                meccsekLB.Items.Refresh();
            }
            else if(windowFullyLoaded)
            {
                meccsekLB.DataContext = GetFilteredMatches();
                meccsekLB.Items.Refresh();
            }
        }

        private void hazaiMeccsekCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if(windowFullyLoaded && (bool)vendegMeccsekCB.IsChecked && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext = GetAwayMatches();
                meccsekLB.Items.Refresh();
            } else if (windowFullyLoaded && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext=null;
                meccsekLB.Items.Refresh();
            }
        }

        private void vendegMeccsekCB_Unchecked(object sender, RoutedEventArgs e)
        {
            if (windowFullyLoaded && (bool)hazaiMeccsekCB.IsChecked && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext = GetHomeMatches(); ;
                meccsekLB.Items.Refresh();
            }
            else if (windowFullyLoaded && !(bool)allTeamsRB.IsChecked)
            {
                meccsekLB.DataContext = null;
                meccsekLB.Items.Refresh();
            }
        }

        private void GetMostWinsandLoses()
        {
            Dictionary<string, int> Winstat = new Dictionary<string, int>();
            Dictionary<string, int> Drawstat = new Dictionary<string, int>();
            Dictionary<string, int> Losestat = new Dictionary<string, int>();
            int mostWins = 0;
            int mostLoses = 0;
            ObservableCollection<string> stat = new ObservableCollection<string>();
            teamNames.ToList().ForEach(x =>
            {
                Winstat[x] = 0;
                Losestat[x] = 0;
                Drawstat[x] = 0;
            });
            matches.ToList().ForEach(x =>
            {
                teamNames.ToList().ForEach(team =>
                {
                    if(x.home == team)
                    {
                        if(int.Parse(x.homeGoal) > int.Parse(x.awayGoal))
                        {
                            Winstat[team]++;
                        }
                        else if(int.Parse(x.homeGoal) != int.Parse(x.awayGoal))
                        {
                            Losestat[team]++;
                        }
                        else
                        {
                            Drawstat[team]++;
                        }
                    }
                    if (x.away == team)
                    {
                        if (int.Parse(x.homeGoal) < int.Parse(x.awayGoal))
                        {
                            Winstat[team]++;
                        }
                        else if (int.Parse(x.homeGoal) != int.Parse(x.awayGoal))
                        {
                            Losestat[team]++;
                        }
                        else
                        {
                            Drawstat[team]++;
                        }
                    }
                });
            });
            teamNames.ToList().ForEach(x =>
            {
                stat.Add($"{x}: {Winstat[x]}W {Drawstat[x]}D {Losestat[x]}L");
            });
            statLB.DataContext = stat;
            Winstat.ToList().ForEach(x =>
            {
                if(x.Value > mostWins)
                {
                    mostWins = x.Value;
                }
            });
            Losestat.ToList().ForEach(x =>
            {
                if(x.Value > mostLoses)
                {
                    mostLoses = x.Value;
                }
            });
            Winstat.ToList().ForEach(x =>
            {
                if (x.Value == mostWins)
                {
                    mostWinsLabel.Content += $"{x.Key} ({x.Value})" ; //ha holtverseny lenne az összeset írja ki
                }
            });
            Losestat.ToList().ForEach(x =>
            {
                if (x.Value == mostLoses)
                {
                    mostLosesLabel.Content += $"{x.Key} ({x.Value}) "; //ha holtverseny lenne az összeset írja ki
                }
            });
        }
    }
}
