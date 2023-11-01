using System;
using System.Collections.Generic;
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
        megold megoldas = new megold();
        public MeccsElozmenyek()
        {
            InitializeComponent();
            this.DataContext = megoldas;
        }
    }
}
