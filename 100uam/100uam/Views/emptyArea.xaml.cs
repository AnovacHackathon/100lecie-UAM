using _100uam.Elements;
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

namespace _100uam.Views
{
    /// <summary>
    /// Interaction logic for emptyArea.xaml
    /// </summary>
    public partial class emptyArea : UserControl
    {
        MainWindow mainWindow;
        Wydzialy otoczenie;

        public emptyArea(Wydzialy otoczenie, string texture, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.otoczenie = otoczenie;
            InitializeComponent();
            WydzialName.Text = otoczenie.GetName;
            emptyAreaTexture.Source = new BitmapImage(new Uri(@texture));
        }

        public Wydzialy GetOtoczenie{
            get{
                return otoczenie;
            }
            }

        private void build_Click(object sender, RoutedEventArgs e)
        {
            var myWin = (MainWindow)Application.Current.MainWindow;
            if (otoczenie.Status==1)
            {
                otoczenie.Zburz();
                otoczenie.Status = 0;
                build.Content = "Buduj";
                myWin.wydatki = myWin.wydatki - (otoczenie.GetCost - 3000);
            }
            else
            {
                otoczenie.Status = 1;
                build.Content = "Zburz";                       
                myWin.wydatki = myWin.wydatki + otoczenie.GetCost;
            }
            myWin.Aktualizacja();
        }

    }
}
