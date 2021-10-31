using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discord_Bot.Utils
{
    
  public  class CustomHelpFormatter : BaseHelpFormatter
    {

        protected DiscordEmbedBuilder _embed;
        protected InteractivityExtension _interact;

        
        public CustomHelpFormatter(CommandContext ctx) : base(ctx)
        {
            _embed = new DiscordEmbedBuilder();
           
         
           
                
            
        }

        public string Categorize (IEnumerable<Command> subcommands , string categorize)
        {
            StringBuilder builder = new StringBuilder();
           
            
            foreach (var cmd in subcommands)
            {
                var categoryAttribute = (CategoryAttribute)cmd.CustomAttributes.FirstOrDefault(x => x is CategoryAttribute);
                if (cmd.Name != "help" &&  categoryAttribute.Category == categorize)
                {
                   
                    builder.Append(cmd.Name).Append("\n");
                }
            }
            return builder.ToString();
        }
        


        public override CommandHelpMessage Build()
        {
           
             return new CommandHelpMessage(embed: _embed);
            
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            var sb = new StringBuilder();
            sb.Append("You can use one of following options:").Append(" ").Append("\n");



            var result = command.Overloads;
            
           for(int i =0;i<result.Count;i++)
            {
                 sb.Append($"{i+1} option:");
                foreach (var overload in result[i].Arguments)
                {
                    sb.Append(" ").Append($"{overload.Name}").Append(" ").Append($"<{ overload.Description}>").Append(" ");
                }
                
                
            }


            var toString = sb.ToString();
            if (string.IsNullOrEmpty(toString))
            {

                _embed.AddField(command.Name, "no arguments").WithColor(DiscordColor.SpringGreen);
            }
            else _embed.AddField(command.Name, toString).WithColor(DiscordColor.SpringGreen);



            return this;

        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {




            _embed.AddField("Misc", Categorize(subcommands, "misc"), true)
                .AddField("Botinfo", Categorize(subcommands, "botinfo"), true)
                .AddField("guild-only", Categorize(subcommands, "guild"), true)
                .AddField("music", Categorize(subcommands, "music"), true)
                .AddField("rp", Categorize(subcommands, "rp"), true);











            return this;
        }
    }
}
            
       

       
