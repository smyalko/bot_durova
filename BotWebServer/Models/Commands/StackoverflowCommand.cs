using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotWebServer.Models.Commands
{
    public class StackoverflowCommand : Command
    {
        public override string Name => "Можешь мне помочь с этой ошибкой?";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override Task Execute(Message message, TelegramBotClient client)
        {
            throw new NotImplementedException();
        }
    }
}
