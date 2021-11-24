using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;


namespace InfSecLaba_1
{
    public class User : INotifyPropertyChanged
    {
        private string _name = "";
        private bool _active = true;
        private bool _passRestrictions = true;

        public string Role { get; set; } = "USER";
        public string Password { get; set; } = "";
        public string Name
        {
            get { return _name; }
            set {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value)
                    return;
                _active = value;
                OnPropertyChanged("Active");
            }
        }
        public bool PassRestrictions
        {
            get { return _passRestrictions; }
            set
            {
                if (_passRestrictions == value)
                    return;
                _passRestrictions = value;
                OnPropertyChanged("PassRestrictions");
            }
        }

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

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Возвращает ссылку на текущего пользователя</returns>
        public User Authorize(string password)
        {
            if(password == Password)
            {
                if (Active == true)
                    return this;
            }
            throw new PasswordException("Неправильный пароль.");
        }

        /// <summary>
        /// Проверяет пароль пользователя
        /// </summary>
        /// <param name="new_password1"></param>
        /// <param name="new_password2"></param>
        /// <param name="old_password"></param>
        public void ChangePassword(string new_password1, string new_password2, string old_password = "")
        {
            if (old_password == Password)
            {
                if (PassRestrictions)
                {
                    if (RestrictionsCheck(new_password1))
                    {
                        if (new_password1 == new_password2)
                        {
                            Password = new_password1;
                            return;
                        }
                        else
                        {
                            throw new PasswordException("Пароли не совпадают.");
                        }
                    }
                    else
                    {
                        throw new PasswordException("Пароль должен содержать хотя бы одну букв, знак препинания и знак арифметической операции.");
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
                        throw new PasswordException("Пароли не совпадают.");
                    }
                }
            }
            else
            {
                throw new PasswordException("Неправильный старый пароль.");
            }
        }

        /// <summary>
        /// Проверяет пароль на обязательные символя
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool RestrictionsCheck(string pass)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
