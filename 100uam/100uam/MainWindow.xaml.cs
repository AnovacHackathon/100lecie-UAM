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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using _100uam.Views;
using _100uam.Elements;

namespace _100uam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public int gameyear = 1919;
        public int wydatki = 0;
        public int prestiz = 0;
        public int zadowolenie =0;
        public Player player = new Player("Pan Andrzej");
        List<Wydzialy> posiadane = new List<Wydzialy>();
        List<Wydzialy> otoczenie = new List<Wydzialy>();
        public MainWindow()
        {
            InitializeComponent();
            playerName.Text = player.Name;
            Aktualizacja();
            bottomPanel.Visibility = System.Windows.Visibility.Hidden;
            showBottomPanel.Visibility = System.Windows.Visibility.Visible;
            otoczenie.Add(new Wydzialy("WMI", "Opis wmi", 50000,2000));
            otoczenie.Add (new Wydzialy("Wydział Anglistyki", "Opis Anglistyki", 30000,3000));
            otoczenie.Add (new Wydzialy("Wydział Biologii", "Opis Biologii", 6000,1000));
            SetMap();
        }



        //Bottom Grin Button Logic
        private void ShowBottomPanelClick(object sender, RoutedEventArgs e)
        {
            bottomPanel.Visibility = System.Windows.Visibility.Visible;
            showBottomPanel.Visibility = System.Windows.Visibility.Hidden;
            staffButton.IsEnabled = true;
            buildingsButton.IsEnabled = false;
            staffGrid.Visibility = System.Windows.Visibility.Hidden;
            buildingsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void BottomCollapseClick(object sender, RoutedEventArgs e)
        {
            bottomPanel.Visibility = System.Windows.Visibility.Hidden;
            showBottomPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void BuildingsButtonClick(object sender, RoutedEventArgs e)
        {
            buildingsButton.IsEnabled = false;
            staffButton.IsEnabled = true;
            buildingsGrid.Visibility = System.Windows.Visibility.Visible;
            staffGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void StaffButtonClick(object sender, RoutedEventArgs e)
        {
            buildingsButton.IsEnabled = true;
            staffButton.IsEnabled = false;
            buildingsGrid.Visibility = System.Windows.Visibility.Hidden;
            staffGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void nextRound_Click(object sender, RoutedEventArgs e)
        {
            Aktualizacja();
            player.playermoney = player.playermoney - wydatki;
            foreach(Wydzialy wydzial in posiadane)
            {
                player.playermoney=player.playermoney-(wydzial.GetUtrzymanieD);
            }

            if(player.playermoney<0)
            {
                MessageBox.Show("Przegrałeś :(");
                System.Windows.Application.Current.Shutdown();
            }
            gameyear = gameyear + 1;
            wydatki = 0;
            Aktualizacja();

        }
 
        public void Aktualizacja()
        {
            posiadane.Clear();
            foreach(Wydzialy wydzial in otoczenie)
            {
                if (wydzial.Status == 1)
                    posiadane.Add(wydzial);
            }
            money.Text = player.Playermoney + " zł";
            round.Text = wydatki + " zł";
            year.Text = "Rok "+ gameyear;
        }
        private void SetMap()
        {
            Random rnd = new Random();
            for (int i = 1; i <= 3; i++)
            {
                int texture_number = rnd.Next(1, 4);
                emptyArea emptyArea = new emptyArea(otoczenie[i-1],@"C:\Users\Anovac\Desktop\100lecie\grafika\trees0" + texture_number.ToString() + @".png",this);
                StackPanel txt = FindName("area" + i) as StackPanel;
                txt.Children.Add(emptyArea);
            }
        }

        private void area1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            Wydzialy wydzial = (stackPanel.Children[0] as emptyArea).GetOtoczenie;
            ViewWykladowca wykladowcatmp = new ViewWykladowca(wydzial, this);
            ViewPersonel personeltmp = new ViewPersonel(wydzial, this);
            PersonelStackPanel.Children.Clear();
            if (wydzial.Status == 1)
            {
                PersonelStackPanel.Children.Add(wykladowcatmp);
                PersonelStackPanel.Children.Add(personeltmp);
            }
            Aktualizacja();
        }

        private void nextRound_Click_1(object sender, RoutedEventArgs e)
        {
            player.playermoney = player.playermoney - wydatki;
            Aktualizacja();
            foreach (Wydzialy wydzial in posiadane)
            {
                player.playermoney = player.playermoney - (wydzial.GetUtrzymanieD);
            }

            if (player.playermoney < 0)
            {
                MessageBox.Show("Przegrałeś :(");
                System.Windows.Application.Current.Shutdown();
            }
            gameyear = gameyear + 1;
            wydatki = 0;
            Aktualizacja();
        }


    }
}
