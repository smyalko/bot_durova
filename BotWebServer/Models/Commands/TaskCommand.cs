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

            //Parse pagination number to define max page number
            var pagination = htmlDocument.GetElementbyId("content").SelectNodes("//div[@class='pagination']//a");

            //Parse number
            int lastPageNumber = int.Parse(pagination[pagination.Count - 1].InnerText);

            //Which page of site must be parsed for random task
            Random random = new Random();
            int pageToSelect = random.Next(1, lastPageNumber);
            Console.WriteLine(pageToSelect);

            //If randomly generated page number is not the same as 1 (first page) download new page
            if (pageToSelect != 1)
            {
                //Donwload news from kod.ru
                response = await httpClient.GetAsync($"https://tproger.ru/category/problems/page/{pageToSelect}/");
                pageContents = await response.Content.ReadAsStringAsync();

                htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(pageContents);
            }
            //Otherwise just parse existing page

            var articles = htmlDocument.GetElementbyId("content").SelectNodes("//article//h2[@class='entry-title']//a");

            //foreach (var article in articles)
            //{
            //    System.Console.WriteLine(article.SelectSingleNode("span[@class='entry-title-heading']").InnerText);
            //    System.Console.WriteLine(article.GetAttributeValue("href", "undefined link"));
            //}

            int randomTaskNumber = random.Next(1, articles.Count - 1);

            //Get random task from tasks
            string randomTaskLink = articles[randomTaskNumber].GetAttributeValue("href", "undefined link");
            string randomTaskTitle = articles[randomTaskNumber].SelectSingleNode("span[@class='entry-title-heading']").InnerText;

            //Construct text message
            string messageText = $"📗  *Задача для тебя:*\n\n{randomTaskTitle}\n\n{randomTaskLink}";

            //Send message to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageText.ToString(), parseMode: ParseMode.Markdown);
        }
    }
}
