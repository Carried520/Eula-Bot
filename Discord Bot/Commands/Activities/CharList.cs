using Discord_Bot.Attributes;
using Discord_Bot.Commands.Activities.ActivityMaker;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities
{
    class CharList : BaseCommandModule
    {
        [Command("charlist")]
        [Description("list of chars")]
        [Category("activity")]
        public async Task ListChars (CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder();
            var ClassBuilder = new StringBuilder();
            var ApBuilder = new StringBuilder();
            var DpBuilder = new StringBuilder();
            string[] ClassArray = { "warrior","ranger","sorceress","berserker","tamer","musa","maehwa","valkyrie","kunoichi","ninja","wizard","witch",
            "mystic","striker","lahn","archer","dk","shai","guardian","hashashin","nova","sage","corsair"};
            for(int i = 0; i < ClassArray.Length; i++)
            {
                var classname = char.ToUpper(ClassArray[i][0]) + ClassArray[i].Substring(1);
                
                var info = new ClassStats().GetStats(classname);
                ClassBuilder.Append(info["Classname"]).Append("\n");
                ApBuilder.Append(info["Ap"]).Append("\n");
                DpBuilder.Append(info["Dp"]).Append("\n");
                
            }
            embed.WithTitle("Class list and stats")
                .AddField("Character", ClassBuilder.ToString(), true)
                .AddField("Ap", ApBuilder.ToString(), true)
                .AddField("Dp", DpBuilder.ToString(), true);
            await ctx.RespondAsync(embed);
        }
    }
}
