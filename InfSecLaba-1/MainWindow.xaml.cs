using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        UserFileController ufc;
        User curUser;
        BindingList<User> userDataList;

        public MainWindow(User authUser)
        {
            curUser = authUser;
            ufc = new UserFileController();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (curUser.Role != "ADMIN")
            {
                WindowUserList.Visibility = Visibility.Collapsed;
            }

            textblock_cur_name.Text = curUser.Name;
            textblock_cur_role.Text = curUser.Role;

            userDataList = new BindingList<User>();
            userDataList = ufc.LoadOnlyUsers();
            

            WindowUserList.ItemsSource = userDataList;
            userDataList.ListChanged += UserDataList_ListChanged;

        }

        private void UserDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    userDataList[e.NewIndex].Name = "User " + (e.NewIndex + 1).ToString();
                    break;
                case ListChangedType.ItemDeleted:
                    ufc.SaveUsers(userDataList, curUser);
                    break;
                case ListChangedType.ItemChanged:
                    if (userDataList[e.NewIndex].Name == "admin")
                    {
                        userDataList[e.NewIndex].Name = "User " + (e.NewIndex + 1).ToString();
                        MessageBox.Show("Нельзя использовать имя: admin");
                    }
                    else if(userDataList[e.NewIndex].Name == "")
                    {
                        userDataList[e.NewIndex].Name = "User " + (e.NewIndex + 1).ToString();
                        MessageBox.Show("Введите имя пользователя");
                    }
                    for (int i = 0; i < userDataList.Count; i++)
                    {
                        if (userDataList[i].Name == userDataList[e.NewIndex].Name && i != e.NewIndex)
                        {
                            userDataList[e.NewIndex].Name = "User " + (e.NewIndex+1).ToString();
                            MessageBox.Show("Такой пользователь уже есть");
                        }
                    }

                    ufc.SaveUsers(userDataList, curUser);
                    break;
            }
        }

        private void MenuItem_ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePaswordWindow changePaswordWindow = new ChangePaswordWindow(ref curUser);
            changePaswordWindow.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(@"Выполнено студентом группы:
ИДБ-18-10, Сливка Н. А.
Разработка программы разграничения полномочий пользователей на основе парольной аутентификации.
Вариант 17: Наличие букв, знаков препинания и знаков арифметических операций.");
        }
    }
}
