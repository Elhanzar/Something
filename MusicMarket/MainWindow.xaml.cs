using MusicMarket.Ability_Company;
using MusicMarket.ContextModels;
using MusicMarket.MetodsModels;
using MusicMarket.Models;
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
        public User user = new User() {
            //Id = 1,
            //Name = "q",
            //Surname = "q",
            //Patronymic = "q",
            //Password = "q"
        };
        public Company company = new Company();
        bool MenuOrShopped = true;
        public int existence()
        {
            if (user.Id == 0 && company.Id == 0)
            {
                Menu.IsEnabled = false;
                Menu_Scrol.IsEnabled = false;
                return 0;
            }     
            else
            {
                if (company.Id != 0)
                {
                    Addproduct.Visibility = Visibility.Visible;
                    ShoppingCart.Visibility = Visibility.Collapsed;
                    MenuUserName.TextWrapping = TextWrapping.Wrap;
                    MenuUserName.Text = $"\t{company.Name}";
                    return 1;
                }
            }
            Addproduct.Visibility = Visibility.Collapsed;
            ShoppingCart.Visibility = Visibility.Visible;
            MenuUserName.TextWrapping = TextWrapping.Wrap;
            MenuUserName.Text = $"\t{user.Surname}\t{user.Name}";
            return 2;
        }
        public MainWindow()
        {
            InitializeComponent();
            existence();
            MenuGenerator(1);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuGenerator(1);
            MenuOrShopped = true;
        }
        private void Shopping_cart_Click(object sender, RoutedEventArgs e)
        {
            ShoppedGenerator(1);
            MenuOrShopped = false;
        }
        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Track track = (Track)button.Tag;
            Order shoppingCart = new Order() {
                Trackid = track.Id,
                Userid = user.Id
            };
            new Metods_ShoppingCart().ADD_InShoppingCart(shoppingCart);
            MenuGenerator(1);
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Track track = (Track)button.Tag;
            new Metods_Track().DeleteTrack(track);
            MenuGenerator(1);
        }
        private void refuse_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Order shoppingCart = (Order)button.Tag;
            new Metods_ShoppingCart().Delete_FromShoppingCart(shoppingCart);
            ShoppedGenerator(1);
        }

        private void Search_Click(object sender, MouseButtonEventArgs e)
        {
            if(MenuOrShopped)
            {
                MenuGenerator(2);
            }
            else
            {
                ShoppedGenerator(2);
            }
        }

        public void MenuGenerator(int ForSearch)
        {
            Menu_Panel.Children.Clear();
            List<Track> tracks = new List<Track>();
            Metods_Track metods_Track = new Metods_Track();
            switch (ForSearch)
            {
                case 1:
                    metods_Track.Get_all();
                    break;
                case 2:
                    metods_Track.PrintWhere(Poiskovik.Text);
                    break; 
                case 3:
                    metods_Track.Expensive();
                    break;
                case 4:
                    metods_Track.Сheap();
                    break;
                case 5:
                    metods_Track.NewsTracks();
                    break;
                case 6:
                    metods_Track.OldsTracks();
                    break;

            };
            metods_Track.SourseUser(ref tracks);

            for (int i = 0; i < tracks.Count; i++)
            {
                DockPanel dockPanel = new DockPanel();

                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.Background = Brushes.Transparent;
                textBlock.Text = "Название: " + tracks[i].Musicname + "\nГруппа: " + tracks[i].Groupname + "\nАвтор: " + tracks[i].Authorname + "\nЖанр: " + tracks[i].Genre 
                    + "\nОсталось: " + tracks[i].Quantity + "\nДата релиза: " + tracks[i].Daterelize + "\nЦена: " + tracks[i].Price;
                textBlock.Width = 201;
                textBlock.Margin = new Thickness(10, 5, 0, 0);

                Button button = new Button();
                Border borderButtons = new Border();
                borderButtons.CornerRadius = new CornerRadius(8, 8, 8, 8);
                borderButtons.BorderThickness = new Thickness(3, 3, 3, 3);
                borderButtons.Height = 50;
                borderButtons.Width = 110;
                if (existence() == 1)
                {
                    if (tracks[i].Authorid == company.Id)
                    {
                        borderButtons.BorderBrush = Brushes.Red;
                        borderButtons.Background = Brushes.Red;
                        button.Background = Brushes.Red;
                        button.Content = "Удалить";
                        button.Height = 40;
                        button.Width = 100;
                        button.Tag = tracks[i];
                        button.Click += Delete_Click;
                        borderButtons.Child = (button);
                        dockPanel.Children.Add(borderButtons);
                    }

                }
                else
                {
                    if (tracks[i].Quantity == 0) button.IsEnabled = false;
                    borderButtons.BorderBrush = Brushes.Green;
                    borderButtons.Background = Brushes.Green;
                    button.Background = Brushes.Green;
                    button.Content = "Купить: " + tracks[i].Price;
                    button.Height = 40;
                    button.Width = 100;
                    button.Tag = tracks[i];
                    button.Click += Shop_Click;
                    borderButtons.Child = (button);
                    dockPanel.Children.Add(borderButtons);
                }

                dockPanel.Height = 100;
                dockPanel.Width = 460;
                dockPanel.Children.Add(textBlock);
                dockPanel.HorizontalAlignment = HorizontalAlignment.Right;
                dockPanel.Margin = new Thickness(20);


                Border border = new Border();
                border.CornerRadius = new CornerRadius(8, 8, 8, 8);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(3, 3, 3, 3);
                border.Margin = new Thickness(0, 20, 0, 0);
                border.Child = dockPanel;
                Menu_Panel.Children.Add(border);
            }
        }

        void ShoppedGenerator(int ForSearch)
        {
            Menu_Panel.Children.Clear();
            List<Order> shoppingCarts = new List<Order>();
            Metods_ShoppingCart metods_ShoppingCart = new Metods_ShoppingCart();
            switch (ForSearch)
            {
                case 1:
                    metods_ShoppingCart.Get_all(user.Id);
                    break;
                case 2:
                    metods_ShoppingCart.PrintWhere(user.Id,Poiskovik.Text);
                    break;
                case 3:
                    metods_ShoppingCart.Expensive(user.Id);
                    break;
                case 4:
                    metods_ShoppingCart.Сheap(user.Id);
                    break;
                case 5:
                    metods_ShoppingCart.NewsTracks(user.Id);
                    break;
                case 6:
                    metods_ShoppingCart.OldsTracks(user.Id);
                    break;
            };
            metods_ShoppingCart.SourseShopCart(ref shoppingCarts);
            for (int i = 0; i < shoppingCarts.Count; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.Width = 300;
                textBlock.Background = Brushes.Transparent;
                textBlock.Text = "Название: " + shoppingCarts[i].Track.Musicname + "\nГруппа: " + shoppingCarts[i].Track.Groupname + "\nАвтор: " + shoppingCarts[i].Track.Authorname + "\nЖанр: " + shoppingCarts[i].Track.Genre + "\nОсталось: " + shoppingCarts[i].Track.Quantity + "\nДата релиза: " + shoppingCarts[i].Track.Daterelize + "\nЦена: " + shoppingCarts[i].Track.Price;
                textBlock.Margin = new Thickness(10, 5, 0, 0);

                Button button = new Button();
                button.Background = Brushes.Red;
                button.Content = "Отказаться";
                button.Height = 40;
                button.Width = 100;
                button.Tag = shoppingCarts[i];
                button.Click += refuse_Click;

                Border borderButtons = new Border();
                borderButtons.CornerRadius = new CornerRadius(8, 8, 8, 8);
                borderButtons.BorderBrush = Brushes.Red;
                borderButtons.Background = Brushes.Red;
                borderButtons.BorderThickness = new Thickness(3, 3, 3, 3);
                borderButtons.Height = 50;
                borderButtons.Width = 110;
                borderButtons.Child = (button);

                DockPanel dockPanel = new DockPanel();
                dockPanel.Height = 100;
                dockPanel.Width = 500;
                dockPanel.Children.Add(textBlock);
                dockPanel.Children.Add(borderButtons);
                dockPanel.HorizontalAlignment = HorizontalAlignment.Right;
                dockPanel.Margin = new Thickness(20);

                Border border = new Border();
                border.CornerRadius = new CornerRadius(8, 8, 8, 8);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(3, 3, 3, 3);
                border.Margin = new Thickness(0, 20, 0, 0);
                border.Child = dockPanel;
                Menu_Panel.Children.Add(border);
            }
        }
        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            Button Entrance_but = (Button)sender;
            if (Entrance_but.Content.ToString() == "Пользователь")
            {
                new The_moment_of_entry().Show();
                Close();
            }
            if (Entrance_but.Content.ToString() == "Компания")
            {
                new The_moment_of_entry_company().Show();
                Close();
            }
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Button Registration_but = (Button)sender;
            if (Registration_but.Content.ToString() == "Пользователь")
            {
                new The_moment_of_registration().Show();
                Close();
            }
            if (Registration_but.Content.ToString() == "Компания")
            {
                new The_moment_of_registration_company().Show();
                Close();
            }
        }

        private void Addproduct_Click(object sender, RoutedEventArgs e)
        {
            The_moment_of_add_track momentAddTrack = new The_moment_of_add_track();
            momentAddTrack.company = company;
            momentAddTrack.Show();
            Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if (MenuOrShopped)
            {
                MenuGenerator(3);
            }
            else
            {
                ShoppedGenerator(3);
            }
        }

        private void Filter_Click1(object sender, RoutedEventArgs e)
        {
            if (MenuOrShopped)
            {
                MenuGenerator(4);
            }
            else
            {
                ShoppedGenerator(4);
            }
        }

        private void Filter_Click2(object sender, RoutedEventArgs e)
        {
            if (MenuOrShopped)
            {
                MenuGenerator(5);
            }
            else
            {
                ShoppedGenerator(5);
            }
        }

        private void Filter_Click3(object sender, RoutedEventArgs e)
        {
            if (MenuOrShopped)
            {
                MenuGenerator(6);
            }
            else
            {
                ShoppedGenerator(6);
            }
        }
    }
}
