using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Attributes
{
    class RequireCertainGuildAttribute : CheckBaseAttribute
    {
        public  ulong GuildId;
        public RequireCertainGuildAttribute(ulong guildId)
        {
            this.GuildId = guildId;
        }

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            bool isGuild = ctx.Guild.Id == GuildId;
            return Task.FromResult(isGuild);
        }

        
    }
}
