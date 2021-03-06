﻿using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class OAuthEndpoint
    {
        private static readonly SearchContext context = new SearchContext("oauth2", "token");

        public RestRequest<OAuthResponse> Post(OAuthRequest oauth)
        {
            return RestRequest<OAuthResponse>.Post(context)
                .WriteToRequest(oauth);
        } 

        //todo convenience methods for different auth types
    }

    //todo connect endpoint
}