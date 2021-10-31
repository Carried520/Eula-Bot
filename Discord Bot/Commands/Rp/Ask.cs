using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
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
         
           var message = await ctx.Channel.SendMessageAsync($"Do you agree with : ```{content}```");
            DiscordEmoji[] emojis =  new DiscordEmoji[]{
                 DiscordEmoji.FromName(ctx.Client,":white_check_mark:",false),
                 DiscordEmoji.FromName(ctx.Client,":x:",false)
            };

            foreach (var emoji in emojis)
            {
                Console.WriteLine(emoji.Name.ToString());
            }
            var poll = await interact.DoPollAsync(message, emojis, DSharpPlus.Interactivity.Enums.PollBehaviour.DeleteEmojis, TimeSpan.FromSeconds(30));
            var yes = poll[0].Total;
            var no = poll[1].Total;
            Console.WriteLine(yes);
            

            
            Console.WriteLine(yes);
            Console.WriteLine(no);
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
