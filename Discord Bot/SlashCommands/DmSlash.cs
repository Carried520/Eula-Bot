using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class DmSlash : ApplicationCommandModule
    {
        [SlashCommand("dm","dm someone")]
        public async Task Dm(InteractionContext ctx , [Option("user","user that is in the guild")] DiscordUser user , [Option("content","content to send")] string content)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var member = (DiscordMember)user;
            await member.CreateDmChannelAsync().Result.SendMessageAsync(content);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Dm sent"));
        }
    }
}
