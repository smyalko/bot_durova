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
                "*Первым программистом* была женщина Ада Лавлейс",
                "*Первая компьютерная игра* была создана в 1961. Весёлым кажется то что она не собирала никаких денег",
                "*Первый вирус* был создан в 1983",
                "*Первый компьютерный баг* был идентифицирован в 1947 как 'мёртвая бабочка'",
                "*Первый компьютер* на самом деле был автоматизированным ткацким станком, который не использовал никакого электричества",
                "*Первым языком программирования высокого уровня* был *FORTRAN*, изобретённый в 1954 работником IBM Джоном Бэкусом",
                "Программирование это одна из наиболее быстрорастущих сфер деятельности",
                "Оригинальным именем JAVA был OAK",
                "JavaScript *не компилируется*",
                "Ты знал сколько всего языков программирования? - 698",
                "Символ Java, который называется Дюк, создал Джо Палранг. Именно он работал над мультфильмом «Шрек»",
                "Программирование может научить тебя думать по-новому",
                "Perl знаком как \"Нож разкладушка\" языков программирования",
                "Ты знал что первым компъютерным багом была настоящая бабочка? (bug - носекомое)Грейс Куппер зарегестрировал первый \"баг\" пока работал с компьютером MARK II",
                "Ctrl + C и Ctrl + V сохранили больше жизней чем Бэтмен и Робин",
                "Ctrl-Z лучше чем машина времени",
                "Программисты всегда желают найди девушку которая умеет программировать",
                "Родитель может убить его ребёнка если присвоеная ему задача больше не нужна",
                "Когда ты форматируешь жёсткий диск, файлы не удаляются)",
                "Программист - человек который превращает колу и пиццу в код",
                "Программисты - основной доход окулистов",
                "Если программист заказывает три банки пива его пальцами, обычно он получает две",
                "Программисты люблят кодить днём и ночью",
                "Сон с проблемой в голове может решить её",
                "Кнопка \"|\" не бесполезна!",
                "1 Мбит и 1 Мбайт интернет соединения не одни и те же вещи",
                "Программист *не чинит пк*!"
            };

            Random random = new Random();
            string randomFact = programmingFacts[random.Next(1, programmingFacts.Length)];

            //Send fact to user
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, randomFact, parseMode: ParseMode.Markdown);
        }
    }
}
