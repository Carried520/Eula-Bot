using DSharpPlus;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DSharpPlus.Interactivity.Extensions;


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



