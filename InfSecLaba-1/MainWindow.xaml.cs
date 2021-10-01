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
        public MainWindow()
        {
            UsersController u = UsersController.GetInstance();
            u.RestUsers();
            u.AddUser("Nik", "ADMIN", "1234");
            u.AddUser("Joh", "USER", "3352");
            u.AddUser("Lua", "ADMIN", "3452", false);
            u.AddUser("Joh", "USER", "3352");
            u.AddUser("Hah", "USER", "435654");
            u.DeleteUser("Hah");
            u.Users[2].ChangePassword("3452", "1111", "1111");
            u.Save();
            InitializeComponent();
        }
    }
}
