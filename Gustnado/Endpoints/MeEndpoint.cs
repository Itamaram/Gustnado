using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;
using Newtonsoft.Json;

namespace Gustnado.Endpoints
{
    public class MeEndpoint : ReadOnlyUserEndpoint
    {
        public MeEndpoint() : base(new SearchContext("me")) { }
        
        public new ReadWriteFollowingsEndpoint Followings => new ReadWriteFollowingsEndpoint(Context);
        public new ReadWriteFavoritesEndpoint Favorites => new ReadWriteFavoritesEndpoint(Context);
        public new ReadWriteWebProfilesEndpoint WebProfiles => new ReadWriteWebProfilesEndpoint(Context);

        //todo activities
    }

    public class ConnectionsEndpoint
    {
        private readonly SearchContext context;

        public ConnectionsEndpoint(SearchContext context)
        {
            this.context = context.Add("connections");
        }

        public RestRequestMany<Connection> Get(int pagesize = 50) => RestRequestMany<Connection>.Get(context, pagesize);

        public RestRequest<NewConnectionResponse> Post(NewConnectionRequest request)
        {
            return RestRequest<NewConnectionResponse>.Post(context)
                .WriteToRequest(request);
        }

        public ConnectionEndpoint this[int id] => new ConnectionEndpoint(context, id);
    }

    public class ConnectionEndpoint
    {
        private readonly SearchContext context;

        public ConnectionEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Connection> Get() => RestRequest<Connection>.Get(context);
    }

    public class NewConnectionRequest
    {
        [JsonProperty("service")]
        public ConnectionService Service { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }

    public class NewConnectionResponse
    {
        [JsonProperty("authorize_url")]
        public string AuthorizeUrl { get; set; }
    }
}