using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    [SlashCommandGroup("pets","pets")]
    class Pets : ApplicationCommandModule
    {
        [SlashCommand("bird","send a pic of bird and share random fact")]
        public async Task Bird (InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var webclient = new WebClient().DownloadString("https://some-random-api.ml/animal/birb");
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            var embed = new DiscordEmbedBuilder()
                 .WithDescription(root.GetProperty("fact").GetString())
                 .WithImageUrl(root.GetProperty("image").GetString())
                 .Build();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }


        [SlashCommand("fox", "send a pic of fox and share random fact")]
        public async Task Fox(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var webclient = new WebClient().DownloadString("https://some-random-api.ml/animal/fox");
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            var embed = new DiscordEmbedBuilder()
                 .WithDescription(root.GetProperty("fact").GetString())
                 .WithImageUrl(root.GetProperty("image").GetString())
                 .Build();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }

        [SlashCommand("dog", "send a pic of dog and share random fact")]
        public async Task Dog(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var webclient = new WebClient().DownloadString("https://some-random-api.ml/animal/dog");
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            var embed = new DiscordEmbedBuilder()
                 .WithDescription(root.GetProperty("fact").GetString())
                 .WithImageUrl(root.GetProperty("image").GetString())
                 .Build();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }
    }
}
