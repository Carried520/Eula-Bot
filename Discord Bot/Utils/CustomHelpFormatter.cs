using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
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
            sb.Append("How to use :").Append(" ").Append("\n");

            var result = command.Overloads;
            
                for (int i = 0; i<result.Count; i++)
                {
                    
                    sb.Append($"{i+1} :").Append(" ").Append($"e!{command.Name}");
                    if (result[i].Arguments.Any())
                    {
                        foreach (var overload in result[i].Arguments)
                        {



                           

                            sb.Append(" ").Append($"{overload.Name}").Append(" ").Append($"<{ overload.Description}>").Append(" ");


                        }
                    }
                    else
                    {
                        sb.Append(" ").Append($"e!{command.Name}").Append("");
                    }
                    
                    sb.Append("\n");


                }
            
            
           


            var toString = sb.ToString();
            if (string.IsNullOrEmpty(toString))
            {

                _embed.AddField(command.Name, "no arguments").WithColor(DiscordColor.SpringGreen);
            }
            else _embed.AddField(command.Name, toString).WithColor(DiscordColor.SpringGreen);
            _embed.AddField("Description",command.Description);
           



            return this;

        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {




            _embed.AddField("Misc", Categorize(subcommands, "misc"), true)
                .AddField("Botinfo", Categorize(subcommands, "botinfo"), true)
                .AddField("music", Categorize(subcommands, "music"), true)
                .AddField("rp", Categorize(subcommands, "rp"), true)
                .AddField("moderation", Categorize(subcommands, "moderation"), true);
              
            if (!string.IsNullOrWhiteSpace(Categorize(subcommands, "guild")))
            {
                _embed.AddField("ahegao-guild-only", Categorize(subcommands, "guild"), true);

            }  if (!string.IsNullOrWhiteSpace(Categorize(subcommands, "nsfw")))
            {
                  _embed.AddField("nsfw", Categorize(subcommands, "nsfw"), true);
            }
            return this;
        }
    }
}
            
       

       
