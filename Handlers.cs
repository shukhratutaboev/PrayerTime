using System.Runtime.CompilerServices;
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
            var user = await _storage.GetUserAsync(callbackQuery.Message.Chat.Id);
            if(user.Latitude == 0)
            {
                if(callbackQuery.Data == "uz") user.Language = "uz";

                if(callbackQuery.Data == "ru") user.Language = "ru";

                if(callbackQuery.Data == "en") user.Language = "en";
                await client.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    Language.welcome(user.Language),
                    ParseMode.Markdown,
                    replyMarkup: Buttons.GetLocationButton(user.Language));
            }
            else
            {
                if(callbackQuery.Data == "uz") user.Language = "uz";

                if(callbackQuery.Data == "ru") user.Language = "ru";

                if(callbackQuery.Data == "en") user.Language = "en";
                await client.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    Language.choosenLan(user.Language),
                    ParseMode.Markdown,
                    replyMarkup: Buttons.MenuButtons(user.Language));
            }

            
            await _storage.UpdateUserAsync(user);
            await  client.DeleteMessageAsync(
                callbackQuery.Message.Chat.Id,
                callbackQuery.Message.MessageId
            );

        }

        private async Task BotOnMessageEdited(ITelegramBotClient client, Message editedMessage)
        {
            throw new NotImplementedException();
        }

        private async Task BotOnMessageRecieved(ITelegramBotClient client, Message message)
        {
            if(message.Text == "/start")
            {
                var newuser = new BotUser(
                    chatId: message.Chat.Id,
                    username: message.From.Username,
                    fullname: $"{message.From.FirstName} {message.From.LastName}",
                    longitude: 0,
                    latitude: 0
                );
                var result = await _storage.InsertUserAsync(newuser);
                if(result.IsSuccess)
                {
                    _logger.LogInformation($"New user added: {newuser.ChatID} {newuser.Username}");
                }
                else
                {
                    _logger.LogInformation("User already exists");
                }
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Iltimos tilni tanlang.",
                    ParseMode.Markdown,
                    replyMarkup: Buttons.LanguageButton());
            }
            var user = await _storage.GetUserAsync(message.Chat.Id);
            if(message.Location != null)
            {
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    $"{Language.locationRecieved(user.Language)}",
                    replyToMessageId: message.MessageId,
                    replyMarkup: Buttons.MenuButtons(user.Language)
                );
                user.Longitude = message.Location.Longitude;
                user.Latitude = message.Location.Latitude;
                user.Timezone = await _timings.getTimeZone(user.Longitude, user.Latitude);
                await _storage.UpdateUserAsync(user);
            }
            else
            {
                var text = message.Text;
                if(text == Language.settings(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        Language.settingReply(user.Language),
                        ParseMode.Markdown,
                        replyMarkup: Buttons.SettingsButtons(user.Notifications, user.Language));
                }
                else if(text == Language.today(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        await _timings.getTodayTimings(user.Longitude ,user.Latitude, user.Language),
                        ParseMode.Markdown,
                        replyMarkup: Buttons.MenuButtons(user.Language));
                }
                else if(text == Language.tomorrow(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        await _timings.getTomorrowTimings(user.Longitude, user.Latitude, user.Language ),
                        ParseMode.Markdown,
                        replyMarkup: Buttons.MenuButtons(user.Language));
                }
                else if(text == Language.backToMenu(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        Language.menu(user.Language),
                        ParseMode.Markdown,
                        replyMarkup: Buttons.MenuButtons(user.Language));
                }
                else if(text == Language.turnOnNotf(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        $"{Language.turnedOnReply(user.Language)}{user.setNotification()}",
                        ParseMode.Markdown,
                        replyMarkup: Buttons.SettingsButtons(user.Notifications, user.Language));
                }
                else if(text == Language.turnOfNotf(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        $"{Language.turnedOfReply(user.Language)}{user.setNotification()}",
                        ParseMode.Markdown,
                        replyMarkup: Buttons.SettingsButtons(user.Notifications, user.Language));
                }
                else if(text == Language.nextpt(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        $"{await nextPrayerTime(user)}",
                        ParseMode.Markdown,
                        replyMarkup: Buttons.MenuButtons(user.Language));
                }
                else if(text == Language.changeLanguage(user.Language))
                {
                    await client.SendTextMessageAsync(
                        message.Chat.Id,
                        Language.chooseLan(user.Language),
                        ParseMode.Markdown,
                        replyMarkup: Buttons.LanguageButton());
                }
            }
            if(message.Text == Language.turnOnNotf(user.Language))
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
            string isen = "";
            if(user.Language == "en") isen = "is ";
            if(DateTime.Compare(DateTime.Parse(b.Fajr), now) > 0)
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.fajr(user.Language)}: *{b.Fajr}* {now}";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Dhuhr), now) > 0)
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.dhuhr(user.Language)}: *{b.Dhuhr}*";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Asr), now) > 0)
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.asr(user.Language)}: *{b.Asr}*";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Maghrib), now) > 0)
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.maghrib(user.Language)}: *{b.Maghrib}*";
            }
            else if(DateTime.Compare(DateTime.Parse(b.Isha), now) > 0)
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.isha(user.Language)}: *{b.Isha}*";
            }
            else
            {
                result = $"{Language.nextpt(user.Language)} {isen}{Language.fajr(user.Language)}: *{e.Fajr}*";
            }
            return result;
        }
        private async Task notification(ITelegramBotClient client , BotUser user)
        {   
            var b = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.Day)).Data.Data.Timings;
            while(user.Notifications && !user.OnWhile)
            {
                user.OnWhile = true;
                await _storage.UpdateUserAsync(user);
                
                var e = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.AddDays(1).Day, 0)).Data.Data.Timings;
                var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(user.Timezone));
                await Task.Delay(300000);
                if(await _timings.getWeekday(user.Longitude, user.Latitude) == "Friday")
                {
                    // await client.SendTextMessageAsync(
                    //     user.ChatID,
                    //     Language.juma(user.Language),
                    //     ParseMode.Markdown);
                    await client.SendPhotoAsync(
                        user.ChatID,
                        "https://look.com.ua/pic/201706/1400x1050/look.com.ua-218457.jpg",
                        Language.juma(user.Language));
                }
                if(DateTime.Compare(DateTime.Parse(b.Fajr), now) > 0)
                {
                    var temp = DateTime.Parse(b.Fajr) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.fajr_n(user.Language)} {b.Fajr}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Sunrise), now) > 0)
                {
                    var temp = DateTime.Parse(b.Sunrise) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.sunrise_n(user.Language)}",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Dhuhr), now) > 0)
                {
                    // await Task.Delay(3600000);
                    // await Task.Delay(100000);
                    // if(await _timings.getWeekday(user.Longitude, user.Latitude) == "Friday")
                    // {
                    //     await client.SendTextMessageAsync(
                    //         user.ChatID,
                    //         Language.juma(user.Language),
                    //         ParseMode.Markdown);
                    // }
                    var temp = DateTime.Parse(b.Dhuhr) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.dhuhr_n(user.Language)} {b.Dhuhr}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Asr), now) > 0)
                {
                    var temp = DateTime.Parse(b.Asr) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.asr_n(user.Language)} {b.Asr}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Maghrib), now) > 0)
                {
                    var temp = DateTime.Parse(b.Maghrib) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.maghrib_n(user.Language)} {b.Maghrib}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                else if(DateTime.Compare(DateTime.Parse(b.Isha), now) > 0)
                {
                    var temp = DateTime.Parse(b.Isha) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.isha_n(user.Language)} {b.Isha}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                else
                {
                    b = (await _timings.GetOrUpdateTimingAsync(user.Longitude, user.Latitude, DateTime.UtcNow.AddDays(1).Day, 0)).Data.Data.Timings;
                    var temp = DateTime.Parse(b.Fajr).AddDays(1) - now;
                    await Task.Delay(Math.Abs((int)temp.TotalSeconds * 1000));
                    if(user.Notifications)
                    await client.SendTextMessageAsync(
                        user.ChatID,
                        $"{Language.fajr_n(user.Language)} {b.Fajr}\n \n{Language.arabic()}\n \n{Language.eslatma(user.Language)}",
                        ParseMode.Markdown);
                }
                await Task.Delay(1000000);
                user.OnWhile = false;
                await _storage.UpdateUserAsync(user);
            }
        }
    }
}