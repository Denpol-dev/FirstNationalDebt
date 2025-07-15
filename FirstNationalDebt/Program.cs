using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var token = "7538875229:AAGCA8CPfR7j2dGsiJZSYMpMxT7LeoeRb5I";
using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(token, cancellationToken: cts.Token);

// Получаем информацию о боте
var me = await bot.GetMe();
await bot.DeleteWebhook();
await bot.DropPendingUpdates();

var chats = new List<long>();

async Task SendWelcomeMessage(long chatId)
{
    chats.Add(chatId);
    await bot.SendMessage(
        chatId: chatId,
        text: "Вас приветствует корпоративный бот поиска решений по клиенту компании First National Debt. Введите имя пользователя tg вашего клиента: ",
        cancellationToken: cts.Token);
}

async Task ResultQuery(long chatId, string query)
{
    query = query.ToLower();

    switch (query)
    {
        case "@false_visi0n":
            {
                await bot.SendMessage(
                    chatId: chatId,
                    text: "> У клиента 33 микрозайма, 12 кредитов и 2 ипотеки.\r\n> Оценка: крайне привлекательный клиент, удерживать всеми силами.\r\n> Код операции: 8V1G22FD.",
                    cancellationToken: cts.Token);
                break;
            }
        case "@mulhamsh":
            {
                await bot.SendMessage(
                    chatId: chatId,
                    text: "> Не является клиентом нашего банка.\r\n> Оценка: иностранный клиент из арабских стран, нужно привлечь для выгодного вливания в наш банк.\r\n> Код операции: 1H3C3AD5.",
                    cancellationToken: cts.Token);
                break;
            }
        case "@include_19":
            {
                await bot.SendMessage(
                    chatId: chatId,
                    text: "> У клиента 942 микрозайма, 1589 кредитов и 1 ипотека.\r\n> Оценка: частые займы для ремонта квартиры. Привлекательный клиент.\r\n> Код операции: 9FD4VFV1.",
                    cancellationToken: cts.Token);
                break;
            }
        case "@fanban":
            {
                await bot.SendMessage(
                    chatId: chatId,
                    text: "> У клиента отсутствуют кредиты и займы. \r\n> Оценка: является исключительно зарплатным клиентом. Необходимо убедить взять займ.\r\n> Код операции: 2BN4JR7Y9.",
                    cancellationToken: cts.Token);
                break;
            }
        default:
            {
                if (query[0] == '\'' && query[1] == ';')
                {
                    if (query.Contains("drop"))
                    {
                        if (query.Contains("client_case"))
                        {
                            await bot.SendMessage(
                                chatId: chatId,
                                text: "> Запрос выполнен.  \r\n> 76,851,452,892 строк затронуто.  \r\n> Код операции: 7T6J44A.",
                                cancellationToken: cts.Token);

                            await bot.SendMessage(
                                chatId: chatId,
                                text: "\r\n> [!] ВНИМАНИЕ: активирован специальный протокол First National Debt \"Cerberus\".\r\n> **\"Все агенты → converge on 55.XXXXX, ...Z9bPxw... (signal lost)\" * *",
                                cancellationToken: cts.Token);
                        }
                        else
                        {
                            await bot.SendMessage(
                                chatId: chatId,
                                text: "> Запрос выполнен.  \r\n> 4,716 строк затронуто.  \r\n> Код операции: 5G1E88Y3.",
                                cancellationToken: cts.Token);
                        }
                    }
                    else
                    {
                        await bot.SendMessage(
                            chatId: chatId,
                            text: "Error 500, try again.",
                            cancellationToken: cts.Token);
                    }
                }
                else if (query[0] == '@')
                {
                    await bot.SendMessage(
                        chatId: chatId,
                        text: "К сожалению, решений по данному клиенту не найдено",
                       cancellationToken: cts.Token);

                }
                else
                {
                    await bot.SendMessage(
                        chatId: chatId,
                        text: "Error 500, try again.",
                        cancellationToken: cts.Token);
                }
                break;
            }
    }
}

bot.OnError += OnError;
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Escape to terminate");

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine($"Ошибка: {exception.Message}");
    await Task.Delay(2000, cts.Token);
}

async Task OnMessage(Message msg, UpdateType type)
{
    Console.WriteLine($"\n--- Новое сообщение ---");
    Console.WriteLine($"Чат ID: {msg.Chat.Id}");
    Console.WriteLine($"Имя пользователя: {msg.From?.Username ?? "N/A"}");
    Console.WriteLine($"Текст: {msg.Text ?? "N/A"}");
    Console.WriteLine($"Тип: {msg.Type}");
    Console.WriteLine($"Дата: {msg.Date}");
    Console.WriteLine("----------------------\n");

    if (!chats.Contains(msg.Chat.Id))
    {
        await SendWelcomeMessage(msg.Chat.Id);
    }
    else
    {
        await ResultQuery(msg.Chat.Id, msg.Text ?? "");
    }
}

while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
await cts.CancelAsync();