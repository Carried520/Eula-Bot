using Discord_Bot.Commands;
using Discord_Bot.Commands.Activities;
using Discord_Bot.Commands.Activities.ActivityMaker;
using Discord_Bot.Commands.Birthday;
using Discord_Bot.Commands.BotInfo;
using Discord_Bot.Commands.Guild;
using Discord_Bot.Commands.Moderation;
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
            public ulong BirthdayGuild { get; set; }
            public ulong BirthdayChannel { get; set; }





        }


        public class GuildPrefix
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }

        }

        public class UserPrefix
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }

        }


        private static Timer _timer;
        private static Timer _BossTimer;



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
                cmd.RegisterCommands<CharacterCreation>();
                cmd.RegisterCommands<ActivityHelp>();
                cmd.RegisterCommands<MoveToAfk>();
                cmd.RegisterCommands<Kick>();
                cmd.RegisterCommands<CharInfo>();
                cmd.RegisterCommands<CharList>();
                cmd.RegisterCommands<Grind>();
                cmd.RegisterCommands<Shop>();
                cmd.RegisterCommands<PrefixCommands>();
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
                SlashCommand.RegisterCommands<Botinvite>();
                SlashCommand.RegisterCommands<DevSever>();
                SlashCommand.RegisterCommands<FeedbackSlash>();
                

                SlashCommand.SlashCommandErrored += OnSlashErrored;



            }

            bot.Ready += OnReady;
            bot.GuildDownloadCompleted += OnGuildDownload;
            bot.MessageCreated += OnMessageCreated;
            bot.ComponentInteractionCreated += Play.OnClick;






            await Task.CompletedTask;

        }

        public static Task OnSlashErrored(SlashCommandsExtension extension, SlashCommandErrorEventArgs e)
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


        public static Task OnGuildMemberJoin(DiscordClient bot, GuildMemberAddEventArgs e)
        {
            _ = Task.Run(async () =>
            {
                if (e.Guild.Id == 875583069678092329L)
                {
                    var role = e.Guild.GetRole(875592272647946282L);
                    await e.Member.GrantRoleAsync(role);
                }
            });
            return Task.CompletedTask;

        }
        public static Task OnGuildDownload(DiscordClient bot, GuildDownloadCompletedEventArgs e)
        {
            _ = Task.Run(async () =>
            {


                var count = 0;
                var channels = 0;
                var memberList = new List<DiscordMember>();
                foreach (var guild in e.Guilds)
                {

                    count += guild.Value.MemberCount;
                    channels += guild.Value.Channels.Count;




                }





                List<string> _list = new List<string> {bot.ShardCount + " Shards!",e.Guilds.Count + " Guilds",
                count + " Users",channels + " Channels", "Some C# game", "Json is son of J", "Is Austria even a country", "Very first C# bot to use music player with interactable buttons!",
                "Watching you through my hidden camera","C#>Java","Do you hear the people sing?"};
                int _statusIndex = 0;

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


        public static Task OnMessageCreated(DiscordClient bot, MessageCreateEventArgs e)
        {
            _ = Task.Run(async () =>
            {

                var cnext = bot.GetCommandsNext();
                var msg = e.Message;
                var client = new MongoClient(Config.Get("uri"));
                var database = client.GetDatabase("Csharp");
                var collection = database.GetCollection<GuildPrefix>("guildprefixes");
                var filter = Builders<GuildPrefix>.Filter.Eq("_id", e.Guild.Id);
                var UserCollection = database.GetCollection<UserPrefix>("userprefixes");
                var Userfilter = Builders<UserPrefix>.Filter.Eq("_id", e.Author.Id);

               
                
                var match = await collection.FindAsync(filter);
                var matched = await match.FirstOrDefaultAsync();
                var UserMatch = await UserCollection.FindAsync(Userfilter);
                var UserMatched = await UserMatch.FirstOrDefaultAsync();

                int GuildPrefix = -1;
                int UserPrefix = -1;
                if (matched != null)
                {
                   GuildPrefix = msg.GetStringPrefixLength(matched.Prefix, StringComparison.InvariantCultureIgnoreCase);
                   
                }
                if(UserMatched != null)
                {
                    
                    UserPrefix = msg.GetStringPrefixLength(UserMatched.Prefix,StringComparison.InvariantCultureIgnoreCase);
                    
                }


                int cmdStart = msg.GetStringPrefixLength(Config.Get("prefix"), StringComparison.InvariantCultureIgnoreCase);
                if (cmdStart == -1 && GuildPrefix == -1 && UserPrefix == -1) await Task.CompletedTask;

                string prefix = "";
                string cmdString = "";
               
                if (GuildPrefix >0 )
                {
                    prefix = msg.Content.Substring(0, GuildPrefix);

                    cmdString = msg.Content.Substring(GuildPrefix);
                }
               else if(UserPrefix > 0)
                {
                    
                    prefix = msg.Content.Substring(0, UserPrefix);
                    cmdString = msg.Content.Substring(UserPrefix);
                }
                else
                {
                     prefix = msg.Content.Substring(0, cmdStart);
                     cmdString = msg.Content.Substring(cmdStart);
                }

               



                var command = cnext.FindCommand(cmdString, out var args);
                if (command == null) await Task.CompletedTask;

                var ctx = cnext.CreateContext(msg, prefix, command, args);
               await Task.Run(async () => await cnext.ExecuteCommandAsync(ctx));


            });


               


            return Task.CompletedTask;
        }










        public static async Task OnError(CommandsNextExtension _, CommandErrorEventArgs e)
        {
            Console.WriteLine(e.Exception);
            var ctx = e.Context;

            if (e.Exception.GetType().ToString() == "DSharpPlus.CommandsNext.Exceptions.ChecksFailedException")
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
                    }

                }


            }
            if (e.Exception.GetType().ToString() == "DSharpPlus.CommandsNext.Exceptions.CommandNotFoundException")
            {



                await ctx.RespondAsync("Command doesnt exist");
            }

            if (e.Exception.GetType().ToString() == "System.ArgumentException")
            {


                var sb = new StringBuilder();
            var attr =   (CooldownAttribute) e.Command.CustomAttributes.FirstOrDefault(x => x is CooldownAttribute);
               
               

                sb.Append("You can use one of following options:").Append(" ").Append("\n");

                var result = e.Command.Overloads;

                for (int i = 0; i < result.Count; i++)
                {
                    sb.Append($"{i + 1} option:");
                    foreach (var overload in result[i].Arguments)
                    {
                        sb.Append(" ").Append($"{overload.Name}").Append(" ").Append($"<{ overload.Description}>").Append(" ");
                    }
                    sb.Append("\n");


                }



                await ctx.RespondAsync(sb.ToString());
                










            }





        }






    }
}



