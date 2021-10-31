using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class FollowChannel : BaseCommandModule
    {
        [Command("followchannel")]
        [Description("follow #eula-news channel")]
        [RequirePermissions(DSharpPlus.Permissions.ManageChannels)]
        [Category("misc")]
        public async Task Follow (CommandContext ctx)
        {
            var NewsChannel = await ctx.Client.GetChannelAsync(903765976166826055L);
            var OutChannel = await ctx.Client.GetChannelAsync(ctx.Channel.Id);
            await ctx.Channel.SendMessageAsync("Following #eula-news channel");
            await NewsChannel.FollowAsync(OutChannel);
            
        }
    }
}
