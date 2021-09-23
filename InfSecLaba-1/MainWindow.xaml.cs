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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InfSecLaba_1
{
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        private static string json_path = "Users.txt";

        public User()
        {
            Name = null;
            Role = null;
        }

        public User(string name)
        {
            using (StreamReader file = File.OpenText(json_path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject jObject = ((JObject)JToken.ReadFrom(reader));
                // TODO: Нужно доделать конструктор. Взятие из User.txt имени и прочего. Сделать этот конструктор только для чтения
            }

            // TODO: Добавить конструктор - для входа в учетку (имя, пароль)
        }

        // TODO: Нужно сделать запись пароля в json формате и избавиться от переменной пароля!!
        public void ChangePassword(string old_password, string new_password1, string new_password2)
        {
            // TODO: Нужно сделать взятие пароля из User.txt
            string password = "";
            if (old_password == password)
            {
                if (new_password1 == new_password2)
                {
                    // password = new_password1;
                }
                else
                {
                    // INFO: passwords dont match
                }
            }
            else
            {
                // INFO: invalide old password
            }
        }

        /// <summary>
        /// Добавляет нового пользователя в json формате в файл с пользователями.
        /// </summary>
        /// <param name="name"> Имя пользователя </param>
        /// <param name="role"> Роль пользоателя: ADMIN, USER </param>
        /// <param name="password"> Пароль пользователя </param>
        /// <param name="active"> Если true - значит пользователь не заблокирован </param>
        public void AddUser(string name, 
                            string role,
                            string password,
                            bool active=true
                            )
        {
            JObject jObject = new JObject();
            jObject.Add(name, new JObject()
            {
                { "Role",  role},
                { "Password", password },
                { "Active", active }
            });

            if (File.Exists(json_path))
            {
                using (StreamWriter file = File.AppendText(json_path))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    jObject.WriteTo(writer);
                }
            }
            else
            {
                // TODO: Должен ли что-то делать если не существует?
            }
        }

        // TODO: Возможно нужен отдельный метод для открытия файла (В лабе 3 туда можно добавить шифрование)
        // TODO: Возможно нужен отдельный метод для работы с json (сделает независимым работу с json от файла на диске (при шифровке))

        public void ResetUserFile()
        {
            using (File.CreateText(json_path));
        }

        // TODO: Блокировка другого пользователя
        public void BlockUser()
        {

        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            User u = new User();
            u.ResetUserFile();
            u.AddUser("Nik", "ADMIN", "1234");
            u.AddUser("Joh", "USER", "324352");
            User user = new User("Nik");
            InitializeComponent();
        }
    }
}
