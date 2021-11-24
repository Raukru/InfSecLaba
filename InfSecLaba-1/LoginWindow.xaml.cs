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
using System.Windows.Shapes;

namespace InfSecLaba_1
{
    public partial class LoginWindow : Window
    {

        int attempt = 0;
        UserFileController ufc = new UserFileController();
        BindingList<User> users;
        User curUser;

        public LoginWindow()
        {
            users = ufc.LoadAll();
            curUser = null;
            InitializeComponent();
        }

        private void Button_Authorize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pass = textbox_pass.Password;
                string name = textbox_name.Text;
                foreach (User user in users)
                {
                    if (user.Name == name)
                    {
                        if (user.Active == false)
                        {
                            MessageBox.Show("Пользователь заблокирован.");
                            return;
                        }
                        else if (user.Password == "")
                        {
                            MessageBox.Show("Необходимо поменять пароль.");
                            curUser = user;
                            ChangePaswordWindow changePaswordWindow = new ChangePaswordWindow(ref curUser);
                            changePaswordWindow.ShowDialog();
                            return;
                        }
                        
                        else if (user.PassRestrictions && !user.RestrictionsCheck(user.Password))
                        {
                            MessageBox.Show("Необходимо поменять пароль.\nПароль должен содержать хотя бы одну букв, знак препинания и знак арифметической операции");
                            curUser = user;
                            ChangePaswordWindow changePaswordWindow = new ChangePaswordWindow(ref curUser);
                            changePaswordWindow.ShowDialog();
                            return;
                        }
                        else
                        {
                            MainWindow mainWindow = new MainWindow(user.Authorize(pass));
                            mainWindow.Show();
                            this.Close();
                            return;
                        }
                    }
                }
                MessageBox.Show("Пользователь не найден.");
                return;
            }
            catch (PasswordException ex)
            {
                MessageBox.Show(ex.Message);

                attempt++;
                if (attempt >= 3)
                {
                    MessageBox.Show("Пароль введен неверное более трех раз.\nЗавершение работы.");
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Необработанная ошибка:\n" + ex.Message);
            }

        }
    }
}
