using Discord_Bot.Attributes;
using Discord_Bot.Services;
using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Lavalink;
using DSharpPlus.Lavalink.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Play : BaseCommandModule
    {

        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        public MusicService MusicService { private get; set; }
       [Command("play")]
        [Category("music")]
        public async Task PlayCommand(CommandContext ctx,  [Description("Enter a phrase to look for relevant video")] params string[] search)
        {
           await MusicService.Play(ctx,search);
       }

       [Command("play")]
        [Description("Play music(supports playlists)")]
        [Priority(1)]
        [Category("music")]
        public async Task PlayUrl(CommandContext ctx, [Description("Provide link to video or playlist")] Uri uri)
        {
            await MusicService.PlayPlaylist(ctx, uri);
            
        }

        [Command("playlist")]
        [Description("Play music from saved playlist")]
        [Category("music")]
        public async Task PlayUrl(CommandContext ctx, string playlist)
        {
            await MusicService.PlaySavedPlaylist(ctx, playlist);

        }



        [Command("repeat")]
        [Description("Repeats current track")]
        [Category("music")]
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
                        else
                        {
                            await Task.Delay(TimeSpan.FromMinutes(5));
                            if (conn.CurrentState.CurrentTrack == null) await conn.DisconnectAsync();
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
                        await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
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
                        if (music.ContainsKey(e.Guild.Id))
                        {
                            var newQueue = music[e.Guild.Id];
                            newQueue.Clear();
                            music[e.Guild.Id] = newQueue;
                        }
                        await connected.StopAsync();

                        await e.Interaction.CreateFollowupMessageAsync(new DiscordFollowupMessageBuilder().WithContent("Queue cleared and player stopped"));
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
   
