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

namespace InfSecLaba_1
{
    public partial class LoginWindow : Window
    {

        int attempt = 0;
        SolidColorBrush LightRed = new SolidColorBrush(Color.FromRgb(255, 100, 100));
        SolidColorBrush LightGray = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        UsersController u;
        User curUser;

        public LoginWindow()
        {
            u = UsersController.GetInstance();
            curUser = null;
            u.RestUsers();
            u.AddUser("admin", "ADMIN");
            u.AddUser("user", "USER");
            u.AddUser("user2", "USER");
            u.AddUser("user3", "USER", "322");
            u.AddUser("user4", "USER");
            InitializeComponent();
        }

        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            u = UsersController.GetInstance();
            string name = textbox_name.Text;

            foreach (User user in u.Users)
            {
                if (user.Name == name)
                {
                    if (user.Password == "")
                    {
                        curUser = user;
                        ViewerFirstEnter();
                        return;
                    }
                    else
                    {
                        curUser = user;
                        ViewerAuthorize();
                        return;
                    }
                }
            }

            MessageBox.Show("Пользователь с таким именем отсутствует");

            ViewerDefault();
        }

        private void Button_Authorize_Click(object sender, RoutedEventArgs e)
        {
            string pass = textbox_pass.Password;
            string pass2 = textbox_pass2.Password;

            if (attempt >= 3)
            {
                MessageBox.Show("Пароль введен неверное более трех раз.\nЗавершение работы.");
                Close();
                return;
            }

            if(pass2 == "")
            {
                try
                {
                    MainWindow mainWindow = new MainWindow(curUser.Authorize(pass));
                    mainWindow.Show();
                    Close();
                }
                catch (PasswordException ex)
                {
                    attempt++;
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    curUser.ChangePassword(pass,pass2);
                    MainWindow mainWindow = new MainWindow(curUser.Authorize(pass));
                    mainWindow.Show();
                    Close();
                }
                catch (PasswordException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            attempt = 0;
            ViewerDefault();
        }

        private void ViewerFirstEnter()
        {
            textblock_enter.Text = "Первый вход";
            textblock_pass.Visibility = Visibility.Visible;
            textblock_pass2.Visibility = Visibility.Visible;
            textbox_name.IsEnabled = false;
            textbox_pass.Visibility = Visibility.Visible;
            textbox_pass2.Visibility = Visibility.Visible;
            button_back.Visibility = Visibility.Visible;
            button_authorize.Visibility = Visibility.Visible;
            button_next.Visibility = Visibility.Collapsed;
        }

        private void ViewerAuthorize()
        {
            textblock_enter.Text = "Вход";
            textblock_pass.Visibility = Visibility.Visible;
            textbox_name.IsEnabled = false;
            textbox_pass.Visibility = Visibility.Visible;
            button_back.Visibility = Visibility.Visible;
            button_authorize.Visibility = Visibility.Visible;
            button_next.Visibility = Visibility.Collapsed;
        }

        private void ViewerDefault()
        {
            textblock_enter.Text = "Вход";
            textblock_pass.Visibility = Visibility.Collapsed;
            textblock_pass2.Visibility = Visibility.Collapsed;
            textbox_name.IsEnabled = true;
            textbox_pass.Visibility = Visibility.Collapsed;
            textbox_pass2.Visibility = Visibility.Collapsed;
            textbox_pass.BorderBrush = LightGray;
            textbox_pass2.BorderBrush = LightGray;
            button_authorize.Visibility = Visibility.Collapsed;
            button_back.Visibility = Visibility.Collapsed;
            button_next.Visibility = Visibility.Visible;
            attempt = 0;

            textbox_pass.Password = "";
            textbox_pass2.Password = "";
        }

        
    }
}
