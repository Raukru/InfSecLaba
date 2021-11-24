using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace InfSecLaba_1
{
    
    class UserFileController
    {
        public static string UsersFilePath = "Users.txt";

        public UserFileController() {  }

        /// <summary>
        /// Сохраняет на диск пользователей
        /// </summary>
        public void SaveUsers(BindingList<User> dataList, User curUser)
        {
            List<User> users = dataList.ToList();
            users.Insert(0, curUser);
            string outputData = JsonConvert.SerializeObject(users);
            
            File.WriteAllText(UsersFilePath, outputData);
            users.RemoveAt(0);
        }
        public void SaveUsers(BindingList<User> dataList)
        {
            string outputData = JsonConvert.SerializeObject(dataList);

            File.WriteAllText(UsersFilePath, outputData);
        }

        /// <summary>
        /// Загружает пользователей с диска
        /// </summary>
        /// <returns>Возвращает список только пользователей</returns>
        public BindingList<User> LoadOnlyUsers()
        {
            var fileExists = File.Exists(UsersFilePath);
            if (!fileExists)
            {
                RestUsers();
            }
            JArray jArray;
            using (StreamReader file = File.OpenText(UsersFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jArray = (JArray)JToken.ReadFrom(reader);
            }

            BindingList<User> users = jArray.ToObject<BindingList<User>>();
            users.RemoveAt(0);
            return users;
        }

        /// <summary>
        /// Загружает всех пользователей и администратора
        /// </summary>
        /// <returns> Возвращает список пользователей и администратора</returns>
        public BindingList<User> LoadAll()
        {
            var fileExists = File.Exists(UsersFilePath);
            if (!fileExists)
            {
                RestUsers();
            }
            JArray jArray;
            using (StreamReader file = File.OpenText(UsersFilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jArray = (JArray)JToken.ReadFrom(reader);
            }

            BindingList<User> users = jArray.ToObject<BindingList<User>>();
            return users;
        }

        /// <summary>
        /// Создает/перезаписывает файл с пользователями
        /// </summary>
        public void RestUsers()
        {
            string file_text = "[" + JsonConvert.SerializeObject(new User("admin", "ADMIN", "", true, true)) + "]";
            File.WriteAllText(UsersFilePath, file_text);
        }

        /// <summary>
        /// Меняет пароль пользователю на диске
        /// </summary>
        /// <param name="newUser">имя пользователя</param>
        /// <param name="pass">новый пароль</param>
        public void ChangePassword(User newUser, string pass)
        {
            BindingList<User> users = LoadAll();
            foreach(User user in users)
            {
                if (newUser.Name == user.Name)
                {
                    user.Password = pass;
                    SaveUsers(users);
                    return;
                }
            }

            throw new Exception("Пользователь не найден");
        }
    }
}
