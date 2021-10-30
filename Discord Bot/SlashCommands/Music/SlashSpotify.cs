using Discord_Bot.Utils;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Discord_Bot.SlashCommands.Music
{
    class SlashSpotify : ApplicationCommandModule
    {

        Dictionary<ulong, List<LavalinkTrack>> music = PlayScheduler.music;
        [SlashCommand("spotifyplaylist","play spotify playlist")]
        public async Task Spotify (InteractionContext ctx,[Option("url","link for playlist")] string url)
        {


            await ctx.CreateResponseAsync(DSharpPlus.InteractionResponseType.DeferredChannelMessageWithSource);
            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("You are not in a voice channel."));
                return;
            }
            var auth = new SpotifyAuth().GetAuth();

            var PlaylistKeyWord = "/playlist/";
            if (!url.Contains(PlaylistKeyWord) || !url.Contains("spotify") || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("Not a spotify url"));
                return;
            }
            int index = url.IndexOf(PlaylistKeyWord);

            string id = url.Substring(index + PlaylistKeyWord.Length);


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
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("The Lavalink connection is not established"));
               
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
                await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent($"Playlist added"));
            }
        
    }
    }
}
