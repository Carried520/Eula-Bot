using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Lavalink;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    public class DeletePlaylist : BaseCommandModule
    {

        public class Playlist
        {
            public ObjectId _id { get; set; }
            public ulong UserId { get; set; }
            public string Title { get; set; }
            public List<LavalinkTrack> ListOfTracks { get; set; }

        }

        [Command("deleteplaylist")]
        [Description("delete playlist")]
        [Category("music")]

        public async Task Delete (CommandContext ctx,string PlaylistName)
        {
            var connect = new MongoClient(Config.Get("uri"));
            var database = connect.GetDatabase("Csharp");
            var collection = database.GetCollection<Playlist>("playlists");
            var filter = Builders<Playlist>.Filter.Eq("UserId",ctx.Member.Id) & Builders<Playlist>.Filter.Eq("Title",PlaylistName);
            var match = await collection.Find(filter).FirstOrDefaultAsync();
            if (match != null)
            {
                await collection.DeleteOneAsync(filter);
                await ctx.RespondAsync($"Playlist {match.Title} deleted");
            } else
            {
                await ctx.RespondAsync("No playlist found");
                return;
            }
            

        }
    }
}
