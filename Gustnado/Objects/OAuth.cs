using Gustnado.Converters;
using Newtonsoft.Json;

namespace Gustnado.Objects
{
    public class OAuthRequest
    {
        /// <summary>
        ///The client id belonging to your application
        ///</summary>
        /// <example></example>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        ///The client secret belonging to your application
        ///</summary>
        /// <example></example>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        ///The redirect uri you have configured for your application
        ///</summary>
        /// <example></example>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        ///(authorization_code, refresh_token, password, client_credentials, oauth1_token)
        ///</summary>
        /// <example></example>
        [JsonProperty("grant_type")]
        public GrantType GrantType { get; set; }

        /// <summary>
        ///The authorization code obtained when user is sent to redirect_uri
        ///</summary>
        /// <example></example>
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        //todo does this even work?
        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class OAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        //todo error?
    }

    [JsonConverter(typeof(EnumConverter<GrantType>))]
    public enum GrantType
    {
        [JsonProperty("authorization_code")]
        AuthorizationCode,
        [JsonProperty("refresh_token")]
        RefreshToken,
        [JsonProperty("password")]
        Password,
        [JsonProperty("client_credentials")]
        ClientCredentials,
        [JsonProperty("oauth1_token")]
        OAuth1Token
    }
}