using System.Collections.Generic;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrayerTime
{
    class Program
    {
        private static string token = "2031388312:AAF6gkicS_0FSHLsWJ_xjNy5Cz__R5L-EHg";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StartReceiving();
        }
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if(msg.Text != null)
            {
                Console.WriteLine($"Salom {msg.Text}");
                // await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
                switch (msg.Text)
                {
                    case "/start":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Botga xush kelibsiz.",
                            replyMarkup: StartButtons());
                        break;
                    case "Back to menu":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Menu",
                            replyMarkup: StartButtons());
                            break;
                    case "Today":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Bu yerda qanaqadir ma'noli gap bo'lishi mumkin edi.");
                        break;
                    case "Tomorrow":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Ertangi kunning namoz vaqtlari");
                        break;
                    case "Settings":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Settings",
                            replyMarkup: SettingsButtons());
                        break;
                    default:
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            "Boshqa tugmani bosing.");
                        break;
                };
            }
        }
        private static IReplyMarkup StartButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Today"}, new KeyboardButton {Text = "Tomorrow"}},
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Next prayer time"}, new KeyboardButton {Text = "Settings"}}
                }
            };
        }
        private static IReplyMarkup SettingsButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Reset location"}, new KeyboardButton {Text = "Turn on notifications"}},
                    new List<KeyboardButton>{ new KeyboardButton {Text = "Change language"}, new KeyboardButton {Text = "Back to menu"}}
                }
            };
        }

    }
}
