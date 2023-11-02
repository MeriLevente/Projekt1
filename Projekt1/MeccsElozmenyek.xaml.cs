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
        }

        private void MakeTheWinnersGreen(ObservableCollection<Match> matches)
        {
            matches.ToList().ForEach(x =>
            {
                if(int.Parse(x.homeGoal) > int.Parse(x.awayGoal))
                {
                    //homeLabel.Foreground = Brushes.Green;
                    //homeGoalLabel.FontWeight = FontWeights.Bold;
                }
            });
        }

        private ObservableCollection<Match> GetFilteredMatches()
        {
            ObservableCollection<Match> filteredMatches = new ObservableCollection<Match>();
            matches.ToList().ForEach(x =>
            {
                if (x.home == teamsCB.SelectedItem.ToString() || x.away == teamsCB.SelectedItem.ToString())
                    filteredMatches.Add(x);
            });
            return filteredMatches;
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
    }
}
