using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

            //Donwload news from kod.ru
            var response = await httpClient.GetAsync("https://kod.ru/tag/news/");
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContents);

            List<string> newsList = new List<string>();

            //XPath query that selects 3 H2 elements with class 'post-card-title'
            var newsTitles = htmlDocument.DocumentNode.SelectNodes("(//h2[@class='post-card-title'])[position() < 4]");

            foreach (var htmlNode in newsTitles)
            {
                //Add title to list
                newsList.Add(htmlNode.InnerText);
            }

            string[] emojis =
            {
                "💡", //💡
                "💼", //💼
                "💾", //💾
                "📱", //📱
                "💻", //💻
            };

            Random random = new Random();
            string randomEmoji = emojis[random.Next(0, emojis.Length - 1)];

            StringBuilder messageText = new StringBuilder();
            messageText.AppendFormat("{0} Hey! This is news for you:\n\n", randomEmoji);

            //Create one string from all elements
            foreach (var title in newsList)
            {
                messageText.AppendFormat("— {0}\n\n", title);
            }

            //Send message to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageText.ToString(), parseMode: ParseMode.Markdown);
        }
    }
}
