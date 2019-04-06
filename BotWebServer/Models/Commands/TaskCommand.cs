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
    public class TaskCommand : Command
    {
        public override string Name => @"/task";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        /*
         * Get random problem from tproger.ru to practice math or programming
         * https://tproger.ru/category/problems/
         */
        public override async Task Execute(Message message, TelegramBotClient client)
        {
            HttpClient httpClient = new HttpClient();

            //Donwload news from kod.ru
            var response = await httpClient.GetAsync("https://tproger.ru/category/problems/");
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContents);

            //Parse for an article elements
            var articles = htmlDocument.GetElementbyId("content").SelectNodes("//article//h2[@class='entry-title']//a");

            Random random = new Random();
            int randomTaskNumber = random.Next(1, articles.Count - 1);

            //Get random task from tasks
            string randomTaskLink = articles[randomTaskNumber].GetAttributeValue("href", "undefined link");
            string randomTaskTitle = articles[randomTaskNumber].SelectSingleNode("span[@class='entry-title-heading']").InnerText;

            //Construct text message
            StringBuilder messageText = new StringBuilder();
            messageText.AppendFormat("📗  *Задача для тебя:*\n\n{0}\n\n{1}", randomTaskTitle, randomTaskLink);

            //Send message to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageText.ToString(), parseMode: ParseMode.Markdown);
        }
    }
}
