namespace PrayerTime.Services
{
    public class Language
    {
        public static string choosenLan(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "O'zbek tili tanlandi🇺🇿";

            if(lan == "ru")
            result = "Русский язык выбран🇷🇺";

            if(lan == "en")
            result = "English was chosen🇺🇸";

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
            {
                result = "Juma muborak.\n\nAbu Hurayra (r.a.) rivoyat qiladilar: “Rasululloh sollallohu alayhi vasallam aytdilar:\n\n";
                result += "“Quyosh chiqqan kunning eng yaxshisi juma kunidir; Uning ustida Odam yaratildi va uning ustida jannatga kiritildi.\n";
                result += "Juma kuni ham Odam alayhissalom jannatdan quvildilar.\n";
                result += "Va (oxirgi) soat (ya’ni qiyomat) juma kunidan boshqa hech bir kunda bo‘lmaydi”. (Muslim)";
            }

            if(lan == "ru")
            {
                result = "Джума мубарак.\n\nСо слов Абу Хурайры передается, что Посланник Аллаха (мир ему и благословение Аллаха) сказал:\n\n";
                result += "«Лучший день для восхода солнца - пятница; На нем был создан Адам, и он был взят на небеса.\n";
                result += "В пятницу Адам был изгнан из Рая.\n";
                result += "И Час (Суда) не будет ни в один день, кроме пятницы ». (Муслим)";
            }

            if(lan == "en")
            {
                result = "Juma mubarak.\n\nAbu Hurairah (may Allah be pleased with him) reported that the Prophet (peace and blessings be upon him) said:\n\n";
                result += "“The best day on which the sun has risen is Friday; on it Adam was created and on it he was made to enter Paradise.\n";
                result += "On Friday, also, Adam was expelled from Paradise.\n";
                result += "And the [last] hour (i.e. the Day of Resurrection) will take place on no day other than Friday.” (Muslim)";
            }
            return result;
        }
        public static string fajr_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Bomdod namozining vaqti kirdi:";

            if(lan == "ru")
            result = "Пришло время для молитвы Фаджр:";

            if(lan == "en")
            result = "It is to pray Fajr:";

            return result;
        }
        public static string sunrise_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Quyosh chiqdi!";

            if(lan == "ru")
            result = "Вышло солнце!";

            if(lan == "en")
            result = "The sun came out!";

            return result;
        }
        public static string dhuhr_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Peshin namozining vaqti kirdi:";

            if(lan == "ru")
            result = "Пришло время для молитвы Зухр:";

            if(lan == "en")
            result = "It is to pray Dhuhr:";

            return result;
        }
        public static string asr_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Asr namozining vaqti kirdi:";

            if(lan == "ru")
            result = "Пришло время для молитвы Аср:";

            if(lan == "en")
            result = "It is to pray Asr:";

            return result;
        }
        public static string maghrib_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Shom namozining vaqti kirdi:";

            if(lan == "ru")
            result = "Пришло время для молитвы Магриб:";

            if(lan == "en")
            result = "It is to pray Maghrib:";

            return result;
        }
        public static string isha_n(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "Xufton namozining vaqti kirdi:";

            if(lan == "ru")
            result = "Пришло время для молитвы Иша:";

            if(lan == "en")
            result = "It is to pray Isha:";

            return result;
        }
        public static string eslatma(string lan)
        {
            string result = null;
            if(lan == "uz")
            result = "“...Albatta, namoz mo‘minlarga belgilangan soatlarda farz qilingandir”, deyiladi (Niso surasi, 103-oyat).";

            if(lan == "ru")
            result = "“Воистину, намаз предписан верующим в определенное время”. (4:103)";

            if(lan == "en")
            result = "“…Verily, the prayer is enjoined on the believers at fixed hours” says another verse (4:103).";

            return result;
        }
        public static string arabic()
        {
            return "فَإِذَا قَضَيْتُمُ الصَّلَاةَ فَاذْكُرُوا اللَّهَ قِيَامًا وَقُعُودًا وَعَلَىٰ جُنُوبِكُمْ ۚ فَإِذَا اطْمَأْنَنْتُمْ فَأَقِيمُوا الصَّلَاةَ ۚ إِنَّ الصَّلَاةَ كَانَتْ عَلَى الْمُؤْمِنِينَ كِتَابًا مَوْقُوتًا";
        }
    }
}
