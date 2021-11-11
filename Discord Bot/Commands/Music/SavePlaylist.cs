using Discord_Bot.Attributes;
using Discord_Bot.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Music
{
    public class SavePlaylist : BaseCommandModule
    {
      
        public class Playlist
        {
            public ObjectId _id { get; set; }
            public ulong UserId { get; set; }
            public string Title { get; set; }
            public List<LavalinkTrack> ListOfTracks { get; set; }
            
        }
        

        [Command("saveplaylist")]
        [Description("save playlist")]
        [Category("music")]
        public async Task SavePlaylistCommand(CommandContext ctx ,string PlaylistTitle,Uri uri)
        {
            if (Uri.IsWellFormedUriString(PlaylistTitle, UriKind.Absolute))
            {
                await ctx.RespondAsync("You should put url in 2nd argument");
                return;
            }
            var connect = new MongoClient(Config.Get("uri"));
           var db = connect.GetDatabase("Csharp");
           var collection = db.GetCollection<Playlist>("playlists");
            var filter = Builders<Playlist>.Filter.Eq("UserId",ctx.Member.Id) & Builders<Playlist>.Filter.Eq("Title",PlaylistTitle);
           var result = await collection.Find(filter).FirstOrDefaultAsync();

            var lava = ctx.Client.GetLavalink();
            if (!lava.ConnectedNodes.Any())
            {
                await ctx.RespondAsync("The Lavalink connection is not established");
                return;
            }
            var node = lava.ConnectedNodes.Values.First();
            var loadResult = node.Rest.GetTracksAsync(uri).Result.Tracks.ToList();

            if (result == null)
            {
                var Tracks = loadResult;
                await collection.InsertOneAsync(new Playlist { UserId = ctx.Member.Id, Title = PlaylistTitle, ListOfTracks = Tracks });
                await ctx.RespondAsync($"Playlist {PlaylistTitle} saved");
            }
            else
            {
                await ctx.RespondAsync("You have already playlist with that name");
                return;
            }
            
        }

        [Command("showplaylists")]
        [Description("save playlist")]
        [Category("music")]
        public async Task Show (CommandContext ctx)
        {
            var connect = new MongoClient(Config.Get("uri"));
            var db = connect.GetDatabase("Csharp");
            var collection = db.GetCollection<Playlist>("playlists");
            var filter = Builders<Playlist>.Filter.Eq("UserId", ctx.Member.Id);
            var find =  await collection.FindAsync(filter);
            var ResultToList = await find.ToListAsync();
            if (ResultToList.Any())
            {
                
                var PlaylistBuilder = new StringBuilder();
                foreach(var result in ResultToList)
                {
                    PlaylistBuilder.Append($"\n{result.Title}");
                    
                }
                var embed = new DiscordEmbedBuilder()
                    .WithTitle("Your playlists")
                    .AddField("Playlists:",PlaylistBuilder.ToString(),false);
                await ctx.RespondAsync(embed);
            }
            else
            {
                await ctx.RespondAsync("You dont have any playlists saved");
            }
            
           
            
        }
    }
}
