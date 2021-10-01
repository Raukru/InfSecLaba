using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfSecLaba_1
{
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        protected static string json_path = "Users.txt";

        public User()
        {
            Name = null;
            Role = null;
            Password = null;
            Active = false;
        }

        public User(string name,
                    string role,
                    string password,
                    bool active = true)
        {
            Name = name;
            Role = role;
            Password = password;
            Active = active;
        }

        // TODO: Доделать авторизацию
        public bool Authorize(string password)
        {
            if(password == Password)
            {
                return true;
            }
            return false;
        }

        public bool ChangePassword(string old_password, string new_password1, string new_password2)
        {
            // TODO: Нужно сделать взятие пароля из User.txt
            if (old_password == Password)
            {
                if (new_password1 == new_password2)
                {
                    Password = new_password1;
                    return true;
                }
            }
            return false;
        }
    }
}
