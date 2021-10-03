using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfSecLaba_1
{
    public class User
    {
        public string Name { get; set; } = null;
        public string Role { get; set; } = null;
        public string Password { get; set; } = null;
        public bool Active { get; set; } = false;
        public bool PassRestrictions { get; set; } = true;

        private string letteSymbols = "qwertyuiopasdfghjklzxcvbnm";
        private string punctuationSymbols = ".,!?;\"():";
        private string mathSymbols = "-+/*=";

        public User() { }

        public User(string name,
                    string role,
                    string password,
                    bool active,
                    bool restrictions)
        {
            Name = name;
            Role = role;
            Password = password;
            Active = active;
            PassRestrictions = restrictions;
        }

        public User Authorize(string password)
        {
            if(password == Password)
            {
                return this;
            }
            throw new PasswordException("Неправильный пароль");
        }

        public void ChangePassword(string new_password1, string new_password2, string old_password = "")
        {
            if (old_password == Password)
            {
                if (PassRestrictions)
                {
                    if (restrictionsCheck(new_password1))
                    {
                        if (new_password1 == new_password2)
                        {
                            Password = new_password1;
                            return;
                        }
                        else
                        {
                            throw new PasswordException("Парольи не совпадают");
                        }
                    }
                    else
                    {
                        throw new PasswordException("Пароль должен содержать хотя бы одну букв, знак препинания и знак арифметическую операций");
                    }
                }
                else
                {
                    if (new_password1 == new_password2)
                    {
                        Password = new_password1;
                        return;
                    }
                    else
                    {
                        throw new PasswordException("Парольи не совпадают");
                    }
                }
            }
            else
            {
                throw new PasswordException("Неправильный старый пароль");
            }
        }

        private bool restrictionsCheck(string pass)
        {
            var passCharArray = pass.ToCharArray();
            bool IsPunctuationSymbol = passCharArray.Any(ch => punctuationSymbols.Contains(ch));
            bool IsMathSymbol = passCharArray.Any(ch => mathSymbols.Contains(ch));
            bool IsLetteSymbol = passCharArray.Any(ch => letteSymbols.Contains(ch));
            if (IsPunctuationSymbol && IsMathSymbol && IsLetteSymbol)
            {
                return true;
            }
            return false;
        }
    }
}
