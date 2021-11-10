using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Discord_Bot.Attributes
{
    class SlashRequireOtherUserAttribute : SlashCheckBaseAttribute
    {
        

        public override  Task<bool> ExecuteChecksAsync(InteractionContext ctx)
        {
            var user = ctx.ResolvedUserMentions[0];
            if (user == ctx.User) return Task.FromResult(false);
            else return Task.FromResult(true);
        }

        
    }
}
