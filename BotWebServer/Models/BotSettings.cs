using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotWebServer.Models
{

    public static class BotSettings
    {
        public static string Token { get; set; } = "";
        public static string Name { get; set; } = "";
        public static string Url { get; set; } = "https://botdurova.azurewebsites.net/{0}";
    }
}
