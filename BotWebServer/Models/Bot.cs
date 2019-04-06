using BotWebServer.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BotWebServer.Models
{
    public class Bot
    {
        private static TelegramBotClient botClient;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if(botClient == null)
            {
                commandsList = new List<Command>();
                commandsList.Add(new StartCommand());
                commandsList.Add(new QuoteCommand());
                commandsList.Add(new NewsCommand());
                commandsList.Add(new FactCommand());
                //TODO: Add more commands

                botClient = new TelegramBotClient(BotSettings.Token);
                string hook = string.Format(BotSettings.Url, "api/message/update");
                await botClient.SetWebhookAsync(hook);
                return botClient;
            }

            return botClient;
        }
    }
}
