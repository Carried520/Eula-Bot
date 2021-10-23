using Discord_Bot.Utils;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Discord_Bot.SlashCommands
{
    class RedditSlash : ApplicationCommandModule
    {
        [SlashCommand("reddit","reddit command")]
        public async Task RedditCommand(InteractionContext ctx , [Option("subreddit","subreddit to search")] string subreddit)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            RedditAuth authorize = new RedditAuth();
            var token = authorize.GetAuth().Result.ToString();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("User-Agent", "Eula Bot");
            String url = "https://oauth.reddit.com/r/" + subreddit + "/new";
            var success = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(success);
            JsonElement root = json.RootElement;
            JsonElement results = root.GetProperty("data");


            var nsfw = results.GetProperty("children")[0].GetProperty("data").GetProperty("over_18").GetBoolean();
            if (nsfw && !ctx.Channel.IsNSFW)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Reddit is nsfw and you arent in nsfw channel"));
                return;
            }
            var count = results.GetProperty("children").GetArrayLength();
            var random = new Random().Next(0, count);
            string result = "";
            try
            {

                result = results.GetProperty("children")[random].GetProperty("data").GetProperty("preview").GetProperty("images")[0].GetProperty("source").GetProperty("url").ToString();
                var decode = HttpUtility.HtmlDecode(result);
                var embed = new DiscordEmbedBuilder()
                    .WithImageUrl(decode)
                    .Build();
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
            }
            catch (KeyNotFoundException)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Post without image try again"));
            }
        }
    }
}
