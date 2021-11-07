using Discord_Bot.Attributes;
using Discord_Bot.Services;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
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

        public SlashService Slash { private get;  set; }
        
        [SlashCommand("kiss","kiss someone")]
        public async Task Kiss (InteractionContext ctx , [Option("user","mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/sfw/kiss/gif", "link", "kisses", user);
           
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
           
        }

        [SlashCommand("pat", "Pat someone")]
        public async Task Pat(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/sfw/pat/gif", "link", "pats", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));
            
        }

        [SlashCommand("fuck","fuck someone")]
        [SlashRequireNsfw]
        public async Task Fuck(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/fuck/gif","link","fucks",user);   
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }


        [SlashCommand("cum", "cum in someone")]
        [SlashRequireNsfw]
        public async Task Cum(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/cum/gif", "link", "cums in", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }


        [SlashCommand("blowjob", "blow someone")]
        [SlashRequireNsfw]
        public async Task Blowjob(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/blowjob/gif", "link", "blows", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }


        [SlashCommand("anal", "anal fuck")]
        [SlashRequireNsfw]
        public async Task Anal(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/anal/gif", "link", "anal fucks", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }


        [SlashCommand("yaoi", "yaoi fuck")]
        [SlashRequireNsfw]
        public async Task Yaoi(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/yaoi/gif", "link", "yaoi fucks", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }


        [SlashCommand("yuri", "yuri fuck")]
        [SlashRequireNsfw]
        public async Task Yuri(InteractionContext ctx, [Option("user", "mention a user")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            var embed = Slash.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/yuri/gif", "link", "yuri fucks", user);
            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().AddEmbed(embed));

        }








    }
}
