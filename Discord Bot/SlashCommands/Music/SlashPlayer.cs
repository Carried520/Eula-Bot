using Discord_Bot.Utils;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace Discord_Bot.SlashCommands.Music
{
    
    class SlashPlayer : ApplicationCommandModule
    {
        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        [SlashCommand("stop","stops a player")]
        public async Task Stop(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            

            if ( conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected."));
                return;
            }
            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not playing."));
                return;
            }
            if (music.ContainsKey(ctx.Guild.Id))
            {
                var newQueue = music[ctx.Guild.Id];
                newQueue.Clear();
                music[ctx.Guild.Id] = newQueue;
            }
            
            await conn.StopAsync();
            
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Cleared queue and stopped the player"));
        }

        [SlashCommand("resume","resume a player")]

        public async Task ResumeCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected."));
                return;
            }
            await conn.ResumeAsync();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Playback resumed")).Result.DeleteAsync();
        }


        [SlashCommand("volume", "set volume")]
        public async Task Volume(InteractionContext ctx, [Option("volume", "volume to set")] long volume)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("I'm not connected to voice chat"));
                return;
            }
            await conn.SetVolumeAsync(Convert.ToInt32(volume));
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Set volume to {volume}"));
        }

        [SlashCommand("queue","show queue")]
        public async Task QueueCommand(InteractionContext ctx)
        {
            try
            {
                await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
                var interact = ctx.Client.GetInteractivity();
                StringBuilder sb = new StringBuilder();
                StringBuilder GetAuthor = new StringBuilder();
                StringBuilder GetTitle = new StringBuilder();
                var embed = new DiscordEmbedBuilder();

                var pageCount = music[ctx.Guild.Id].Count / 20 + 1;
                if (music[ctx.Guild.Id].Count % 20 == 0) pageCount--;
                var pages = music[ctx.Guild.Id]
                     .Select((t, i) => new { name = t, index = i })
                 .GroupBy(x => x.index / 20)
                 .Select(page => new Page("",
                 new DiscordEmbedBuilder
                 {
                     Title = "Playlist",
                     Description = $"\n{string.Join("\n", page.Select(track => $"`{track.index + 1:00}` {track.name.Title}"))}",
                     Footer = new EmbedFooter
                     {
                         Text = $"Page {page.Key + 1}/{pageCount}"
                     }
                 }
                 )).ToArray();

                var emojis = new PaginationEmojis
                {
                    SkipLeft = null,
                    SkipRight = null,
                    Stop = null,
                    Left = DiscordEmoji.FromUnicode("◀"),
                    Right = DiscordEmoji.FromUnicode("▶")
                };
               
                
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Queue"));
                await interact.SendPaginatedMessageAsync(ctx.Channel, ctx.User, pages, emojis, PaginationBehaviour.Ignore, PaginationDeletion.KeepEmojis, TimeSpan.FromHours(1));

            }
            catch (Exception)
            {
               await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Are u an actual baka no queue is running"));
                
            }
        }

        [SlashCommand("track","shows current track")]
        public async Task TrackCommand(InteractionContext ctx)
        {

            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected."));
                return;
            }
            if(conn.CurrentState.CurrentTrack == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Not track playing at the moment."));
                return;
            }

            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Current track : {conn.CurrentState.CurrentTrack.Title} at " +
                $"{(Convert.ToInt32(conn.CurrentState.PlaybackPosition.TotalSeconds / 60))} minutes {Convert.ToInt32(conn.CurrentState.PlaybackPosition.TotalSeconds % 60)} seconds"));
            


        }

        [SlashCommand("shuffle","shuffle a playlist")]
        public async Task  Shuffle(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            if (music.ContainsKey(ctx.Guild.Id)) return;
            var newQueue = music[ctx.Guild.Id];
            if (!newQueue.Any()) return;
            newQueue.Shuffle();
            music[ctx.Guild.Id] = newQueue;
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Shuffled queue"));
        }

        [SlashCommand("skip","skip a track")]
        public async Task Skip(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected."));
                return;
            }
            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Player isnt playing now"));
                return;
            }
            await conn.StopAsync();
        }
    }
}

