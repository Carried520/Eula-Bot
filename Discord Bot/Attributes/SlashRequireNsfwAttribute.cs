using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Discord_Bot.Attributes
{
    class SlashRequireNsfwAttribute : SlashCheckBaseAttribute
    {
        

        public override  Task<bool> ExecuteChecksAsync(InteractionContext ctx)
        {
            return Task.FromResult(ctx.Channel.IsNSFW);
        }
    }
}
