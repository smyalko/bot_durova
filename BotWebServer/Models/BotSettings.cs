using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebServer.Models
{

    public static class BotSettings
    {
        public static string Token { get; set; } = "893042911:AAEXKnJeOoGXRUB-iYyiDE1TwCrrEEMnFZI";
        public static string Name { get; set; } = "Bot Durova";
        public static string Url { get; set; } = "https://botdurova.azurewebsites.net/{0}";
    }
}
