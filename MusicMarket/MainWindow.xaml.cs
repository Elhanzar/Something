using MusicMarket.The_moments_with_registration_and_entry;
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

namespace MusicMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Shopping_cart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Purchased_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Liked_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            Button Entrance_but = (Button)sender;
            if (Entrance_but.Content.ToString() == "Пользователь")
            {
                new The_moment_of_entry().Show();
            }
            if (Entrance_but.Content.ToString() == "Компания")
            {
                new The_moment_of_entry_company().Show();
            }
            MessageBox.Show("Extrance");
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Button Registration_but = (Button)sender;
            if (Registration_but.Content.ToString() == "Пользователь")
            {
                new The_moment_of_registration().Show();
            }
            if (Registration_but.Content.ToString() == "Компания")
            {
                new The_moment_of_registration_company().Show();
            }
            MessageBox.Show("Registr");
        }
    }
}
