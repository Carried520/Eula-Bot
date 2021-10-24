using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class TimerSlash : ApplicationCommandModule
    {
        [SlashCommand("timer","set a timer")]
        public async Task Timer (InteractionContext ctx , [Choice("seconds","seconds")] [Choice("minutes","minutes")] [Option("units", "Seconds,Minutes")] string units , [Option("value","amount of time")] double value)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var time = TimeSpan.FromSeconds(0);


            if (units == "minutes" && value >= 15)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"You can't put 15 minutes or more"));
                return;
            }

            if (units == "seconds")
            {
                time = TimeSpan.FromSeconds(value);
            } else if(units == "minutes")
            {
                time = TimeSpan.FromMinutes(value);
            } 
            await Task.Delay(time);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"{ctx.Member.Mention} {value} {units} have passed"));
           



        }
    }
}
