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
    public class ThinkAboutCommand : Command
    {
        public override string Name => @"Что ты думаешь о ";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            HttpClient httpClient = new HttpClient();

            string query = message.Text.Replace("Что ты думаешь ", "");
            query = query.Replace("?", "");
            query = query.Replace(" ", "+");

            //Url-encoded query
            string urlQuery = System.Web.HttpUtility.UrlEncode(query); 

            //Download videos list from youtube
            var response = await httpClient.GetAsync($"https://www.youtube.com/results?search_query={urlQuery}");
            var pageContents = await response.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContents);

            //XPath select first block with video
            var videosNodes = htmlDocument.DocumentNode.SelectNodes("(//ol[@class='item-section']//li//div[contains(@class, 'yt-lockup-content')])");

            string videoTitle = videosNodes[0].SelectSingleNode("(//a[contains(@class, 'yt-uix-tile-link')])").InnerText;
            string videoLink = "https://youtube.com" + videosNodes[0].SelectSingleNode("(//a[contains(@class, 'yt-uix-tile-link')])").GetAttributeValue("href", "no link");
            string videoAuthor = videosNodes[0].SelectSingleNode("(//div[contains(@class,'yt-lockup-byline')])").InnerText;

            string messageText = $"Моё мнение сходится с видео *{videoAuthor}*:\n\n{videoTitle}\n{videoLink}";

            
            //Send message to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, messageText, parseMode: ParseMode.Markdown);
        }
    }
}
