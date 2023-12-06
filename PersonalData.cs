using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;

namespace Pizza
{
    internal static class PersonalData
    {
        public static string nameUser = string.Empty;
        public static string phoneNumber = string.Empty;
        public static string email = string.Empty;
        public static string data = string.Empty;
        public static string login = string.Empty;
        public static string password = string.Empty;

        public static void Save()
        {
            Preferences.Set("name",nameUser);
            Preferences.Set("phone",phoneNumber);
            Preferences.Set("email",email);
            Preferences.Set("data",data);
            Preferences.Set("login",login);
            Preferences.Set("passwd",password);
        }

        public static void Load() 
        {
            nameUser = Preferences.Get("name",nameUser);
            phoneNumber = Preferences.Get("phone", phoneNumber);
            email = Preferences.Get("email", email);
            data = Preferences.Get("data", data);
            login = Preferences.Get("login", login);
            password = Preferences.Get("passwd", password);
        }
    }
}
