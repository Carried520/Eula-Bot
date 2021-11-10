using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Resume : BaseCommandModule
    {
        [Command("resume")]
        [Description("resume player")]
        [Category("music")]
        public async Task ResumeCommand(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            } 
            

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }
            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("There is no track playing");
                return;
            }
          await conn.ResumeAsync();
            
        
            
            
            
            

        }


        
    }
}
