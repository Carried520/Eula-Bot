using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class Ask : BaseCommandModule
    {
        [Command("askaquestion")]
        [Description("ask your question")]
        [Category("rp")]
        public async Task AskCommand(CommandContext ctx,[RemainingText] string content)
        {
          var interact =  ctx.Client.GetInteractivity();
            DiscordEmoji[] emojis = new DiscordEmoji[]{
                 DiscordEmoji.FromName(ctx.Client,":white_check_mark:",false),
                 DiscordEmoji.FromName(ctx.Client,":x:",false)
            };
            var message = await ctx.Channel.SendMessageAsync($"Do you agree with : ```{content}```");
            

            
            var poll = await interact.DoPollAsync(message, emojis, DSharpPlus.Interactivity.Enums.PollBehaviour.DeleteEmojis, TimeSpan.FromSeconds(30));
            var yes = poll[0].Voted.Count;
            var no = poll[1].Voted.Count;
            
            

           
            if (yes > no)
            {
                await ctx.Channel.SendMessageAsync("Most ppl agree!");
            } else if(yes == no)
            {
                await ctx.Channel.SendMessageAsync("Undecided-Same number of votes for yes and no");
            }
            else if(yes<no)
            {
                await ctx.Channel.SendMessageAsync("Most ppl disagree!");
            }
           
            
            
            
        }
    }
}
