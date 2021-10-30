using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class JoinSlash : ApplicationCommandModule
    {

        [SlashCommand("join","joins a channel")]
        
        public async Task Join(InteractionContext ctx)
        {


            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var link = ctx.Client.GetLavalink();
            if (!link.ConnectedNodes.Any())
            {
                await ctx.FollowUpAsync( new DiscordFollowupMessageBuilder().WithContent("The Lavalink connection is not established"));
                return;
            }
            var node = link.ConnectedNodes.Values.First();
            var channel = ctx.Member.VoiceState.Channel;
            if (channel.Type != ChannelType.Voice)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Not a valid voice channel"));
                return;
            }

            await node.ConnectAsync(channel);
            await  ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Joined {channel.Name}!"));


        }



        [SlashCommand("leave","leave a channel")]
        
        public async Task LeaveCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            var link = ctx.Client.GetLavalink();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            if (!link.ConnectedNodes.Any())
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("The Lavalink connection is not established"));
                return;
            }
            var node = link.ConnectedNodes.Values.First();
            if(ctx.Member.VoiceState == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You aren't connected to voice channel"));
                return;
                
            }
            var channel = ctx.Member.VoiceState.Channel;
            if (channel.Type != ChannelType.Voice || channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Not a valid voice channel"));
                return;
            }
            var conn = node.GetGuildConnection(channel.Guild);

            if (conn == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Lavalink is not connected"));
                return;
            }

            await conn.StopAsync();
            await conn.DisconnectAsync();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Left {channel.Name}"));
        }
    }
}
