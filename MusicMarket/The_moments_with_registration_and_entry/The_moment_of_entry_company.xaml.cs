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
    /// <summary>
    /// Логика взаимодействия для The_moment_of_entry_company.xaml
    /// </summary>
    public partial class The_moment_of_entry_company : Window
    {
        public The_moment_of_entry_company()
        {
            InitializeComponent();
            this.DataContext = new Company();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
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
            if (Name.Text != null)
            {
                if (Name.Text.Length > 50 || IsDigitsOnly(Name.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (Number.Text != null)
            {
                if (!IsDigitsOnly(Number.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (Email.Text != null)
            {
                if (IsDigitsOnly(Email.Text))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            if (Password.Text != null)
            {
                if (IsDigitsOnly(Password.Text))
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
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (MyValid())
            {
                Metods_Company metods_Company = new Metods_Company();
                if (
                    metods_Company.Opening_Company(
                    new Company()
                    {
                        Name = Name.Text,
                        Number = Number.Text,
                        Email = Email.Text,
                        Password = Password.Text
                    }
                ))
                {
                    Company сompany = new Company();
                    metods_Company.SourseCompany(ref сompany);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.company = сompany;
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
