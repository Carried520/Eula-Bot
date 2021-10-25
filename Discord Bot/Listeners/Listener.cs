using Discord_Bot.Commands;
using Discord_Bot.Commands.Guild;
using Discord_Bot.Commands.Music;
using Discord_Bot.Commands.Rp;
using Discord_Bot.SlashCommands;
using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Discord_Bot
{
    class Listener

    {

        public class Exp
        {
            
            public ulong Id { get; set; }
            public int Experience { get; set; }
            public int Level { get; set; }
        }
        private static Timer _timer;


        public IReadOnlyDictionary<int, CommandsNextExtension> Commands;

        public Listener(IReadOnlyDictionary<int, CommandsNextExtension> commands)
        {
            Commands = commands;
        }


        public static async Task Main(DiscordShardedClient bot)
        {



            var services = new ServiceCollection()
               .AddSingleton<Random>()
               .BuildServiceProvider();


            var Commands = await bot.UseCommandsNextAsync(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { Config.get("prefix") },
                Services = services

            });

            

            foreach (var cmd in Commands.Values)
            {
                cmd.RegisterCommands<hello>();
                cmd.RegisterCommands<RandomCommand>();
                cmd.RegisterCommands<Fox>();
                cmd.RegisterCommands<Clear>();
                cmd.RegisterCommands<CoinFlip>();
                cmd.RegisterCommands<Ping>();
                cmd.RegisterCommands<UserInfo>();
                cmd.RegisterCommands<Question>();
                cmd.RegisterCommands<Reaction>();
                cmd.RegisterCommands<Mute>();
                cmd.RegisterCommands<Unmute>();
                cmd.RegisterCommands<Join>();
                cmd.RegisterCommands<Play>();
                cmd.RegisterCommands<Resume>();
                cmd.RegisterCommands<Volume>();
                cmd.RegisterCommands<Track>();
                cmd.RegisterCommands<Skip>();
                cmd.RegisterCommands<RedditCmd>();
                cmd.RegisterCommands<ExpCommand>();
                cmd.RegisterCommands<Dm>();
                cmd.RegisterCommands<Clock>();
                cmd.RegisterCommands<Ask>();
                cmd.RegisterCommands<Marry>();
                cmd.RegisterCommands<Divorce>();
                cmd.RegisterCommands<ShibeCommand>();
                cmd.RegisterCommands<BossCommand>();
                cmd.RegisterCommands<Queue>();
                cmd.RegisterCommands<Shuffle>();
                cmd.RegisterCommands<Stop>();
                cmd.SetHelpFormatter<CustomHelpFormatter>();

                

                cmd.CommandErrored += OnError;

            }

            var slash = await bot.UseSlashCommandsAsync();
            foreach (var value in slash.Values)
            {

                value.RegisterCommands<Delete>(569505274667466762);
                value.RegisterCommands<Guildmission>(569505274667466762);
                value.RegisterCommands<Userinfo>(569505274667466762);
                value.RegisterCommands<RedditSlash>(569505274667466762);
                value.RegisterCommands<DmSlash>(569505274667466762);
                value.RegisterCommands<TimerSlash>(569505274667466762);
                value.RegisterCommands<Rp>(569505274667466762);
                value.RegisterCommands<Pets>(569505274667466762);
                value.RegisterCommands<Faq>(569505274667466762);
            }

            bot.Ready += OnReady;
            bot.GuildDownloadCompleted += OnGuildDownload;
            bot.MessageCreated += OnMessageCreated;
            bot.ComponentInteractionCreated += Play.OnClick;
            

          



            await Task.CompletedTask;

        }

        public static async Task OnGuildDownload(DiscordClient bot, GuildDownloadCompletedEventArgs e)
        {

            
            var count = 0;
            var channels = 0;
            foreach (var guild in e.Guilds)
            {
             
                count += guild.Value.MemberCount;
                channels += guild.Value.Channels.Count;
            }
            

            List<string> _list = new List<string> {bot.ShardCount + " Shards!",e.Guilds.Count + " Guilds",
                count + " Users",channels + " Channels", "Some C# game", "Json is son of J", "Is Austria even a country", "Very first C# bot to use music player with interactable buttons!",
                "Watching you through my hidden camera","C#>Java"};
            int _statusIndex = 0;

            _timer = new Timer(async _ =>
            {
                await bot.UpdateStatusAsync(new DiscordActivity(_list.ElementAtOrDefault(_statusIndex), ActivityType.Playing));
                _statusIndex = _statusIndex + 1 == _list.Count ? 0 : _statusIndex + 1;
            },
             null,
             TimeSpan.FromSeconds(1),
             TimeSpan.FromMinutes(1));
            var channel = e.Guilds[569505274667466762].GetChannel(699396897994965061);

            
            var bossOne = new BossSchedule().Bosses();
            var embed = new DiscordEmbedBuilder();
             embed.WithDescription($"{bossOne[0]}");
            var message = await bot.SendMessageAsync(channel,embed.Build());
            _timer = new Timer(async _ =>
            {
                var boss = new BossSchedule().Bosses();
                    var embeded = new DiscordEmbedBuilder();
               embeded.WithDescription($"{boss[0]}");
                await message.ModifyAsync(new DiscordMessageBuilder().WithEmbed(embeded.Build()));

                _ = Task.Run(async () =>
                {
                    if(Convert.ToInt32(boss[1]) == 10)
                    {
                        var msg = await bot.SendMessageAsync(channel,"Boss spawns in 10 minutes!");
                        await Task.Delay(TimeSpan.FromMinutes(10));
                        await msg.DeleteAsync();
                    }
                    else if(Convert.ToInt32(boss[1]) == 5)
                    {
                        var msg = await bot.SendMessageAsync(channel, "Boss spawns in 5 minutes!");
                        await Task.Delay(TimeSpan.FromMinutes(5));
                        await msg.DeleteAsync();
                    }
                    else if(Convert.ToInt32(boss[1]) == 2)
                    {
                        var msg = await bot.SendMessageAsync(channel, "Boss spawns in 1 minute.Hurry!");
                        await Task.Delay(TimeSpan.FromMinutes(1));
                        await msg.DeleteAsync();
                    }
                });


                },
             null,
             TimeSpan.FromMinutes(1),
             TimeSpan.FromMinutes(1));


           





                await Task.CompletedTask;
        }

        public static  Task OnReady(DiscordClient bot, ReadyEventArgs e)
        {
            _ = Task.Run(async () =>
            {


                bot.Logger.LogInformation($"{bot.CurrentUser.Username}#{bot.CurrentUser.Discriminator} is ready to start");



                var endpoint = new ConnectionEndpoint
                {
                    Hostname = "127.0.0.1",
                    Port = 2333
                };

                var lavalinkConfig = new LavalinkConfiguration
                {
                    Password = "youshallnotpass",
                    RestEndpoint = endpoint,
                    SocketEndpoint = endpoint
                };
                var lavalink = bot.UseLavalink();

              var lava =  await lavalink.ConnectAsync(lavalinkConfig);
                _ = Task.Run(async () =>
                {
                    lava.PlaybackFinished += Play.PlayInGuild;
                    
                    
                    
                    await Task.Delay(-1);
                });


                _ = Task.Run(async () => {

                   lava.Discord.VoiceServerUpdated += OnVoiceUpdated;
                    await Task.CompletedTask;
                
                });




            });


            return Task.CompletedTask;
        }

        public static  Task OnVoiceUpdated(DiscordClient bot, VoiceServerUpdateEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                await Task.CompletedTask;
            });
            return Task.CompletedTask;
            }

        
        public static Task OnMessageCreated(DiscordClient bot , MessageCreateEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                if (e.Author.IsBot) return;
                var id = e.Author.Id;
                var exp = 100;

                var client = new MongoClient(Config.get("uri"));
                var database = client.GetDatabase("Csharp");
                var collection = database.GetCollection<Exp>("Exp");
                var filter = Builders<Exp>.Filter.Eq("_id", id);        
                var list = collection.Find(filter).FirstOrDefaultAsync().Result;
                

                if (list == null)
                {
                  await  collection.InsertOneAsync(new Exp { Id = id , Experience = exp ,Level=1});
                    await e.Channel.SendMessageAsync($"Level up {0}->{1}");
                }

                 else
                 {
                    var search = list.Experience;
                     var up = exp+search;
                    var CurrentLevel = list.Level;
                    var RequiredExp = 5000 * CurrentLevel ;
                    if (up>=RequiredExp)
                    {
                        var updated = Builders<Exp>.Update.Set("Level", CurrentLevel + 1).Set("Experience", up - RequiredExp);
                        
                        collection.UpdateOne(filter, updated);
                        

                        await e.Channel.SendMessageAsync($"Level up {CurrentLevel}->{CurrentLevel+1}");
                    }

                    var update = Builders<Exp>.Update.Set("Experience",up);
                     Console.WriteLine(search);
                      collection.UpdateOne(filter,update);
                   
                     
                   
                 }


                await Task.CompletedTask;
            });
              


            return Task.CompletedTask;
        }


        public static async Task OnError(CommandsNextExtension _, CommandErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
            var ctx = e.Context;

            if (e.Exception.GetType().ToString() == "DSharpPlus.CommandsNext.Exceptions.ChecksFailedException") {


                var failedChecks = ((ChecksFailedException)e.Exception).FailedChecks;
                foreach (var failedCheck in failedChecks)
                {
                    if (failedCheck is CooldownAttribute)
                    {

                        var cooldown = (CooldownAttribute)failedCheck;
                        await e.Context.RespondAsync($"Cooldown : {Math.Floor(cooldown.GetRemainingCooldown(ctx).TotalSeconds / 60)} minutes {Math.Floor(cooldown.GetRemainingCooldown(ctx).TotalSeconds % 60)} seconds ");
                    }
                }


            }
            if (e.Exception.GetType().ToString() == "DSharpPlus.CommandsNext.Exceptions.CommandNotFoundException")
            {
               
               
                
             await  ctx.RespondAsync("Command doesnt exist");
            }

            if ( e.Exception.GetType().ToString() == "System.ArgumentException")
            {
                Console.WriteLine(e.Exception);
                var sb = new StringBuilder()
                .Append("You can use one of following options:").Append(" ");
                foreach (var over in e.Command.Overloads)
                {
                    foreach (var arg in over.Arguments)
                    {
                        sb.Append($"\n{arg.Name}").Append(" ").Append($"<{ arg.Description}>");
                    }
                }



                await ctx.RespondAsync(sb.ToString());
            }










        }



    }






    }



