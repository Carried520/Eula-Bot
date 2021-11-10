using Discord_Bot.Attributes;
using Discord_Bot.Commands.Activities.ActivityMaker;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities
{
    class Shop : BaseCommandModule
    {
        public class CharData
        {

            public ulong Id { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public int Ap { get; set; }
            public int Dp { get; set; }
            public long Silver { get; set; }

        }


        public class ShopData
        {

            public ulong Id { get; set; }
            public List<string> items { get; set; }

        }

        [Command("shop")]
        [Category("activity")]
        [Description("Buy something from shop idk")]
        public async Task ShopCommand(CommandContext ctx,string item)
        {
            var id = ctx.Member.Id;
            var ArrayOfItems = new string[] {"rosar","vangertz","lemoria","dimtree","blackstar","fallengod","kutum"};
            if (!ArrayOfItems.Contains(item.ToLower()))
            {
                await ctx.RespondAsync("Not a valid item");
                return;
            }






            var msg = new ShopItems().InsertShopItemAsync(ctx, id, item);
            if (!string.IsNullOrEmpty(msg.Result))
            {
                await ctx.RespondAsync(msg.Result);
            }
            


        }

        [Command("shop")]
        [Category("activity")]
        [Description("Buy something from shop idk")]
        public async Task ShopList(CommandContext ctx)
        {
            var ArrayOfItems = new string[] {"rosar","vangertz","lemoria","dimtree", "blackstar", "fallengod", "kutum" };
            var NameBuilder = new StringBuilder();
            var ApBuilder = new StringBuilder();
            var CostBuilder = new StringBuilder();
            var DpBuilder = new StringBuilder();
            
            var embed = new DiscordEmbedBuilder();
            var shop = new ShopItems();
            var builder = new DiscordMessageBuilder();
            foreach(var item in ArrayOfItems)
            {
                var iteminfo = shop.GetItemStatsAsync(item).Result;
                NameBuilder.Append($"\n{item}");
                ApBuilder.Append($"\n{iteminfo[1]}")
                .Append($"/{iteminfo[2]}");
                if (iteminfo[0] > 1000000000)
                {
                    CostBuilder.Append($"\n{iteminfo[0] / 1000000000}b");
                } else
                {
                    CostBuilder.Append($"\n{iteminfo[0] / 1000000}m");
                }
                
            }
            embed.AddField("Name", NameBuilder.ToString(), true)
                .AddField("Ap/Dp", $"{ApBuilder}", true)
     
                .AddField("Cost", CostBuilder.ToString(), true);
            await ctx.RespondAsync(builder.WithContent("Use this command with one of items below").WithEmbed(embed.Build()));

            
        }
    }
}
