using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.Services
{
    public class MusicService
    {


        public class Playlist
        {
            public ObjectId _id { get; set; }
            public ulong UserId { get; set; }
            public string Title { get; set; }
            public List<LavalinkTrack> ListOfTracks { get; set; }
        }


        public static Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        public List<LavalinkTrack> ListQueue(CommandContext ctx)
        {
            var guild = ctx.Guild.Id;
            var query = music[guild];
            return query;
        }


        public async Task  Play(CommandContext ctx,params string[] search)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < search.Length; i++)
            {
                sb.Append(search[i]).Append(" ");
            }
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }
            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();

            if (node.GetGuildConnection(ctx.Member.VoiceState.Guild) == null)
            {
                var link = ctx.Client.GetLavalink();
                if (!link.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }
                var nodey = link.ConnectedNodes.Values.First();
                var channel = ctx.Member.VoiceState.Channel;
                if (channel.Type != ChannelType.Voice)
                {
                    await ctx.RespondAsync("Not a valid voice channel.");
                    return;
                }
                await nodey.ConnectAsync(channel);
            }
            var loadResult = await node.Rest.GetTracksAsync(sb.ToString());

            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Track search failed for {search}.");
                return;
            }
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);
            var track = loadResult.Tracks.First();

            if (conn.CurrentState.CurrentTrack == null)
            {
                var builder = new DiscordMessageBuilder()
                    .WithContent("Player")
                    .AddComponents(new DiscordComponent[]
                    {
                      new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                   });
                await ctx.Channel.SendMessageAsync(builder);
                await conn.PlayAsync(track);
            }
            else
            {
                if (!music.ContainsKey(ctx.Guild.Id))
                {
                    List<LavalinkTrack> queue = new List<LavalinkTrack>();
                    queue.Clear();
                    queue.Add(track);
                    music.Add(ctx.Guild.Id, queue);
                }
                else
                {
                    var newQueue = music[ctx.Guild.Id];
                    newQueue.Add(track);
                    music[ctx.Guild.Id] = newQueue;
                }
                await ctx.RespondAsync($"{track.Title} added to queue");
            }
        }




        public async Task PlayPlaylist (CommandContext ctx,Uri uri)
        {
            var PlaylistKeyWord = "/playlist/";

            if (uri.OriginalString.Contains(PlaylistKeyWord) && uri.OriginalString.Contains("spotify") && Uri.IsWellFormedUriString(uri.OriginalString, UriKind.Absolute))
            {

                var auth = new SpotifyAuth().GetAuth();


                int index = uri.OriginalString.IndexOf(PlaylistKeyWord);

                string id = uri.OriginalString.Substring(index + PlaylistKeyWord.Length);
                string FinalId;
                if (id.Contains("&"))
                {
                    var EndIndex = id.IndexOf("&dl");
                    FinalId = id.Substring(0, EndIndex);
                }
                else
                {
                    FinalId = id.Substring(0, id.Length);
                }




                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Result);

                String PlaylistUrl = $"https://api.spotify.com/v1/playlists/{FinalId}/tracks?fields=items(track(name))";
                var success = await client.GetAsync(PlaylistUrl).Result.Content.ReadAsStringAsync();
                JsonDocument json = JsonDocument.Parse(success);
                JsonElement root = json.RootElement.GetProperty("tracks").GetProperty("items");

                var ListOfTracks = new List<string>();
                for (int i = 0; i < root.GetArrayLength(); i++)
                {
                    var TrackInfo = root[i].GetProperty("track");
                    var artists = TrackInfo.GetProperty("album").GetProperty("artists");
                    var builder = new StringBuilder();
                    for (int j = 0; j < artists.GetArrayLength(); j++)
                    {
                        builder.Append(artists[j].GetProperty("name").GetString()).Append(" ");
                    }
                    builder.Append("-").Append(" ").Append(TrackInfo.GetProperty("name").GetString());

                    ListOfTracks.Add(builder.ToString());
                }


                var lava = ctx.Client.GetLavalink();
                if (!lava.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }
                var node = lava.ConnectedNodes.Values.First();
                var connect = node.ConnectAsync(ctx.Member.VoiceState.Channel).Result;
                var ListofLavalinkTracks = new List<LavalinkTrack>();
                for (int i = 0; i < ListOfTracks.Count; i++)
                {
                    var loadResult = node.Rest.GetTracksAsync(ListOfTracks[i]).Result.Tracks.First();
                    ListofLavalinkTracks.Add(loadResult);
                }


                if (connect.CurrentState.CurrentTrack == null)
                {
                    var builder = new DiscordMessageBuilder()
                       .WithContent("Player")
                       .AddComponents(new DiscordComponent[]
                       {
                       new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                      });
                    await ctx.Channel.SendMessageAsync(builder);

                    foreach (var track in ListofLavalinkTracks)
                    {
                        if (connect.CurrentState.CurrentTrack == null)
                        {
                            await connect.PlayAsync(track);
                        }
                        else
                        {
                            if (!music.ContainsKey(ctx.Guild.Id))
                            {
                                List<LavalinkTrack> queue = new List<LavalinkTrack>();
                                queue.Clear();
                                queue.Add(track);
                                music.Add(ctx.Guild.Id, queue);
                            }
                            else
                            {
                                var newQueue = music[ctx.Guild.Id];
                                newQueue.Add(track);
                                music[ctx.Guild.Id] = newQueue;
                            }
                        }
                    }
                }

            }
            else
            {


                var lava = ctx.Client.GetLavalink();
                if (!lava.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }
                var node = lava.ConnectedNodes.Values.First();
                var connect = node.ConnectAsync(ctx.Member.VoiceState.Channel).Result;
                var loadResult = node.Rest.GetTracksAsync(uri).Result.Tracks.ToList();
                if (connect.CurrentState.CurrentTrack == null)
                {
                    var builder = new DiscordMessageBuilder()
                       .WithContent("Player")
                       .AddComponents(new DiscordComponent[]
                       {
                       new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                      });
                    await ctx.Channel.SendMessageAsync(builder);
                }
                foreach (var track in loadResult)
                {
                    if (connect.CurrentState.CurrentTrack == null)
                    {
                        await connect.PlayAsync(track);
                    }
                    else
                    {
                        if (!music.ContainsKey(ctx.Guild.Id))
                        {
                            List<LavalinkTrack> queue = new List<LavalinkTrack>();
                            queue.Clear();
                            queue.Add(track);
                            music.Add(ctx.Guild.Id, queue);
                        }
                        else
                        {
                            var newQueue = music[ctx.Guild.Id];
                            newQueue.Add(track);
                            music[ctx.Guild.Id] = newQueue;
                        }
                    }
                }

            }
        }


        public async Task PlaySavedPlaylist(CommandContext ctx, string playlist)
        {
            var connect = new MongoClient(Config.Get("uri")); 
            var db = connect.GetDatabase("Csharp");
            var collection = db.GetCollection<Playlist>("playlists");
            var filter = Builders<Playlist>.Filter.Eq("UserId", ctx.Member.Id);
            var docs = await collection.FindAsync(filter);
            var match = await docs.ToListAsync();
            
            if (match != null)
            {
                var matched = match.FirstOrDefault(x => x.Title.ToString() == playlist);
                if(matched == null)
                {
                    await ctx.RespondAsync("Playlist doesn't exist");
                }
                var Tracks = matched.ListOfTracks;
                var lava = ctx.Client.GetLavalink();
                if (!lava.ConnectedNodes.Any())
                {
                    await ctx.RespondAsync("The Lavalink connection is not established");
                    return;
                }

                var node = lava.ConnectedNodes.Values.First();
                if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
                {
                    await ctx.RespondAsync("You are not in a voice channel.");
                    return;
                }



                var conn = node.ConnectAsync(ctx.Member.VoiceState.Channel).Result;
                
                if (conn.CurrentState.CurrentTrack == null)
                {
                    var builder = new DiscordMessageBuilder()
                       .WithContent("Player")
                       .AddComponents(new DiscordComponent[]
                       {
                       new DiscordButtonComponent(ButtonStyle.Secondary, "resume", "▶️"),
                       new DiscordButtonComponent(ButtonStyle.Secondary, "stop", "⏹️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "pause", "⏸️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary, "skip", "⏭️"),
                        new DiscordButtonComponent(ButtonStyle.Secondary,"repeat","🔁")
                      });
                    await ctx.Channel.SendMessageAsync(builder);
                }

                foreach(var track in Tracks)
                {
                    if (conn.CurrentState.CurrentTrack == null)
                    {
                        await conn.PlayAsync(track);
                    }
                    else
                    {
                        if (!music.ContainsKey(ctx.Guild.Id))
                        {
                            List<LavalinkTrack> queue = new List<LavalinkTrack>();
                            queue.Clear();
                            queue.Add(track);
                            music.Add(ctx.Guild.Id, queue);
                        }
                        else
                        {
                            var newQueue = music[ctx.Guild.Id];
                            newQueue.Add(track);
                            music[ctx.Guild.Id] = newQueue;
                        }
                    }
                }

            }
            else
            {
                await ctx.RespondAsync("You don't have any playlists");
            }
           
        }   

    }
}
