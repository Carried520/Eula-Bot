using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class Guildmission : ApplicationCommandModule
    {

        [SlashCommand("guild","Guildmission tool")]
        
        public async Task DeleteCommand(InteractionContext ctx, [Option("amount", "Amount of mobs")] long amount , [Option("mobs","what kind of mobs")] string content,
         [Choice("smh",0.5)]
           [Choice("regular",1.0)]
           [Choice("boss",2.0)]
            [Option("missiontype", "type of misssion(regular,smh,boss")] double mission , [Option("family", "players that contributed to guild mission")] string family  )
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
            string trim = Regex.Replace(family, @"\s+", "");
            string[] users = trim.Split(",");
            var builder = new StringBuilder();
            Console.WriteLine(mission);
            foreach(var value in users)
            {
                Console.WriteLine(value);
                builder.Append("\n" + value).Append(" ").Append($"0->{mission.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
            }
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"```{builder}```"));


        }
    }
}
