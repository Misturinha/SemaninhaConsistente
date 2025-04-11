using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POCSemaninhaConsciente
{
    public class LastFmIntegrator(string user, string apiKey)
    {
        #region Consts
        const string API_URL = "https://ws.audioscrobbler.com/2.0/";
        const string GET_TOP_TRACS = "user.gettoptracks";
        #endregion

        #region Propriedades
        public HttpClient HttpClient { get; set; } = new HttpClient();
        public string User { get; set; } = user;
        public string ApiKey { get; set; } = apiKey;
        #endregion

        #region Métodos
        public async Task ObterMusicasMaisOuvidas()
        {
            try
            {
                int currentPage = 1;
                bool keepLooping = true;
                do
                {
                    var url = $"{API_URL}?method={GET_TOP_TRACS}&user={User}&api_key={ApiKey}&format=json&limit=400&period=7day&page={currentPage}";
                    HttpResponseMessage response = await HttpClient.GetAsync(url);

                    // Ensure success status code
                    response.EnsureSuccessStatusCode();

                    // Read response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    LastFmResponse tracks = JsonSerializer.Deserialize<LastFmResponse>(responseBody);
                    currentPage++;
                    keepLooping = tracks?.TopTracks?.Músicas?.Count > 0;
                } while (keepLooping);
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }

    public class Config(string apiKey)
    {
        public string ApiKey { get; set; } = apiKey;
    }
    public class LastFmResponse
    {
        [JsonPropertyName("toptracks")]
        public TopTracks TopTracks { get; set; }
    }
    public class TopTracks
    {
        [JsonPropertyName("track")]
        public List<Track> Músicas { get; set; }
    }

    public class Track
    {
        [JsonPropertyName("streamable")]
        public StreamableInfo Streamable { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("image")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("@attr")]
        public Attributes Attributes { get; set; }

        [JsonPropertyName("playcount")]
        public string PlayCount { get; set; }
    }

    public class StreamableInfo
    {
        [JsonPropertyName("fulltrack")]
        public string FullTrack { get; set; }

        [JsonPropertyName("#text")]
        public string Text { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("#text")]
        public string Text { get; set; }
    }

    public class Artist
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }
    }

    public class Attributes
    {
        [JsonPropertyName("rank")]
        public string Rank { get; set; }
    }
}
