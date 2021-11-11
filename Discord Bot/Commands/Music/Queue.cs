using Discord_Bot.Attributes;
using Discord_Bot.Services;
using Discord_Bot.Utils;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace Discord_Bot.Commands.Music
{
    class Queue : BaseCommandModule
    {
        public MusicService MusicService{ private get; set; }
        [Command("queue")]
        [Description("Shows queue")]
        [Category("music")]

        public async Task GetQueue(CommandContext ctx)
        {
            try
            {
                var music = MusicService.music;
                var interact = ctx.Client.GetInteractivity();
                StringBuilder sb = new StringBuilder();
                StringBuilder GetAuthor = new StringBuilder();
                StringBuilder GetTitle = new StringBuilder();
                var embed = new DiscordEmbedBuilder();

                var pageCount = music[ctx.Guild.Id].Count / 20 + 1;
                if (music[ctx.Guild.Id].Count % 20 == 0) pageCount--;
                var pages = music[ctx.Guild.Id]
                     .Select((t, i) => new { title = t, index = i })
                 .GroupBy(x => x.index / 20)
                 .Select(page => new Page("",
                 new DiscordEmbedBuilder
                 {
                     Title = "Playlist",
                     Description = $"\n{string.Join("\n", page.Select(track => $"`{track.index + 1:00}` {track.title.Title}"))}",
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

                
                await interact.SendPaginatedMessageAsync(ctx.Channel, ctx.User, pages, emojis, PaginationBehaviour.Ignore, PaginationDeletion.KeepEmojis, TimeSpan.FromHours(1));

            }
            catch (Exception)
            {

                await ctx.Channel.SendMessageAsync("No queue is running");
            }
        }
    }
}
