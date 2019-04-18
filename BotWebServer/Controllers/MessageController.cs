using BotWebServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotWebServer.Controllers
{
    [Route("api/message")]

    public class MessageController : Controller
    {
        private ILogger logger;
        private DatabaseContext context;

        public MessageController(ILogger<MessageController> logger, DatabaseContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        [Route("update")]
        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var chatId = message.Chat.Id;
            var botClient = await Bot.GetBotClientAsync();

            logger.LogInformation($"User id: {update.Message.Chat.Id}");
            logger.LogInformation($"Message: {update.Message.Text}");

            //Search for suitable command and run it
            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    logger.LogInformation($"Executed command {command.Name}");
                    break;
                }
            }

            var chat = await context.Chats.Where(c => c.Id == chatId).FirstOrDefaultAsync();

            //If there are no such chat info in database, add it
            if (chat == null)
            {
                context.Chats.Add(new ChatInfo
                {
                    Id = (int)chatId
                });

                logger.LogInformation($"New chat {chatId} successfully saved to database!");
            }

            return Ok();
        }
    }
}
