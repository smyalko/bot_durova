using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotWebServer.Models.Commands
{
    public class NewsCommand : Command
    {
        public override string Name => @"/news";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        /*
         * Get and parse last 10 news from kod.ru
         * https://kod.ru/tag/news/
         */
        public override async Task Execute(Message message, TelegramBotClient client)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://kod.ru/tag/news/");
            var pageContents = response.Content.ReadAsStringAsync();


        }
    }
}
