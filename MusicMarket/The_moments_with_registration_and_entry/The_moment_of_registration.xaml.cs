using MusicMarket.MetodsModels;
using MusicMarket.Models;
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

namespace MusicMarket.The_moments_with_registration_and_entry
{
    public partial class The_moment_of_registration : Window
    {
        public The_moment_of_registration()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

            new Metods_User(
                new User()
                {
                    Name = Name.Text,
                    Surname = Surname.Text,
                    Patronymic = Patronymic.Text,
                    Email = Email.Text,
                    Password = Password.Text
                }
            );
            Close();
        }
    }
}
