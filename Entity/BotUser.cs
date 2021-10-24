using System.ComponentModel.DataAnnotations;
using System;

namespace PrayerTime.Entity
{
    public class BotUser
    {
        [Key]
        public long ChatID {get; set;}
        public string Username {get; set;}
        public string Fullname {get; set;}
        public double Longitude {get; set;}
        public double Latitude {get; set;}
        
        [Obsolete("Used only for Entity binding!")]
        public BotUser() {}

        public BotUser(long chatId, string username, string fullname, double longitude, double latitude)
        {
            ChatID = chatId;
            Username = username;
            Fullname = fullname;
            Longitude = longitude;
            Latitude = latitude;
        }
        
    }
}