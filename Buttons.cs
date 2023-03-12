using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using PrayerTime.Services;

namespace PrayerTimeBot
{
    public class Buttons
    {
        public static IReplyMarkup LanguageButton()
        {
            return new InlineKeyboardMarkup(
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "O'zbek tili🇺🇿", "uz"),
                    InlineKeyboardButton.WithCallbackData(text: "Русский🇷🇺", "ru"),
                    InlineKeyboardButton.WithCallbackData(text: "English🇺🇸", "en")
                }
            );
        }
        public static IReplyMarkup GetLocationButton(string lan)
        {
            return new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>
                {
                    new KeyboardButton(Language.shareLocation(lan)) { RequestLocation = true }
                }
            })
            {
                ResizeKeyboard = true
            };
        }
        
        public static IReplyMarkup MenuButtons(string lan)
        {
        return new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
        {
            new List<KeyboardButton>{ new KeyboardButton(Language.today(lan)), new KeyboardButton(Language.tomorrow(lan))},
            new List<KeyboardButton>{ new KeyboardButton(Language.nextpt(lan)), new KeyboardButton(Language.settings(lan))}
        })
        {
            ResizeKeyboard = true
        };
}
        public static IReplyMarkup SettingsButtons(bool notifications, string lan)
        {
            string notificationsButton;
            if(notifications)
            {
                notificationsButton = Language.turnOfNotf(lan);
            }
            else
            {
                notificationsButton = Language.turnOnNotf(lan);
            }
            return new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{ new KeyboardButton(Language.setLocation(lan)) {RequestLocation = true }, new KeyboardButton(notificationsButton)},
                new List<KeyboardButton>{ new KeyboardButton(Language.changeLanguage(lan)), new KeyboardButton(Language.backToMenu(lan))}
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}