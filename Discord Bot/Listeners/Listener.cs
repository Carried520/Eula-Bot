using Discord_Bot.Commands;
using Discord_Bot.Commands.Birthday;
using Discord_Bot.Commands.BotInfo;
using Discord_Bot.Commands.Guild;
using Discord_Bot.Commands.Music;
using Discord_Bot.Commands.Rp;
using Discord_Bot.SlashCommands;
using Discord_Bot.SlashCommands.Music;
using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Lavalink.EventArgs;
using DSharpPlus.Net;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
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

        public class BirthdayData
        {

            public ulong Id { get; set; }
            public DateTime BirthdayDate { get; set; }



        }


        private static Timer _timer;
        private static Timer _BossTimer;
        private static Timer _BirthdayChecker;


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
                StringPrefixes = new[] { Config.Get("prefix") },
                Services = services,
                UseDefaultCommandHandler = false


            });

            

            foreach (var cmd in Commands.Values)
            {
                cmd.RegisterCommands<hello>();
                cmd.RegisterCommands<RandomCommand>();
                cmd.RegisterCommands<Fox>();
                cmd.RegisterCommands<Clear>();
                cmd.RegisterCommands<CoinFlip>();
                cmd.RegisterCommands<Spotify>();
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
                cmd.RegisterCommands<Dm>();
                cmd.RegisterCommands<Clock>();
                cmd.RegisterCommands<Ask>();
                cmd.RegisterCommands<Marry>();
                cmd.RegisterCommands<Divorce>();
                cmd.RegisterCommands<ShibeCommand>();
                cmd.RegisterCommands<Queue>();
                cmd.RegisterCommands<Shuffle>();
                cmd.RegisterCommands<Stop>();
                cmd.RegisterCommands<GuildMission>();
                cmd.RegisterCommands<GuildList>();
                cmd.RegisterCommands<ResetAll>();
                cmd.RegisterCommands<ResetOne>();
                cmd.RegisterCommands<BossRole>();
                cmd.RegisterCommands<FollowChannel>();
                cmd.RegisterCommands<Birthday>();
                cmd.RegisterCommands<BotInviteLink>();
                cmd.RegisterCommands<DevServerLink>();
                cmd.RegisterCommands<FeedbackCommand>();

                cmd.SetHelpFormatter<CustomHelpFormatter>();

                

                cmd.CommandErrored += OnError;

            }

            var slash = await bot.UseSlashCommandsAsync();
            foreach (var SlashCommand in slash.Values)
            {

                SlashCommand.RegisterCommands<Delete>();
                SlashCommand.RegisterCommands<Userinfo>();
                SlashCommand.RegisterCommands<RedditSlash>();
                SlashCommand.RegisterCommands<DmSlash>();
                SlashCommand.RegisterCommands<TimerSlash>();
                SlashCommand.RegisterCommands<Rp>();
                SlashCommand.RegisterCommands<Pets>();
                SlashCommand.RegisterCommands<Faq>();
                SlashCommand.RegisterCommands<JoinSlash>();
                SlashCommand.RegisterCommands<SlashPlay>();
                SlashCommand.RegisterCommands<SlashPlayer>();
                SlashCommand.RegisterCommands<SlashSpotify>();
                
                SlashCommand.SlashCommandErrored += OnSlashErrored;
                
                

            }

            bot.Ready += OnReady;
            bot.GuildDownloadCompleted += OnGuildDownload;
            bot.MessageCreated += OnMessageCreated;
            bot.ComponentInteractionCreated += Play.OnClick;
            

          



            await Task.CompletedTask;

        }

        public static Task OnSlashErrored (SlashCommandsExtension extension , SlashCommandErrorEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                Random random = new Random();
                var members = e.Context.Guild.Members;
                int index = random.Next(members.Count);
                var key = members.Values.ElementAt(index);

                await e.Context.Channel.SendMessageAsync($" Slash Command {e.Context.CommandName} failed to execute ");
            
            });
                return Task.CompletedTask;
        }



        public static Task OnGuildDownload(DiscordClient bot, GuildDownloadCompletedEventArgs e)
        {
            _ = Task.Run(async () =>
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

                




                var channel = e.Guilds[875583069678092329].GetChannel(875585800870457355);
                var role = e.Guilds[875583069678092329].GetRole(876317727801880647);

                
                var bossOne = new BossSchedule().Bosses();
                var embed = new DiscordEmbedBuilder();
                embed.WithDescription($"{bossOne[0]}");
                var message = await bot.SendMessageAsync(channel, embed.Build());
                _BossTimer = new Timer(async _ =>
                {

                    var boss = new BossSchedule().Bosses();
                    var embeded = new DiscordEmbedBuilder();
                    embeded.WithDescription($"{boss[0]}");
                    await message.ModifyAsync(new DiscordMessageBuilder().WithEmbed(embeded.Build()));
                    _ = Task.Run(async () =>
                    {
                        _ = Task.Run(async () =>
                        {
                            if (boss.Any())
                            {
                                if (Convert.ToInt32(boss[1]) == 10)
                                {
                                    var msg = await bot.SendMessageAsync(channel, $" {role.Mention} Boss spawns in 10 minutes!");
                                    await Task.Delay(TimeSpan.FromMinutes(10));
                                    await msg.DeleteAsync();


                                }




                            }

                        });



                        await Task.Delay(-1);
                    });


                }, null,
                 TimeSpan.FromSeconds(1),
                 TimeSpan.FromMinutes(1));
                
            });
               


        
            return Task.CompletedTask;
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

                    lava.PlayerUpdated += OnVoiceUpdated;
                    
                    
                    await Task.CompletedTask;
                
                });
               

                   


                    

               

            });


            return Task.CompletedTask;
        }

            public static  Task OnVoiceUpdated(LavalinkGuildConnection guildConnection, PlayerUpdateEventArgs e)
            {
                _ = Task.Run(async () =>
                {
                
                    if (guildConnection.CurrentState.CurrentTrack == null )
                    {
                       
                            await Task.Delay(TimeSpan.FromMinutes(5));
                            if (guildConnection.CurrentState.CurrentTrack == null) await guildConnection.DisconnectAsync();
                        
                    }
                        
                });
                return Task.CompletedTask;
                }

        
        public static Task OnMessageCreated(DiscordClient bot , MessageCreateEventArgs e)
        {

            var cnext = bot.GetCommandsNext();
            var msg = e.Message;

            var cmdStart = msg.GetStringPrefixLength(Config.Get("prefix"),StringComparison.InvariantCultureIgnoreCase);
            if (cmdStart == -1) return Task.CompletedTask;

            var prefix = msg.Content.Substring(0, cmdStart);
            var cmdString = msg.Content.Substring(cmdStart);

            var command = cnext.FindCommand(cmdString, out var args);
            if (command == null) return Task.CompletedTask;

            var ctx = cnext.CreateContext(msg, prefix, command, args);
            Task.Run(async () => await cnext.ExecuteCommandAsync(ctx));

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



