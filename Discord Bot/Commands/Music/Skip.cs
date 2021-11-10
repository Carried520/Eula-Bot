using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class Skip : BaseCommandModule
    {
        [Command("skip")]
        [Description("skips a track")]
        [Category("music")]
        public async Task StopCommand (CommandContext ctx)
        {

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            
            
            
            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }
           if(conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("Player isnt playing now");
            }
           await conn.StopAsync();
           
           
        }
    }
}
