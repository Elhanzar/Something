using MusicMarket.ContextModels;
using MusicMarket.MetodsModels;
using MusicMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusicMarket.Ability_Company
{
    /// <summary>
    /// Логика взаимодействия для The_moment_of_add_track.xaml
    /// </summary>
    public partial class The_moment_of_add_track : Window
    {
        public The_moment_of_add_track()
        {
            InitializeComponent();
            this.DataContext = new Models.Track();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.company = company;
            mainWindow.Menu.IsEnabled = true;
            mainWindow.Menu_Scrol.IsEnabled = true;
            mainWindow.TreeRgtExt.Visibility = Visibility.Collapsed;
            mainWindow.existence();
            mainWindow.MenuGenerator(1);
            mainWindow.Show();
            Close();
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        bool MyValid()
        {
            if (Musicname.Text != null)
            {
                if (Musicname.Text.Length > 50 || IsDigitsOnly(Musicname.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (Groupname.Text != null)
            {
                if (Groupname.Text.Length > 50 || IsDigitsOnly(Groupname.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (Genre.Text != null)
            {
                if (Genre.Text.Length > 50 || IsDigitsOnly(Genre.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (int.Parse(Quantity.Text)!= 0)
            {
                if (!IsDigitsOnly(Quantity.Text.ToString()))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (int.Parse(Costprice.Text) != 0)
            {
                if (!IsDigitsOnly(Costprice.Text.ToString()))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (int.Parse(Price.Text) != 0)
            {
                if (!IsDigitsOnly(Price.Text.ToString()))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public Company company = new Company();
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (MyValid())
            {
                Metods_Track metods_Track = new Metods_Track();
                if (metods_Track.Add_track(
                    new Models.Track()
                    {
                        Musicname = Musicname.Text,
                        Groupname = Groupname.Text,
                        Genre = Genre.Text,
                        Quantity = int.Parse(Quantity.Text),
                        Costprice = int.Parse(Costprice.Text),
                        Price = int.Parse(Price.Text),
                        Daterelize = DateTime.Now,
                        Authorid = company.Id,
                        Authorname = company.Name
                    }
                ))
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.company = company;
                    mainWindow.Menu.IsEnabled = true;
                    mainWindow.Menu_Scrol.IsEnabled = true;
                    mainWindow.TreeRgtExt.Visibility = Visibility.Collapsed;
                    mainWindow.existence();
                    mainWindow.MenuGenerator(1);
                    mainWindow.Show();
                    Close();
                }
                else MessageBox.Show("Не получилось :_(");
            }
            else
            {
                return;
            }
        }
        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());

        }
    }
}
