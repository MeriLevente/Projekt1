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
        public MeccsElozmenyek(ObservableCollection<Match> matches, ObservableCollection<string> teamNames)
        {
            InitializeComponent();
            this.matches = matches;
            this.teamNames = teamNames;
            ResultsUpdater();
        }

        public void ResultsUpdater()
        {
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

        private void teamsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            allTeamsRB.IsChecked = false;
            ObservableCollection<Match> filteredMatches = new ObservableCollection<Match>();
            matches.ToList().ForEach(x =>
            {
               if (x.home == teamsCB.SelectedItem.ToString() || x.away == teamsCB.SelectedItem.ToString())
                     filteredMatches.Add(x);
            });
            meccsekLB.DataContext = filteredMatches;
            meccsekLB.Items.Refresh();
        }

        private void allTeamsRB_Checked(object sender, RoutedEventArgs e)
        {
            meccsekLB.DataContext = matches;
            meccsekLB.Items.Refresh();
        }
    }
}
