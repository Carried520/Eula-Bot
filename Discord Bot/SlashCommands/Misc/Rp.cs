using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands
{
    [SlashCommandGroup("rp","rp commands")]
    class Rp : ApplicationCommandModule
    {
        
        [SlashCommand("kiss","kiss someone")]
        public async Task Kiss (InteractionContext ctx , [Option("user","mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var webclient = new WebClient().DownloadString("https://purrbot.site/api/img/sfw/kiss/gif");
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty("link");

            var embed = new DiscordEmbedBuilder()
                .WithDescription($"{ctx.Member.Mention} kisses {user.Mention} ")
                .WithImageUrl(results.GetString())
                .Build();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }

        [SlashCommand("pat", "Pat someone")]
        public async Task Pat(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var webclient = new WebClient().DownloadString("https://purrbot.site/api/img/sfw/pat/gif");
            JsonDocument json = JsonDocument.Parse(webclient);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty("link");

            var embed = new DiscordEmbedBuilder()
                .WithDescription($"{ctx.Member.Mention} pats {user.Mention} ")
                .WithImageUrl(results.GetString())
                .Build();
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
        }

    }
}
