
using Discord_Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using Microsoft.Extensions.Logging;
using System.Linq;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Discord_Bot.SlashCommands;
using System.Collections;


namespace Discord_Bot
{
    
    class Bot
    {

        public static void Main(string[] args)
        {
            
            MainAsync().GetAwaiter().GetResult();
        }

        public  static async Task MainAsync()
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var token = Config.Get("token");
            var Bot = new DiscordShardedClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                MinimumLogLevel = LogLevel.Debug

            });
            
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
            await Bot.UseInteractivityAsync();
            
            await Bot.StartAsync();
            await Listener.Main(Bot);
            await Task.Delay(-1);
           

        }

       
    }




}



