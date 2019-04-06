using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotWebServer.Models.Commands
{
    public class FactCommand : Command
    {
        public override string Name => @"/fact";

        public override bool Contains(Message message)
        {
            if (message.Type != MessageType.Text)
                return false;

            return message.Text.Contains(Name);
        }

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            string[] programmingFacts = {
                "The *first computer programmer* was a female, named Ada Lovelace",
                "The *first game* was created in 1961. Fun facts are that it didn’t earn any money",
                "The *first virus* was created in 1983",
                "The *first computer* “bug” was identified in1947 as a dead moth",
                "The *first computer* was actually a loom called the Jacquard loom, an automated, mechanical loom, which didn’t use any electricity",
                "The *first high-level* (very close to real English that we use to communicate) *programming language* was *FORTRAN*. invented in 1954 by IBM’s John Backus",
                "Computer programming is one of the fastest growing occupations currently",
                "The original name for JAVA was OAK",
                "JavaScript is *not compiled*",
                "Majors related to computer programming are among the highest paying in colleges and universities A programming language is basically a language that allows a human being to communicate with a computer The lifestyle we live today with our tablets, and mobile phones wouldn’t be possible without computer programming",
                "Did you know how many total programming languages? – it’s 698",
                "Most people are intimidated by the thought of learning how to program, however as with anything, the more you practice and repeatedly do that task, the easier it gets",
                "The Java mascot, ‘The Duke’ was created by Joe Palrang. Palrang is the same guy who has worked on the Hollywood blockbuster, Shrek. Duke is celebrated at Oracle",
                "Four states of programmer progress:\na) Complex Programming\nb) Making Progress\nc) Slow Progress\nd) Stuck",
                "It is not a tool or magic it is power to create your Imagination in reality",
                "Programming can learn you a new way of thinking",
                "Perl is sometimes known as the “Swiss-Army knife” of programming languages",
                "APIs are like stars, once a class is there everybody will assume it will always be there",
                "Did you know first computer bug was named due to a real bug as shown in below pic? Grace Hopper recorded the first computer ‘bug’ in the book as she was working for the MARK II computer",
                "Ctrl + C and Ctrl + V have saved more lives than Batman and Robin",
                "The Ctrl-Z is better than a time machine",
                "That there is one thing called “Constant Variable”",
                "Programmers always looking for a girl who can code",
                "A parent may kill its children if the task assigned to them is no longer needed",
                "Writing cryptic code is deep joy in the soul of a programmer",
                "When you format your hard drive, the files are not deleted",
                "A coder is a person who transforms cola & pizza to code",
                "“Refresh button” of the windows desktop is not some magical tool which keeps your computer healthy",
                "The programmers are the main source of income for eye doctors",
                "If any programmer orders three beers with his fingers, he normally only gets two",
                "Programmers love to code day and night",
                "Sleeping with a problem can actually solve it",
                "“|” Key is not useless",
                "1 Mbps and 1 MBps internet connection don’t mean the same thing",
                "A programmer is similar to a game of golf. The point is not getting the ball in the hole but how many strokes it takes",
                "A programmer *is not a PC repairman*"
            };

            Random random = new Random();
            string randomFact = programmingFacts[random.Next(1, programmingFacts.Length)];

            //Send fact to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, randomFact, parseMode: ParseMode.Markdown);
        }
    }
}
