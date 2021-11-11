using Discord_Bot.Attributes;
using Discord_Bot.Commands;
using Discord_Bot.Commands.Birthday;
using Discord_Bot.Commands.BotInfo;
using Discord_Bot.Commands.Guild;
using Discord_Bot.Commands.Music;
using Discord_Bot.Commands.Rp;
using Discord_Bot.Services;
using Discord_Bot.SlashCommands;
using Discord_Bot.SlashCommands.Music;
using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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
using System.Reflection;
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
            public ulong BirthdayGuild { get; set; }
            public ulong BirthdayChannel { get; set; }





        }




        private static Timer _timer;
        private static Timer _BossTimer;
        private static Timer _presence;
        private static Timer _TopGGTimer;



        public IReadOnlyDictionary<int, CommandsNextExtension> Commands;

        public Listener(IReadOnlyDictionary<int, CommandsNextExtension> commands)
        {
            Commands = commands;
        }


        public static async Task Main(DiscordShardedClient bot)
        {



            var services = new ServiceCollection()
               .AddSingleton<Random>()
               .AddSingleton<DiscordService>()
               .AddScoped<DatabaseService>()
               .AddSingleton<MusicService>()
               .BuildServiceProvider();


            var Commands = await bot.UseCommandsNextAsync(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { Config.Get("prefix") },
                Services = services,
                UseDefaultCommandHandler = true,
                PrefixResolver = ResolvePrefix,
                EnableMentionPrefix = false
            }) ;


            
            
            foreach (var cmd in Commands.Values)
            {
                
                cmd.RegisterCommands<RandomCommand>();
                cmd.RegisterCommands<Fox>();
                cmd.RegisterCommands<Clear>();
                cmd.RegisterCommands<CoinFlip>();
                cmd.RegisterCommands<UserInfo>();
                cmd.RegisterCommands<Question>();
                cmd.RegisterCommands<Mute>();
                cmd.RegisterCommands<Unmute>();
                cmd.RegisterCommands<Join>();
                cmd.RegisterCommands<Play>();
                cmd.RegisterCommands<Resume>();
                cmd.RegisterCommands<Volume>();
                cmd.RegisterCommands<Track>();
                cmd.RegisterCommands<Skip>();
                cmd.RegisterCommands<RedditCmd>();
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
              //  cmd.RegisterCommands<CharacterCreation>();
               // cmd.RegisterCommands<ActivityHelp>();
                cmd.RegisterCommands<MoveToAfk>();
                cmd.RegisterCommands<Kick>();
               // cmd.RegisterCommands<CharInfo>();
               // cmd.RegisterCommands<CharList>();
               // cmd.RegisterCommands<Grind>();
               // cmd.RegisterCommands<Shop>();
                cmd.RegisterCommands<PrefixCommands>();
                cmd.RegisterCommands<Changelog>();
                cmd.RegisterCommands<RpCommands>();
                cmd.RegisterCommands<Player>();
                cmd.RegisterCommands<Vell>();
                cmd.RegisterCommands<SavePlaylist>();
                cmd.RegisterCommands<DeletePlaylist>();
               
                cmd.SetHelpFormatter<CustomHelpFormatter>();
                


                cmd.CommandErrored += OnError;
                

            }

            var slash = await bot.UseSlashCommandsAsync(new SlashCommandsConfiguration
            {
                Services = new ServiceCollection().AddSingleton<SlashService>().BuildServiceProvider()
            }); 
            foreach (var SlashCommand in slash.Values)
            {
                
                SlashCommand.RegisterCommands<Delete>();
                SlashCommand.RegisterCommands<Userinfo>();
                SlashCommand.RegisterCommands<RedditSlash>();
                SlashCommand.RegisterCommands<TimerSlash>();
                SlashCommand.RegisterCommands<Rp>();
                SlashCommand.RegisterCommands<Pets>();
                SlashCommand.RegisterCommands<Faq>();
                SlashCommand.RegisterCommands<JoinSlash>();
                SlashCommand.RegisterCommands<SlashPlay>();
                SlashCommand.RegisterCommands<SlashPlayer>();
                SlashCommand.RegisterCommands<SlashSpotify>();
                SlashCommand.RegisterCommands<Botinvite>();
                SlashCommand.RegisterCommands<DevSever>();
                SlashCommand.RegisterCommands<FeedbackSlash>();
                

                SlashCommand.SlashCommandErrored += OnSlashErrored;



            }

            bot.Ready += OnReady;
            bot.GuildMemberAdded += OnGuildMemberJoin;
            bot.GuildDownloadCompleted += OnGuildDownload;
            bot.ComponentInteractionCreated += Play.OnClick;
            bot.MessageCreated += OnMessageCreated;




           
           


            _ = Task.Run(async () =>
            {





                int _statusIndex = 0;

                _presence = new Timer(async _ =>
                {
                    
                    var guilds = 0;
                    var count = 0;
                    var channels = 0;
                    var memberList = new List<DiscordMember>();
                    foreach (var client in bot.ShardClients)
                    {
                        guilds += client.Value.Guilds.Count;
                        foreach (var guild in client.Value.Guilds.Values)
                        {

                            count += guild.MemberCount;
                            channels += guild.Channels.Count;
                        }


                    }


                    List<string> _list = new List<string> {bot.ShardClients.Count + " Shards!",guilds + " Guilds",
                count + " Users", channels + " Channels", "Some C# game",  "Probably first C# bot to use music player with interactable buttons!",
                "Watching you through my hidden camera","Do you hear the people sing?"};


                    await bot.UpdateStatusAsync(new DiscordActivity(_list.ElementAtOrDefault(_statusIndex), ActivityType.Playing));
                    _statusIndex = _statusIndex + 1 == _list.Count ? 0 : _statusIndex + 1;






                },
                 null,
                 TimeSpan.FromSeconds(1),
                 TimeSpan.FromSeconds(15));


                await Task.Delay(-1);

            });


            _ = Task.Run(async () =>
            {
                _TopGGTimer = new Timer(async _ => {


                    var guilds = 0;
                    foreach (var client in bot.ShardClients)
                    {
                        guilds += client.Value.Guilds.Count;
                         


                    }


                    await new TopGG().Update(guilds,bot.ShardClients.Count);
                
                },null,TimeSpan.FromSeconds(1),TimeSpan.FromHours(1));
                await Task.Delay(-1);
            
            });





                await Task.CompletedTask;

        }



        public static Task OnMessageCreated(DiscordClient bot, MessageCreateEventArgs e)
        {
            _ = Task.Run(() => {
                
            });
            return Task.CompletedTask;
        }
        public static Task OnSlashErrored(SlashCommandsExtension extension, SlashCommandErrorEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                var context = e.Context;
                if(e.Exception is SlashExecutionChecksFailedException slex)
                {
                    foreach(var check in slex.FailedChecks)
                    {
                        if(check is SlashRequireNsfwAttribute)
                        {
                            await e.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"This command can be only run in nsfw channel"));
                        } else if(check is SlashRequireOtherUserAttribute)
                        {
                            await e.Context.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"You cant mention yourself in this command"));
                        }
                    }
                }

            });
            return Task.CompletedTask;
        }


        public static Task OnGuildMemberJoin(DiscordClient bot, GuildMemberAddEventArgs e)
        {
            _ = Task.Run(async () =>
            {
              
                if (e.Guild.Id == 875583069678092329UL)
                {
                    
                    var role =  e.Guild.GetRole(875592272647946282UL);
                     var member = await e.Guild.GetMemberAsync(e.Member.Id);
                    await member.GrantRoleAsync(role);
                }
            });
            return Task.CompletedTask;

        }
        public static Task OnGuildDownload(DiscordClient bot, GuildDownloadCompletedEventArgs e)
        {

            _ = Task.Run(async () =>
            {


                _timer = new Timer(async _ =>
                {

       

                    var today = DateTime.Now;

                    var client = new MongoClient(Config.Get("uri"));
                    var database = client.GetDatabase("Csharp");
                    var collection = database.GetCollection<BirthdayData>("Birthday");


                    var ListOfAddedBirthdays = collection.Find(new BsonDocument()).ToList();
                    foreach (var document in ListOfAddedBirthdays)
                    {
                        if (document.BirthdayDate.Day == today.Day && document.BirthdayDate.Month == today.Month && today.Hour == 0 && today.Minute == 0)
                        {
                            var BirthdayGuild = e.Guilds[document.BirthdayGuild];
                            var BirthdayChannel = BirthdayGuild.GetChannel(document.BirthdayChannel);
                            var BirthdayMember =  await BirthdayGuild.GetMemberAsync(document.Id);
                            Console.WriteLine(BirthdayMember.Id);
                            var embed = new DiscordEmbedBuilder()
                            .WithImageUrl("https://www.sampleposts.com/wp-content/uploads/2020/11/happy-100th-birthday-quote-wish-grandma-grandpa-1-800x533.jpg")
                            .WithDescription($"{BirthdayMember.Mention} has Birthday today! Happy birthday!")
                            .WithTitle("Happy Birthday");
                            await BirthdayChannel.SendMessageAsync(embed.Build());




                        }

                    }

                },
                 null,
                 TimeSpan.FromSeconds(1),
                 TimeSpan.FromMinutes(1));






                var channel = e.Guilds[875583069678092329UL].GetChannel(875585800870457355UL);
                var role = e.Guilds[875583069678092329UL].GetRole(876317727801880647UL);


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
                 TimeSpan.FromMinutes(1),
                 TimeSpan.FromMinutes(1));

            });




            return Task.CompletedTask;
        }

        public static Task OnReady(DiscordClient bot, ReadyEventArgs e)
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

                var lava = await lavalink.ConnectAsync(lavalinkConfig);
                _ = Task.Run(async () =>
                {
                    lava.PlaybackFinished += Play.PlayInGuild;



                    await Task.Delay(-1);
                });


                _ = Task.Run(async () =>
                {

                    lava.PlayerUpdated += OnVoiceUpdated;


                    await Task.CompletedTask;

                });


               










            });


            return Task.CompletedTask;
        }


        


        public static Task OnVoiceUpdated(LavalinkGuildConnection guildConnection, PlayerUpdateEventArgs e)
        {
            _ = Task.Run(async () =>
            {

                if (guildConnection.CurrentState.CurrentTrack == null)
                {

                    await Task.Delay(TimeSpan.FromMinutes(5));
                    if (guildConnection.CurrentState.CurrentTrack == null) await guildConnection.DisconnectAsync();


                }

            });
            return Task.CompletedTask;
        }








        private static async Task<int> ResolvePrefix(DiscordMessage message)
        {
            if (message.Channel.Type == ChannelType.Private) await Task.FromResult(0);
            var guild = message.Channel.Guild;
            if(guild == null)
            {
                return(-1);
            }
            var GuildID = guild.Id;
            var GuildFunction = await new GetGuildPrefix().GetPrefixAsync(GuildID);
            var UserId = message.Author.Id;
            var UserFunction = await new GetGuildPrefix().GetUserPrefix(UserId);
            if(GuildFunction != null)
            {
                foreach(var prefix in GuildFunction)
                {
                    var PrefixPlace = message.GetStringPrefixLength(prefix,StringComparison.OrdinalIgnoreCase);
                    if (PrefixPlace != -1) return (PrefixPlace);
                    
                }
            }
            if (UserFunction != null)
            {
                foreach (var UserPrefix in UserFunction)
                {
                    var PrefixPlace = message.GetStringPrefixLength(UserPrefix, StringComparison.OrdinalIgnoreCase);
                    if (PrefixPlace != -1) return PrefixPlace;

                }
            }



            return -1;
        }

        



        public static async Task OnError(CommandsNextExtension _, CommandErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
            var ctx = e.Context;

            if (e.Exception is ChecksFailedException)
            {
              
                var failedChecks = ((ChecksFailedException)e.Exception).FailedChecks;
                foreach (var failedCheck in failedChecks)
                {
                    if (failedCheck is CooldownAttribute)
                    {
                        
                        var cooldown = (CooldownAttribute)failedCheck;
                        if (cooldown.GetRemainingCooldown(ctx).TotalSeconds > 86400)
                        {
                            await e.Context.RespondAsync($"Cooldown : {Math.Floor(cooldown.GetRemainingCooldown(ctx).TotalSeconds / 86400)} days");
                        }
                        else
                        {
                            await e.Context.RespondAsync($"Cooldown : {Math.Floor(cooldown.GetRemainingCooldown(ctx).TotalSeconds / 60)} minutes {Math.Floor(cooldown.GetRemainingCooldown(ctx).TotalSeconds % 60)} seconds ");
                        }

                    }
                    else if (failedCheck is RequirePermissionsAttribute)
                    {
                        await e.Context.RespondAsync("You dont have permissions");
                    } else if (failedCheck is RequireNsfwAttribute)
                    {
                        await e.Context.RespondAsync("Not a nsfw channel");
                    } else if(failedCheck is RequireCertainGuildAttribute guild)
                    {
                        var guildName = await e.Context.Client.GetGuildAsync(guild.GuildId);
                        await e.Context.RespondAsync($"Usable within {guildName.Name}  Server");
                    } else if(failedCheck is RequireRoleId roleId)
                    {
                        if(e.Context.Guild.Id != roleId.GuildId)
                        {
                            var guildName =  await e.Context.Client.GetGuildAsync(roleId.GuildId);
                            await e.Context.RespondAsync($" Only usable in {guildName.Name} Server");
                        }
                        else 
                        {
                            await e.Context.RespondAsync($"{e.Context.Guild.GetRole(roleId.RoleId).Name} role required");
                        }
                        
                    }

                }


            }
            if (e.Exception is  CommandNotFoundException)
            {



                await ctx.RespondAsync("Command doesnt exist");
            }

            if (e.Exception is ArgumentException)
            {


                var sb = new StringBuilder();
               
             

                sb.Append("How to use :").Append(" ").Append("\n");

                var result = e.Command.Overloads;

                for (int i = 0; i<result.Count; i++)
                {

                    sb.Append($"{i+1} :").Append(" ").Append($"e!{e.Command.Name}");
                    if (result[i].Arguments.Any())
                    {
                        foreach (var overload in result[i].Arguments)
                        {





                            sb.Append(" ").Append($"{overload.Name}").Append(" ").Append($"<{ overload.Description}>").Append(" ");


                        }
                    }
                    else
                    {
                        sb.Append(" ").Append($"e!{e.Command.Name}").Append("");
                    }

                    sb.Append("\n");


                }


                await ctx.RespondAsync(sb.ToString());
                









            }





        }






    }
}



