using Discord_Bot.Attributes;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{

    class Join : BaseCommandModule
    {

        [Command("join")]
        [Description("Joins a channel")]
        [Category("music")]
        public async Task JoinCommand(CommandContext ctx)
        {
            var link = ctx.Client.GetLavalink();
            if (!link.ConnectedNodes.Any())
            {
                await ctx.RespondAsync("The Lavalink connection is not established");
                return;
            }
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            var node = link.ConnectedNodes.Values.First();
            var channel = ctx.Member.VoiceState.Channel;
            if (channel.Type != ChannelType.Voice)
            {
                await ctx.RespondAsync("Not a valid voice channel.");
                return;
            }

            await node.ConnectAsync(channel);
            await ctx.RespondAsync($"Joined {channel.Name}!");


        }



        [Command("leave")]
        [Description("Leave a channel")]
        [Category("music")]

        public async Task LeaveCommand(CommandContext ctx)
        {

            var link = ctx.Client.GetLavalink();
            if (!link.ConnectedNodes.Any())
            {
                await ctx.RespondAsync("The Lavalink connection is not established");
                return;
            }
            var node = link.ConnectedNodes.Values.First();
            var channel = ctx.Member.VoiceState.Channel;
            if (channel.Type != ChannelType.Voice || channel==null)
            {
                await ctx.RespondAsync("Not a valid voice channel.");
                return;
            }
            var conn = node.GetGuildConnection(channel.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            await conn.StopAsync();
            await conn.DisconnectAsync();
            await ctx.RespondAsync($"Left {channel.Name}!");
            


        }

       


    }
}
