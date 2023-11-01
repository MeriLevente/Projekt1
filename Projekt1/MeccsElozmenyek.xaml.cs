using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// <summary>
    /// Interaction logic for MeccsElozmenyek.xaml
    /// </summary>
    public partial class MeccsElozmenyek : Window
    { 
        public MeccsElozmenyek()
        {
            InitializeComponent();      
        }

        public void ResultsUpdater(ObservableCollection<Match> matches)
        {
            meccsekLB.DataContext = matches;
            MakeTheWinnersGreen(matches);
            meccsekLB.Items.Refresh();
        }

        private void MakeTheWinnersGreen(ObservableCollection<Match> matches)
        {
            matches.ToList().ForEach(x =>
            {
                if(int.Parse(x.homeGoal) > int.Parse(x.awayGoal))
                {
                    
                }
            });
        }
    }
}
