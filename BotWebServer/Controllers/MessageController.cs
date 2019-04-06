using BotWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotWebServer.Controllers
{
    [Route("api/message")]

    public class MessageController:Controller
    {
        private ILogger logger;

        public MessageController(ILogger<MessageController> logger)
        {
            this.logger = logger;
        }

        [Route("update")]
        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            logger.LogInformation($"User id: {update.Message.Chat.Id}");
            logger.LogInformation($"Message: {update.Message.Text}");

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }

            return Ok();
        }
    }
}
