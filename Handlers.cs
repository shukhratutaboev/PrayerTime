using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PrayerTimeBot.Services;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using PrayerTimeBot.Entity;
using PrayerTime.Services;

namespace PrayerTimeBot
{
    public class Handlers
    {
        private readonly ILogger<Handlers> _logger;
        private readonly IStorageService _storage;
        private readonly TimingsByLLCache _timings;
        public Handlers(ILogger<Handlers> logger, IStorageService storage, TimingsByLLCache timings)
        {
            _logger = logger;
            _storage = storage;
            _timings = timings;
        }

        public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ctoken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException => $"Error occured with Telegram Client: {exception.Message}",
                _ => exception.Message
            };
            _logger.LogCritical(errorMessage);
            return Task.CompletedTask;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ctoken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageRecieved(client, update.Message),
                UpdateType.EditedMessage => BotOnMessageEdited(client, update.EditedMessage),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(client, update.CallbackQuery),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(client, update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(client, update.ChosenInlineResult),
                _ => UnknownUpdateHandlerAsync(client, update)
            };
            try
            {
                await handler;
            }
            catch(Exception e)
            {
                _logger.LogWarning(e.Message);
            }
        }

        private async Task UnknownUpdateHandlerAsync(ITelegramBotClient client, Update update)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnChosenInlineResultReceived(ITelegramBotClient client, ChosenInlineResult chosenInlineResult)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnInlineQueryReceived(ITelegramBotClient client, InlineQuery inlineQuery)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnCallbackQueryReceived(ITelegramBotClient client, CallbackQuery callbackQuery)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnMessageEdited(ITelegramBotClient client, Message editedMessage)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnMessageRecieved(ITelegramBotClient client, Message message)
        {
            if(message.Text == "/start")
            {
                var user = new BotUser(
                    chatId: message.Chat.Id,
                    username: message.From.Username,
                    fullname: $"{message.From.FirstName} {message.From.LastName}",
                    longitude: 0,
                    latitude: 0
                );
                var result = await _storage.InsertUserAsync(user);
                if(result.IsSuccess)
                {
                    _logger.LogInformation($"New user added: {user.ChatID} {user.Username}");
                }
                else
                {
                    _logger.LogInformation("User already exists");
                }
            }
            if(message.Location != null)
            {
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Lokatsiyangiz muvaffaqiyatli qabul qilindi",
                    replyToMessageId: message.MessageId,
                    replyMarkup: Buttons.MenuButtons()
                );
                var user = await _storage.GetUserAsync(message.Chat.Id);
                user.Longitude = message.Location.Longitude;
                user.Latitude = message.Location.Latitude;
                user.Timezone = await _timings.getTimeZone(user.Longitude, user.Latitude);
                await _storage.UpdateUserAsync(user);
            }
            else
            {
                var _user = await _storage.GetUserAsync(message.Chat.Id);
                var a = message.Text switch
                {
                    "/start"    => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    "Botga xush kelibsiz.\nLokatsiyangizni jo'natmasangiz bot ishlamaydi.",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.GetLocationButton()),
                    "Sozlamalar"  => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    "Sozlamalardan birini tanlang",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.SettingsButtons(_user.Notifications)),
                    "Bugungi namoz vaqtlari"     => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    await _timings.getTodayTimings(_user.Longitude ,_user.Latitude),
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.MenuButtons()),
                    "Ertangi namoz vaqtlari"  => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    await _timings.getTomorrowTimings(_user.Longitude, _user.Latitude, _user.Timezone ),
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.MenuButtons()),
                    "Menyuga qaytish" => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    "Back to menu",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.MenuButtons()),
                    "Bildirishnomalarni yoqish" => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    $"Bildirishnomalar yoqildi{_user.setNotification()}",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.SettingsButtons(_user.Notifications)),
                    "Bildirishnomalarni o'chirish" => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    $"Bildirishnomalar o'chirildi{_user.setNotification()}",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.SettingsButtons(_user.Notifications)),
                    "Keyingi namoz vaqti" => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    $"{await nextPrayerTime(_user)}",
                                    ParseMode.Markdown,
                                    replyMarkup: Buttons.MenuButtons()),
                    _           => await client.SendTextMessageAsync(
                                    message.Chat.Id,
                                    $"Hozircha shu.\n{message.Date.ToLocalTime()} m and {DateTime.UtcNow} utc and {DateTime.Now} now",
                                    ParseMode.Markdown)
                };
            }
            if(message.Text == "Bildirishnomalarni yoqish")
            {
                var _user = await _storage.GetUserAsync(message.Chat.Id);
                notification(client, _user);
            }
        }
        private async Task<string> nextPrayerTime(BotUser user)
        {

            var b = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.Day)).Data.Data.Timings;
            var e = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.AddDays(1).Day, 0)).Data.Data.Timings;
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(user.Timezone));
            string result;
            if(DateTime.Compare(DateTime.Parse(b.Fajr), now) > 0)
            {
                result = $"Keyingi namoz vaqti Bomdod: {b.Fajr} {now}";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Dhuhr), now) > 0)
            {
                result = $"Keyingi namoz vaqti Peshin: {b.Dhuhr}";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Asr), now) > 0)
            {
                result = $"Keyingi namoz vaqti Asr: {b.Asr}";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Maghrib), now) > 0)
            {
                result = $"Keyingi namoz vaqti Shom: {b.Maghrib}";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Isha), now) > 0)
            {
                result = $"Keyingi namoz vaqti Xufton: {b.Isha}";
            }
            else
            {
                result = $"Keyingi namoz vaqti Bomdod: {e.Fajr}";
            }
            return result;

        }
        private async Task notification(ITelegramBotClient client , BotUser user)
        {
            // Console.WriteLine(await _timings.getWeekday(user.Longitude, user.Latitude));
            
            var b = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.Day)).Data.Data.Timings;
            while(user.Notifications && !user.OnWhile)
            {
                user.OnWhile = true;
                await _storage.UpdateUserAsync(user);
                
                var e = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.AddDays(1).Day, 0)).Data.Data.Timings;
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(user.Timezone));
                if(DateTime.Compare(DateTime.Parse(b.Fajr), now) > 0)
                {
                    var temp = DateTime.Parse(b.Fajr) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        "Bomdod vaqti kirdi",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Sunrise), now) > 0)
                {
                    var temp = DateTime.Parse(b.Sunrise) - now;
                    // Thread.Sleep(Math.Abs((int)temp.TotalSeconds * 1000));
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        "Quyosh chiqdi",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Dhuhr), now) > 0)
                {
                    // Thread.Sleep(3600000);
                    await Task.Delay(3600000);
                    if(await _timings.getWeekday(user.Longitude, user.Latitude) == "Friday")
                    {
                        await client.SendTextMessageAsync(
                            user.ChatID,
                            "Juma",
                            ParseMode.Markdown);
                    }
                    var temp = DateTime.Parse(b.Dhuhr) - now;
                    // Thread.Sleep(Math.Abs((int)temp.TotalSeconds * 1000));
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        "Peshin.",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Asr), now) > 0)
                {
                    var temp = DateTime.Parse(b.Asr) - now;
                    // Thread.Sleep(Math.Abs((int)temp.TotalSeconds * 1000));
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        "Asr.",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Maghrib), now) > 0)
                {
                    var temp = DateTime.Parse(b.Maghrib) - now;
                    // Thread.Sleep(Math.Abs((int)temp.TotalSeconds * 1000));
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        "Shom",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Isha), now) > 0)
                {
                    var temp = DateTime.Parse(b.Isha) - now;
                    // Thread.Sleep(1000/*Math.Abs((int)temp.TotalSeconds * 1000)*/);
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"Xufton vaqti kirdi: {b.Isha}",
                        ParseMode.Markdown);
                }
                else
                {
                    b = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.AddDays(1).Day, 0)).Data.Data.Timings;
                    var temp = DateTime.Parse(b.Fajr).AddDays(1) - now;
                    // Thread.Sleep(Math.Abs((int)temp.TotalSeconds * 1000));
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"Bomdod.{temp}",
                        ParseMode.Markdown);
                }
                // Thread.Sleep(1000/*1000000*/);
                await Task.Delay(1000000);
                user.OnWhile = false;
                await _storage.UpdateUserAsync(user);
            }
        }
    }
}