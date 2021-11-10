using Discord_Bot.Attributes;
using Discord_Bot.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Rp
{
    class RpCommands : BaseCommandModule
    {
        public DiscordService Discord { private get; set; }

        [Command("kiss")]
        [Category("rp")]
        [Description("kiss")]
        public async Task Kiss(CommandContext ctx,DiscordMember user)
        {
           
            await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/sfw/kiss/gif", "link", "kisses", user);
            
            

        }

        [Command("pat")]
        [Category("rp")]
        [Description("pat")]
        public async Task Pat(CommandContext ctx,DiscordMember user)
        {
            

           await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/sfw/pat/gif", "link", "pats", user);
            

        }

        [Command("fuck")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("fuck")]
        public async Task Fuck(CommandContext ctx, DiscordMember user)
        {
           
            await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/fuck/gif", "link", "fucks", user);
            

        }


        [Command("cum")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("cum")]
        public async Task Cum(CommandContext ctx, DiscordMember user)
        {
            
             await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/cum/gif", "link", "cums in", user);
            

        }


        [Command("blowjob")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("blowjob")]
        public async Task Blowjob(CommandContext ctx, DiscordMember user)
        {
            
           await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/blowjob/gif", "link", "blows", user);
            

        }


        [Command("anal")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("anal")]
        public async Task Anal(CommandContext ctx, DiscordMember user)
        {
           
            await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/anal/gif", "link", "anal fucks", user);
            

        }


        [Command("yaoi")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("yaoi")]
        public async Task Yaoi(CommandContext ctx, DiscordMember user)
        {
            
            await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/yaoi/gif", "link", "yaoi fucks", user);
            

        }


        [Command("yuri")]
        [Category("nsfw")]
        [RequireNsfw]
        [Description("yuri")]
        public async Task Yuri(CommandContext ctx, DiscordMember user)
        {
            
            await Discord.SendRpEmbed(ctx, "https://purrbot.site/api/img/nsfw/yuri/gif", "link", "yuri fucks", user);
           

        }
    }
}
