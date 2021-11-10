using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    class Faq : ApplicationCommandModule
    {
        [SlashCommand("faq","faq about bot")]
        public async Task AskCommand(InteractionContext ctx)
        {
            var embed = new DiscordEmbedBuilder()
                .AddField("**How do I run a normal command**", "Its simple just use e!help for list of commands", false)
                .AddField("**How do I run slash command(application command)**","Type /(slash on keyboard) list of commands should appear",false)
                .AddField("**How do I control bot music by using buttons**","On playing first song in queue music player should appear.You can always summon the player" +
                "by typing e!player",false)
                .AddField("**Why cant I run nsfw command?**","You have to be in nsfw channel",false)
                .AddField("**Will you implement x feature?**","Sure just dm me",false)
                .Build();

            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource,new DiscordInteractionResponseBuilder().AddEmbed(embed));
                
        }
    }
}
