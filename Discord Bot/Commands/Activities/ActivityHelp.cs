using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities
{
    class ActivityHelp : BaseCommandModule
    {

        public string Categorize(IEnumerable<Command> subcommands, string categorize)
        {
            StringBuilder builder = new StringBuilder();


            foreach (var cmd in subcommands)
            {
                var categoryAttribute = (CategoryAttribute)cmd.CustomAttributes.FirstOrDefault(x => x is CategoryAttribute);
                if (cmd.Name != "help" && categoryAttribute.Category == categorize)
                {

                    builder.Append(cmd.Name).Append("\n");
                }
            }
            return builder.ToString();
        }
        [Command("activityhelp")]
        [Category("activity")]
        public async Task Help(CommandContext ctx)
        {
            var commands = ctx.CommandsNext.RegisteredCommands.Values;
            var embed = new DiscordEmbedBuilder()
                .AddField("Activity commands", Categorize(commands,"activity"),true);
            await ctx.RespondAsync(embed.Build());
            
        }
    }
}
