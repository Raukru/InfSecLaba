using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InfSecLaba_1
{
        class UsersController
    {
        public IList<User> Users { get; set; }
        public static string UsersFilePath = "Users.txt";

        private UsersController()
        {
            // TODO: При первом запуске должен создаваться файл с админом
            Users = LoadUsers();
        }

        private static UsersController instance;

        public static UsersController GetInstance()
        {
            if(instance == null)
            {
                instance = new UsersController();
            }
            return instance;
        }

        /// <summary>
        /// Добавляет нового пользователя в список пользователей и в файл.
        /// </summary>
        /// <param name="name"> Имя пользователя </param>
        /// <param name="role"> Роль пользоателя: ADMIN, USER </param>
        /// <param name="password"> Пароль пользователя </param>
        /// <param name="active"> Если true - значит пользователь не заблокирован </param>
        public bool AddUser(string name,
                            string role = "USER",
                            string password = "",
                            bool active = true,
                            bool restrictions = true
                            )
        {
            User newUser = new User
            {
                Name = name,
                Role = role,
                Password = password,
                Active = active,
                PassRestrictions = restrictions
            };

            foreach(User user in Users)
            {
                if(user.Name == newUser.Name)
                {
                    return false;
                }
            }
            
            Users.Add(newUser);
            Save();
            return true;

        }

        /// <summary>
        /// Удаляет пользователя по имени
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        public bool DeleteUser(string name)
        {
            foreach(User u in Users)
            {
                if (u.Name == name)
                {
                    Users.Remove(u);
                    Save();
                    return true;
                }   
            }
            return false;
        }

        /// <summary>
        /// Сохраняет на диск пользователей
        /// </summary>
        public void Save()
        {
            JArray jArray = new JArray(
                Users.Select(u => new JObject
                {
                    { "Name", u.Name },
                    { "Role", u.Role },
                    { "Password", u.Password },
                    { "Active", u.Active },
                })
            );
            File.WriteAllText(UsersFilePath, jArray.ToString());
        }

        /// <summary>
        /// Загружает пользователей с диска
        /// </summary>
        /// <returns>Возвращает список пользователей</returns>
        public static IList<User> LoadUsers()
        {
            JArray jArray;
            using (StreamReader file = File.OpenText(UsersFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jArray = (JArray)JToken.ReadFrom(reader);
            }

            IList<User> Users = jArray.ToObject<IList<User>>();
            return Users;
        }

        public void RestUsers()
        {
            Users.Clear();
            File.WriteAllText(UsersFilePath, "[]");
        }
    }
}
