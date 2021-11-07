using Discord_Bot.Attributes;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class Player : BaseCommandModule
    {

        [Command("player")]
        [Description("Brings up  music player controls")]
        [Category("music")]
        public async Task PlayerCommand(CommandContext ctx)
        {
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            if (conn == null)
            {
                await ctx.RespondAsync("What for? Bot isnt connected");
                return;
            }
            if (conn.CurrentState.CurrentTrack == null)
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
    }
}
