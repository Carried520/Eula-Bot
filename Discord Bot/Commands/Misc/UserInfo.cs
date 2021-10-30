using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    class UserInfo : BaseCommandModule
    {

        public DiscordEmbedBuilder Embed(DiscordMember member)
        {
            var embed = new DiscordEmbedBuilder();  
            embed.AddField("User", $"{member.DisplayName}#{member.Discriminator.ToString()}", false)
            .AddField("Discriminator", $"{member.Discriminator.ToString()}", false)
            .AddField("Is user Bot", $"{member.IsBot}", false)
            .AddField("Created on", $"{member.CreationTimestamp}", false)
           .AddField("Joined Server on", $"{member.JoinedAt}", false)
           .WithImageUrl($"{member.AvatarUrl}")
            .WithFooter($"Powered by C#(not this shitty Java) ")
            .WithTimestamp(DateTime.Now)
            .WithTitle("Userinfo");
            return embed;

        }


        [Command("userinfo")]
        [Description("Info about users")]
        
        public async Task User(CommandContext ctx,[Description("Optional argument - mention a user otherwise command will get your info")]  DiscordMember member)
        {
            
            
            await ctx.Channel.SendMessageAsync(Embed(member).Build());
        }

       [Command("userinfo")]
        

        public async Task SelfUser(CommandContext ctx)
        {
            var member = ctx.Member;
            await ctx.Channel.SendMessageAsync(Embed(member).Build());
        }
    }
}
