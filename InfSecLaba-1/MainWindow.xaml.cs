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


namespace InfSecLaba_1
{
    public partial class MainWindow : Window
    {
        UsersController u;
        User curUser;

        public MainWindow(User authUser)
        {
            InitializeComponent();
            u = UsersController.GetInstance();
            curUser = authUser;
            
            WindowUserList.ItemsSource = u.Users;
            if (curUser.Role != "ADMIN")
            {
                WindowUserList.Visibility = Visibility.Collapsed;
            }

            textblock_cur_name.Text = curUser.Name;
            textblock_cur_role.Text = curUser.Role;
        }

        // TODO: Нужно сделать добавление пользователей администратором (интерфейс)
        // TODO: Нужно сделать блокироку пользователей (интерфейс)
        // TODO: Нужно сделать Включение выключение ограниченей (интерфейс)

        // TODO: Сделать окошко About

        private void MenuItem_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePaswordWindow changePaswordWindow = new ChangePaswordWindow(ref curUser);
            changePaswordWindow.ShowDialog();
        }
    }
}
