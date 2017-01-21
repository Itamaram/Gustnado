using Gustnado.Converters;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;
using Newtonsoft.Json;

namespace Gustnado.Endpoints.Playlists
{
    public class PlaylistsEndpoint
    {
        private static readonly SearchContext context = new SearchContext("playlists");

        public RestRequestMany<Playlist> Get(PlaylistsFilters filters)
        {
            return RestRequestMany<Playlist>.Get(context)
                .WriteToQueryString(filters);
        }

        public RestRequestMany<Playlist> Get() => RestRequestMany<Playlist>.Get(context);

        public RestRequest<Playlist> Put(Playlist playlist)
        {
            return RestRequest<Playlist>.Put(context)
                .AddJsonToRequestBody(playlist);
        }
    }

    public class PlaylistEndpoint
    {
        private readonly SearchContext context;

        public PlaylistEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Playlist> Get() => RestRequest<Playlist>.Get(context);

        public RestRequest<Playlist> Put(Playlist playlist)
        {
            return RestRequest<Playlist>.Put(context)
                .AddJsonToRequestBody(playlist);
        }
    }

    public class PlaylistsFilters
    {
        /// <summary>
        /// a string to search for (see search documentation). When this parameter is used, only compact representations are returned.
        /// </summary>
        [JsonProperty("q")]
        public string Q { get; set; }

        /// <summary>
        /// compact (no track listing) or id (track listing only contains track IDs).
        /// </summary>
        [JsonProperty("representation")]
        public PlaylistRepresentation Representation { get; set; }
    }


    [JsonConverter(typeof(EnumConverter<PlaylistRepresentation>))]
    public enum PlaylistRepresentation
    {
        [JsonProperty("compact")]
        Compact,
        [JsonProperty("id")]
        Id
    }
}