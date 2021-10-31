using Discord_Bot.Attributes;
using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Discord_Bot.Commands
{







    class Question : BaseCommandModule
    {
        [Command("question")]
        [Description("Answer a goddamn question")]
        [Category("misc")]
        [Cooldown(1,300,CooldownBucketType.User)]

        public async Task QuestionCommand (CommandContext ctx)
        {
            
            


                string webClient = new WebClient().DownloadString("https://opentdb.com/api.php?amount=1");
                JsonDocument json = JsonDocument.Parse(webClient);
                JsonElement root = json.RootElement;
                JsonElement results = root.GetProperty("results");

                string question = HttpUtility.HtmlDecode(results[0].GetProperty("question").ToString());
                string correct = HttpUtility.HtmlDecode(results[0].GetProperty("correct_answer").ToString());
                JsonElement answers = results[0].GetProperty("incorrect_answers");
                var array = answers.EnumerateArray();
                List<String> list = new List<string>();
                while (array.MoveNext())
                {
                    list.Add(array.Current.ToString());
                }
                list.Add(correct);
                list.Shuffle();

                var compo = new DiscordComponent[list.Count];
                for (int i = 0; i < compo.Length; i++)
                {
                    if (list[i] == correct)
                    {
                        compo[i] = new DiscordButtonComponent(ButtonStyle.Secondary, "correct", correct);
                    }
                    else compo[i] = new DiscordButtonComponent(ButtonStyle.Secondary, list[i], list[i]);
                }
                var builder = new DiscordMessageBuilder()
                    .WithContent(question)
                    .AddComponents(compo);
                await ctx.RespondAsync(builder);
            

                ctx.Client.ComponentInteractionCreated += async (s, e) =>
                {
                    _ = Task.Run(async () =>
                    {



                        if (e.Id == "correct")
                        {
                            await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage,
                                new DiscordInteractionResponseBuilder().WithContent($"Correct answer "));
                        }
                        else
                        {
                            var emoji = DiscordEmoji.FromName(ctx.Client, ":kekw:", true);
                            await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage,
                                new DiscordInteractionResponseBuilder().WithContent($"Incorrect  answer {emoji} "));
                        }

                    });

                    await Task.CompletedTask; 
                };


             
           
        }
    }
}
