using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PrayerTime
{
    public class Handlers
    {
        private static ILogger<Handlers> _logger;
        private static float _longitude;
        private static float _latitude;

        public static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ctoken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException => $"Error occured with Telegram Client: {exception.Message}",
                _ => exception.Message
            };
            _logger.LogCritical(errorMessage);
            return Task.CompletedTask;
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ctoken)
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
        }

        private static async Task UnknownUpdateHandlerAsync(ITelegramBotClient client, Update update)
        {
            throw new NotImplementedException();
        }

        private static async Task BotOnChosenInlineResultReceived(ITelegramBotClient client, ChosenInlineResult chosenInlineResult)
        {
            throw new NotImplementedException();
        }

        private static async Task BotOnInlineQueryReceived(ITelegramBotClient client, InlineQuery inlineQuery)
        {
            throw new NotImplementedException();
        }

        private static async Task BotOnCallbackQueryReceived(ITelegramBotClient client, CallbackQuery callbackQuery)
        {
            throw new NotImplementedException();
        }

        private static async Task BotOnMessageEdited(ITelegramBotClient client, Message editedMessage)
        {
            throw new NotImplementedException();
        }

        private static async Task BotOnMessageRecieved(ITelegramBotClient client, Message message)
        {
            if(message.Location != null)
            {
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Location successfully accepted",
                    replyToMessageId: message.MessageId,
                    replyMarkup: Buttons.MenuButtons()
                );
                _longitude = message.Location.Longitude;
                _latitude = message.Location.Latitude;
                Console.WriteLine($"{_latitude} {_longitude} from @{message.From.Username}");
            }
            var a = message.Text switch
            {
                "/start"    => await client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Botga xush kelibsiz.",
                                replyMarkup: Buttons.GetLocationButton()),
                "Settings"  => await client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Settings",
                                replyMarkup: Buttons.SettingsButtons()),
                "Back to menu" => await client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Back to menu",
                                replyMarkup: Buttons.MenuButtons()),
                _           => await client.SendTextMessageAsync(
                                message.Chat.Id,
                                "Hozircha shu.")
            };
        }
    }
}