namespace PrayerTime.Services
{
    public class Language
    {
        public static string choosenLan(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "O'zbek tili tanlandi";

            if(lan == "ru")
            result = "Русский язык выбран";

            if(lan == "en")
            result = "English was chosen";

            return result;
        }
        public static string shareLocation(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Joylashuvni aniqlash";

            if(lan == "ru")
            result = "Поделиться местоположением";

            if(lan == "en")
            result = "Share location";

            return result;
        }
        public static string locationRecieved(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Joylashuvingiz qabul qilindi";

            if(lan == "ru")
            result = "Местоположение получено";

            if(lan == "en")
            result = "Location recieved";

            return result;
        }
        public static string welcome(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Xush kelibsiz.\nJoylashuvingizni jo'natmasangiz bot ishlamaydi.";

            if(lan == "ru")
            result = "Добро пожаловать.\nБот не будет работать, если вы не укажете свое местоположение.";

            if(lan == "en")
            result = "Welcome.\nThe bot will not work if you do not provide your location.";

            return result;
        }
        public static string nextpt(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Keyingi namoz vaqti";

            if(lan == "ru")
            result = "Время следующей молитвы";

            if(lan == "en")
            result = "Next prayer time";

            return result;
        }
        public static string settings(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Sozlamalar";

            if(lan == "ru")
            result = "Настройки";

            if(lan == "en")
            result = "Settings";

            return result;
        }
        public static string today(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bugungi namoz vaqtlari";

            if(lan == "ru")
            result = "Сегодняшнее время молитвы";

            if(lan == "en")
            result = "Today's prayer times";

            return result;
        }
        public static string tomorrow(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Ertangi namoz vaqtlari";

            if(lan == "ru")
            result = "Завтрашнее время молитвы";

            if(lan == "en")
            result = "Tomorrow's prayer times";

            return result;
        }
        public static string turnOnNotf(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bildirishnomalarni yoqish";

            if(lan == "ru")
            result = "Включить уведомление";

            if(lan == "en")
            result = "Turn on notification";

            return result;
        }
        public static string turnOfNotf(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bildirishnomalarni o'chirish";

            if(lan == "ru")
            result = "Выключить уведомление";

            if(lan == "en")
            result = "Turn off notification";

            return result;
        }
        public static string setLocation(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Joylashuvni o'zgartirish";

            if(lan == "ru")
            result = "Изменить местоположение";

            if(lan == "en")
            result = "Change location";

            return result;
        }
        public static string changeLanguage(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Tilni o'zgartirish";

            if(lan == "ru")
            result = "Изменить язык";

            if(lan == "en")
            result = "Change language";

            return result;
        }public static string backToMenu(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Menyuga qaytish";

            if(lan == "ru")
            result = "Назад к меню";

            if(lan == "en")
            result = "Back to menu";

            return result;
        }
        public static string settingReply(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Sozlamalar menyusi";

            if(lan == "ru")
            result = "Меню настроек";

            if(lan == "en")
            result = "Settings menu";

            return result;
        }
        public static string turnedOnReply(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bildirishnomalar yoqildi";

            if(lan == "ru")
            result = "Уведомление включено";

            if(lan == "en")
            result = "Notification is turned on";

            return result;
        }
        public static string turnedOfReply(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bildirishnomalar o'chirildi";

            if(lan == "ru")
            result = "Уведомление выключено";

            if(lan == "en")
            result = "Notification is turned off";

            return result;
        }
        public static string menu(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Menyu";

            if(lan == "ru")
            result = "Меню";

            if(lan == "en")
            result = "Menu";

            return result;
        }
        public static string chooseLan(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Tilni tanlang";

            if(lan == "ru")
            result = "Выберите язык";

            if(lan == "en")
            result = "Choose language";

            return result;
        }
        public static string fajr(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bomdod";

            if(lan == "ru")
            result = "Фаджр";

            if(lan == "en")
            result = "Fajr";

            return result;
        }
        public static string sunrise(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Quyosh chiqishi";

            if(lan == "ru")
            result = "Восход";

            if(lan == "en")
            result = "Sunrise";

            return result;
        }
        public static string dhuhr(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Peshin";

            if(lan == "ru")
            result = "Зухр";

            if(lan == "en")
            result = "Dhuhr";

            return result;
        }
        public static string asr(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Asr";

            if(lan == "ru")
            result = "Аср";

            if(lan == "en")
            result = "Asr";

            return result;
        }
        public static string maghrib(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Shom";

            if(lan == "ru")
            result = "Магриб";

            if(lan == "en")
            result = "Maghrib";

            return result;
        }
        public static string isha(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Xufton";

            if(lan == "ru")
            result = "Иша";

            if(lan == "en")
            result = "Isha";

            return result;
        }
        public static string juma(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Juma muborak.";

            if(lan == "ru")
            result = "Пятница";

            if(lan == "en")
            result = "Friday";

            return result;
        }
    }
}
