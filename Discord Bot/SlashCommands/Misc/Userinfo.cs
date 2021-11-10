using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class Userinfo : ApplicationCommandModule
    {
        [SlashCommand("userinfo", "Info about given user")]
        public async Task User(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser member = null)
        {
            if(member == null) member = ctx.User;
            
            var embed = new DiscordEmbedBuilder();
            embed.AddField("User", $"{member.Username}#{member.Discriminator.ToString()}", false)
            .AddField("Discriminator", $"{member.Discriminator.ToString()}", false)
            .AddField("Is user Bot", $"{member.IsBot}", false)
            .AddField("Created on", $"{member.CreationTimestamp}", false)
           
           .WithImageUrl($"{member.AvatarUrl}")
            .WithFooter($"Powered by C#(not this shitty Java) ")
            .WithTimestamp(DateTime.Now.ToLocalTime())
            .WithTitle("Userinfo");
            
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed.Build()));
        }
    }
}
