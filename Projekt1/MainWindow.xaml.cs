using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        megold megoldas = new megold();
        private int id = 1;
        private int fordulokSzama = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = megoldas;
            //makeSortLB();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            megoldas.runOnClose();
        }

        private void Mentés_Click(object sender, RoutedEventArgs e)
        {
            string name = CsapatNevTb.Text.ToString();
            if (!String.IsNullOrEmpty(name))
            {
                if (!megoldas.teamsNames.Contains(name))
                {
                    if (LogoUrlTb.Text.ToString() != "")
                    {
                        megoldas.makeNewTeam(name, LogoUrlTb.Text.ToString());
                        data.Items.Refresh();
                        CsapatNevTb.Text = "";
                        LogoUrlTb.Text = "";
                        megoldas.getTeamsName();
                        hazaiNev.Items.Refresh();
                        vendegNev.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Kérem adjon meg a csapat lógóját!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("Ilyen néven már létezik egy csapat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Kérem adjon meg egy csapatnevet!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }


        private void hazaiNev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hazaiNev.SelectedItem != null)
            {
                string? teamName = hazaiNev.SelectedItem.ToString();
                megoldas.teams.ToList().ForEach(x =>
                {
                    if (x.name == teamName)
                    {
                        hazaiLogo.Source = new BitmapImage(new Uri(x.logoSource));
                    }
                });
            }
        }
        private void vendegNev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vendegNev.SelectedItem != null)
            {
                string teamName = vendegNev.SelectedItem.ToString();
                megoldas.teams.ToList().ForEach(x =>
                {
                    if (x.name == teamName)
                    {
                        vendegLogo.Source = new BitmapImage(new Uri(x.logoSource));
                    }
                });
            }
        }

        private void RogzitBtn_Click(object sender, RoutedEventArgs e)
        {
            int teamsFinishedDb = 0;
            meccsElőzményekBtn.IsEnabled = true;
            if (vendegNev.SelectedItem != null && hazaiNev.SelectedItem != null)
            {
                if (!String.IsNullOrEmpty(hazaiGolok.Text) && int.TryParse(hazaiGolok.Text, out int hg) && !String.IsNullOrEmpty(vendegGolok.Text) && int.TryParse(vendegGolok.Text, out int vg))
                {
                    if (vendegNev.SelectedItem != hazaiNev.SelectedItem)
                    {
                        megoldas.makeNewMatch(hazaiNev.SelectedItem.ToString(), hazaiGolok.Text, vendegNev.SelectedItem.ToString(), vendegGolok.Text);
                        hazaiGolok.Text = "";
                        vendegGolok.Text = "";
                        var UriSrcHT = new Uri(@"/Projekt1;component/img/HT.png", UriKind.Relative);
                        hazaiLogo.Source = new BitmapImage(UriSrcHT);
                        var UriSrcAT = new Uri(@"/Projekt1;component/img/AT.png", UriKind.Relative);
                        vendegLogo.Source = new BitmapImage(UriSrcAT);
                        data.Items.Refresh();
                        megoldas.sortData("Pontok száma");
                        if(fordulokSzama != 0)
                        {
                            foreach (var t in megoldas.teams)
                            {
                                if (t.games >= fordulokSzama)
                                {
                                    teamsFinishedDb++;
                                    
                                }
                            }
                            if (teamsFinishedDb == megoldas.teams.Count)
                            {
                                //MessageBox.Show($"{megoldas.teams[0].name}", "Vége", MessageBoxButton.OK);
                                urlapSP.Visibility = Visibility.Hidden;

                                StackPanel bajnokSp = new StackPanel();
                                bajnokSp.SetValue(Grid.RowProperty, 1);
                                bajnokSp.SetValue(Grid.ColumnProperty, 1);

                                Image bajnokImg = new Image();
                                bajnokImg.Source = new BitmapImage(new Uri(megoldas.teams[0].logoSource));
                                bajnokImg.Width = 100;
                                bajnokImg.Height = 100;
                                bajnokImg.HorizontalAlignment = HorizontalAlignment.Center;
                                bajnokImg.Margin = new Thickness(0, 100, 0, 0);
                                bajnokSp.Children.Add(bajnokImg);

                                Label bajnokLabel = new Label();
                                bajnokLabel.Content = $"{megoldas.teams[0].name} a bajnok";
                                bajnokLabel.FontSize = 25;
                                bajnokLabel.HorizontalAlignment = HorizontalAlignment.Center;
                                bajnokLabel.Foreground = new SolidColorBrush(Colors.White);
                                bajnokSp.Children.Add(bajnokLabel);

                                Button newSeasonBtn = new Button();
                                newSeasonBtn.Content = "OK";
                                newSeasonBtn.Width = 100;
                                newSeasonBtn.Height = 30;
                                newSeasonBtn.HorizontalAlignment = HorizontalAlignment.Center;
                                newSeasonBtn.Click += (s, e) =>
                                {
                                    fordulokDBBtn.IsEnabled = true;
                                    fordulokDBTB.Text = "";
                                    fordulokDBTB.IsEnabled = true;
                                    grid.Children.Remove(bajnokSp);
                                    urlapSP.Visibility = Visibility.Visible;
                                    //itt kellene Resettelni az eredményeket és a tabellát
                                };
                                bajnokSp.Children.Add(newSeasonBtn);

                                grid.Children.Add(bajnokSp);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Egy csapat önmagával nem játszhat!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Helytelen adatok adott meg a góloknál!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void meccsElőzményekBtn_Click(object sender, RoutedEventArgs e)
        {
            MeccsElozmenyek window = new MeccsElozmenyek(megoldas.matches, megoldas.teamsNames);
            window.ShowDialog();
        }

        private void fordulokDBBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fordulokDBTB.Text) && int.TryParse(fordulokDBTB.Text, out int fDb))
            {
                fordulokDBTB.IsEnabled = false;
                fordulokDBBtn.IsEnabled = false;
                urlapSP.IsEnabled = true;
                fordulokSzama = fDb;
            }
        }


        //public void makeSortLB()
        //{
        //    string[] sortTypes = { "Csapatok", "Lejátszott mecsek száma", "Lőtt gólok száma", "Kapott gólok száma", "Pontok száma" };
        //    sortCB.ItemsSource = sortTypes;
        //    sortCB.SelectedIndex = 4;


        //}

        //public void sortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    megoldas.sortData(sortCB.SelectedItem.ToString());
        //}
    }
}
