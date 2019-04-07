using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotWebServer.Models
{
    public class QuoteCommand : Command
    {
        public override string Name => @"/quote";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        /*Downloading ramdon quote from tproger.ru
         * https://tproger.ru/wp-content/plugins/citation-widget/get-quote.php
         */

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://tproger.ru/wp-content/plugins/citation-widget/get-quote.php");
            var pageContents = await response.Content.ReadAsStringAsync();

            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, pageContents, parseMode: ParseMode.Markdown);
        }
    }
}
