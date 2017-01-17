using Gustnado.Objects;
using Gustnado.Requests.Tracks;

namespace Gustnado.Requests.OAuth
{
    public class OAuthEndpoint
    {
        private static readonly SearchContext context = new SearchContext("oauth2", "token");

        public RestRequest<OAuthResponse> Post(OAuthRequest oauth)
        {
            return RestRequest<OAuthResponse>.Post(context)
                .AddSoundCloudObject(oauth);
        } 

        //todo convenience methods for different auth types
    }
}