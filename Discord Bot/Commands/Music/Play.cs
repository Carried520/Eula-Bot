using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Lavalink.EventArgs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Play : BaseCommandModule
    {
       public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
       [Command("play")]
       public async Task PlayCommand(CommandContext ctx,  [Description("Enter a phrase to look for relevant video")] params string[] search)
        {
           StringBuilder sb = new StringBuilder();
            for (int i = 0; i < search.Length; i++)
            {
                sb.Append(search[i]).Append(" ");
            }
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();

           if (node.GetGuildConnection(ctx.Member.VoiceState.Guild) == null)
            {
                var link = ctx.Client.GetLavalink();
                if (!link.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }
                var nodey = link.ConnectedNodes.Values.First();
                var channel = ctx.Member.VoiceState.Channel;
                if (channel.Type != ChannelType.Voice)
                {
                    await ctx.RespondAsync("Not a valid voice channel.");
                    return;
                }
               await nodey.ConnectAsync(channel);
            }
           var loadResult = await node.Rest.GetTracksAsync(sb.ToString());

           if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Track search failed for {search}.");
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
           var track = loadResult.Tracks.First();

           if (conn.CurrentState.CurrentTrack == null)
            {
               var builder = new DiscordMessageBuilder()
                   .WithContent("Player")
                   .AddComponents(new DiscordComponent[]
                   {
                      new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                  });
                await ctx.Channel.SendMessageAsync(builder);
               await conn.PlayAsync(track);
            }
            else
            {
               if (!music.ContainsKey(ctx.Guild.Id))
                {
                    List<LavalinkTrack> queue = new List<LavalinkTrack>();
                    queue.Clear();
                    queue.Add(track);
                    music.Add(ctx.Guild.Id, queue);
                }
                else
                {
                    var newQueue = music[ctx.Guild.Id];
                   newQueue.Add(track);
                    music[ctx.Guild.Id] = newQueue;
               }
               await ctx.RespondAsync($"{track.Title} added to queue");
           }
       }

       [Command("play")]
        [Description("Play music(supports playlists)")]
        [PriorityAttribute(1)]
       public async Task PlayUrl(CommandContext ctx, [Description("Provide link to video or playlist")] Uri uri)
        {
            
            var lava = ctx.Client.GetLavalink();
            if (!lava.ConnectedNodes.Any())
            {
                await ctx.RespondAsync("The Lavalink connection is not established");
                return;
            }
           var node = lava.ConnectedNodes.Values.First();
           var connect =  node.ConnectAsync(ctx.Member.VoiceState.Channel).Result;
           var loadResult = node.Rest.GetTracksAsync(uri).Result.Tracks.ToList();
           if(connect.CurrentState.CurrentTrack == null)
            {
                var builder = new DiscordMessageBuilder()
                   .WithContent("Player")
                   .AddComponents(new DiscordComponent[]
                   {
                       new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                  });
                await ctx.Channel.SendMessageAsync(builder);
           }
           foreach (var track in loadResult)
            {
               if (connect.CurrentState.CurrentTrack == null)
                {
                    await connect.PlayAsync(track);
                } 
                else
                {
                   if (!music.ContainsKey(ctx.Guild.Id))
                    {
                        List<LavalinkTrack> queue = new List<LavalinkTrack>();
                        queue.Clear();
                        queue.Add(track);
                        music.Add(ctx.Guild.Id, queue);
                    }
                    else
                    {
                        var newQueue = music[ctx.Guild.Id];
                       newQueue.Add(track);
                        music[ctx.Guild.Id] = newQueue;
                   }
                }
            }  
        }

       [Command("repeat")]
        [Description("Repeats current track")]
        public async Task Repeat(CommandContext ctx)
        {
           var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            var track = conn.CurrentState.CurrentTrack;
           if (!music.ContainsKey(ctx.Guild.Id))
            {
                List<LavalinkTrack> queue = new List<LavalinkTrack>
                        {
                            track
                        };
                music.Add(ctx.Guild.Id, queue);
            }
            else
            {
                var newQueue = music[ctx.Guild.Id];
                newQueue.Insert(0, track);
                music[ctx.Guild.Id] = newQueue;
                await ctx.RespondAsync("Repeating");
            }
       }
        [Command("player")]
        [Description("Brings up  music player controls")]
        public async Task Player(CommandContext ctx)
        {
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
               if(conn == null)
            {
                await ctx.RespondAsync("What for? Bot isnt connected");
                    return;
            }
               if(conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("What for? No track is playing");
                return;
            }
           var builder = new DiscordMessageBuilder()
                  .WithContent("Player")
                  .AddComponents(new DiscordComponent[]
                  {
                       new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                 });
            await ctx.Channel.SendMessageAsync(builder);
       }
       public static  Task PlayInGuild(LavalinkGuildConnection conn , TrackFinishEventArgs e)
        {


            _ = Task.Run(async () => {
                if (music.ContainsKey(conn.Guild.Id))
                {
                    if (conn.CurrentState.CurrentTrack == null)
                    {
                        var CurrentQueue = music[conn.Guild.Id];
                        if (CurrentQueue.Any())
                        {
                            await conn.PlayAsync(CurrentQueue[0]);
                            CurrentQueue.RemoveAt(0);
                            music[conn.Guild.Id] = CurrentQueue;
                        }



                    }

                }
            });
            return Task.CompletedTask;
               
                
                
        }




        public static async Task OnClick(DiscordClient bot, ComponentInteractionCreateEventArgs e)
        {
            _ = Task.Run(async () => {
               var lavalink = bot.GetLavalink();
                var nodes = lavalink.ConnectedNodes.Values.First();
                var connected = nodes.GetGuildConnection(e.Guild);
                switch (e.Id)
                {
                    case "stop":
                       if (connected == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Im not connected"));
                            return;
                        }
                        if (connected.CurrentState.CurrentTrack == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Im not playing now"));
                            return;
                        }
                        await connected.StopAsync();
                        if (!music.ContainsKey(e.Guild.Id)) return;
                        var newQueue = music[e.Guild.Id];
                        newQueue.Clear();
                        music[e.Guild.Id] = newQueue;
                       await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("Queue cleared and player stopped"));
                        break;
                   case "resume":
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                       if (connected == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink not connected"));
                            return;
                        }
                        else
                        {
                            await connected.ResumeAsync();
                         var respond = await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Resumed player"));
                            await Task.Delay(5000);
                            await respond.DeleteAsync();
                        }
                        break;
                    case "pause":
                       await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (connected == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected"));
                            return;
                        }
                      else  if (connected.CurrentState.CurrentTrack == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("There are no tracks loaded"));
                            return;
                        }
                        else
                        {
                            await connected.PauseAsync();
                            var responded = await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Paused"));
                            await Task.Delay(5000);
                            await responded.DeleteAsync();
                       }
                        break;
                   case "repeat":
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (connected == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected"));
                            return;
                        }
                        else if (connected.CurrentState.CurrentTrack == null)
                        {
                             await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent($"No tracks loaded"));
                       }
                        else
                        {
                            var track = connected.CurrentState.CurrentTrack;
                            if (!music.ContainsKey(e.Guild.Id))
                            {
                                List<LavalinkTrack> queue = new List<LavalinkTrack>
                        {
                            track
                        };
                                music.Add(e.Guild.Id, queue);
                                var response2 = await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent($"Repeating track - {connected.CurrentState.CurrentTrack.Title}"));
                                await response2.DeleteAsync();
                            }
                            else
                            {
                                var newestQueue = music[e.Guild.Id];
                                newestQueue.Insert(0, track);
                                music[e.Guild.Id] = newestQueue;
                            var response3 =    await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent($"Repeating track - {connected.CurrentState.CurrentTrack.Title}"));
                                await Task.Delay(5000);
                                await  response3.DeleteAsync();
                            }
                        }
                        break;
                    case "skip":
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                        if (connected == null)
                        {
                           await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected"));
                           return;
                        }
                        if (connected.CurrentState.CurrentTrack == null)
                        {
                            await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Player isnt playing"));
                            return;
                        }
                       await connected.StopAsync();
                     var response4 = await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent($"Skipping track"));
                        await Task.Delay(5000);
                        await response4.DeleteAsync();
                        break;
                }
            });
            await Task.CompletedTask;   
            }
        }
    }
   
