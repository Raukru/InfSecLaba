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
    public partial class ChangePaswordWindow : Window
    {
        User curUser;
        UserFileController ufc = new UserFileController();

        public ChangePaswordWindow(ref User user)
        {
            InitializeComponent();
            curUser = user;
            textblock_username.Text = curUser.Name;
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                curUser.ChangePassword(
                    textbox_pass.Password,
                    textbox_pass2.Password,
                    textbox_pass_old.Password);
                ufc.ChangePassword(curUser, textbox_pass.Password);
                Close();
            }
            catch (PasswordException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
