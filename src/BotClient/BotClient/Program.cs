using APIForBD.Contracts.Product;
using APIForBD.Contracts.User;
using Newtonsoft.Json;
using System.Net.Http;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;

namespace BotClient
{
    internal class Program
    {

        static async Task Main(string[] args)
        {

            var botClient = new TelegramBotClient("6847645908:AAGUL0u_Y12vFRa1sxR_QpVZhFor3mZsg_A");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );


            var me = await botClient.GetMeAsync();

            /*HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://localhost:7098/api/Users");
            Console.WriteLine(result);

            var test = await result.Content.ReadAsStringAsync();
            Console.WriteLine(test);

            GetUserResponse[] users = JsonConvert.DeserializeObject<GetUserResponse[]>(test);

            foreach (var u in users)
            {
                Console.WriteLine(u.UserId + " " + u.Username);
            }*/


            Console.WriteLine($"{me} is working");

            Console.ReadLine();

            cts.Cancel();
        }
        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;

        }


        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Type == UpdateType.Message)
            {

                var message = update.Message;
                var messageText = message.Text;

                var chatId = message.Chat.Id;

                Console.WriteLine($"Recieved a '{messageText}' message in chat {chatId}.");





                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said: \n" + messageText,
                    cancellationToken: cancellationToken);

                if (message.Text == "Проверка")
                {
                    await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Проверка: ОК!",
                    cancellationToken: cancellationToken);
                }



                if (message.Text == "Привет")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Здраствуй,Андроед ",
                        cancellationToken: cancellationToken);
                }



                if (message.Text == "Картинка")
                {
                    await botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: InputFile.FromUri("https://i.ytimg.com/vi/7Ogiya7HZCE/maxresdefault.jpg"),
                        cancellationToken: cancellationToken);
                }

                if (message.Text == "Видео")
                {
                    await botClient.SendVideoAsync(
                        chatId: chatId,
                        video: InputFile.FromUri("https://dt163.dlsnap12.xyz/download?file=MDYwOTlmNDk4ODlkNjY0NTM4Yzc5MTBiODcyZDc5YzAxNDY1MmZhYjk1MTQ2OWUzOTY1YTE5NzM2OWI5NjQ2ZV80ODBwLm1wNOKYr1kybWF0ZS5teC3QotCw0L3QtdGGINC00YDQsNC60L7QvdCwINC80LXQvCB8IFRvb3RobGVzcyBkYW5jZSBtZW1l4pivNDgwcA"),
                        cancellationToken: cancellationToken);
                }



                if (message.Text == "Стикер")
                {
                    await botClient.SendStickerAsync(
                        chatId: chatId,
                        sticker: InputFile.FromString("CAACAgIAAxkBAAI7f2WvrH-WWBiJ3ygo8ilcW89uwvuwAALTFAACdogpS9j_3IjCnFTyNAQ"),
                        cancellationToken: cancellationToken);
                }

                if (message.Text == "Кнопка")
                {
                    var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                          new KeyboardButton("Арбуз"),
                          new KeyboardButton("Дыня"),
                          new KeyboardButton("Банан")

                        }
                    });
                    await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите один из вариантов:",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                }
                if (message.Text == "Арбуз" || message.Text == "Дыня" || message.Text == "Банан")
                {
                    var replyKeyboardRemove = new ReplyKeyboardRemove();

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вы сделали свой выбор!",
                        replyMarkup: replyKeyboardRemove,
                        cancellationToken: cancellationToken);
                }




                if (message.Text == "БД")
                {
                    var keyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("Пользователи") },
                        new[] { new KeyboardButton("Продукты") },
                    })
                    {
                        ResizeKeyboard = true
                    };

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите действие:",
                        replyMarkup: keyboard,
                        cancellationToken: cancellationToken);
                }
                else if (message.Text == "Пользователи")
                {
                    await GetUsersAsync(botClient, chatId, cancellationToken);
                }
                else if (message.Text == "Продукты")
                {
                    await GetProductsAsync(botClient, chatId, cancellationToken);
                }
                if (message.Text == "Пользователи" || message.Text == "Продукты")
                {
                    var keyboard = new ReplyKeyboardRemove();

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выполнено!",
                        replyMarkup: keyboard,
                        cancellationToken: cancellationToken);
                }



                static async Task GetProductsAsync(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync("https://localhost:7094/api/Product", cancellationToken);
                    var content = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<GetProductResponse[]>(content);

                    var messageText = "";
                    foreach (var product in products)
                    {
                        messageText += $"\n\nДанные продукта:\nProductId: {product.ProductId}\nProductName: {product.ProductName}\nProductDescription: {product.ProductDescription}\nSeason: {product.Season}\nPrice: {product.Price}";
                    }

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: messageText,
                        cancellationToken: cancellationToken);
                }
                static async Task GetUsersAsync(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
                {
                    HttpClient client = new HttpClient();

                    var response = await client.GetAsync("https://localhost:7094/api/User", cancellationToken);
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<GetUserResponse[]>(content);

                    var messageText = "";
                    foreach (var user in users)
                    {
                        messageText += $"\n\nДанные пользователя:\nUserId: {user.UserId}\nSurname: {user.Surname}\nUsername: {user.Username}\nEmail: {user.Email}";
                    }

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: messageText,
                        cancellationToken: cancellationToken);
                }

            }
        }
    }
}