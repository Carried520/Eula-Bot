using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    class MusicGetter : BaseCommandModule
    {
        [Command("log")]
        public async Task GetMusic(CommandContext ctx)
        {
            foreach(var ListOfTracks in Play.music.Keys)
            {
                foreach(var track in Play.music[ListOfTracks])
                {
                    Console.WriteLine(track.Title);
                }
            }
            
        }
    }
}
