using System.ComponentModel.DataAnnotations;
using System;

namespace PrayerTimeBot.Entity
{
    public class BotUser
    {
        [Key]
        public long ChatID {get; set;}
        public string Username {get; set;}
        public string Fullname {get; set;}
        public float Longitude {get; set;}
        public float Latitude {get; set;}
        public bool Notifications {get; set;}
        public string Timezone { get; set; }
        public bool OnWhile { get; set; }
        public string Language {get; set;}
        
        [Obsolete("Used only for Entity binding!")]
        public BotUser() {}

        public BotUser(long chatId, string username, string fullname, float longitude, float latitude)
        {
            ChatID = chatId;
            Username = username;
            Fullname = fullname;
            Longitude = longitude;
            Latitude = latitude;
            Notifications = false;
            Timezone = null;
            OnWhile = false;
            Language = null;
        }
        public string setNotification()
        {
            if(Notifications)
            {
                Notifications = false;
            }
            else
            {
                Notifications = true;
            }
            return null;
        }
        public void setLanguage(string l)
        {
            if(l == "uz")
            Language = "uz";

            if(l == "ru")
            Language = "ru";

            if(l == "en")
            Language = "en";
        }
    }
}