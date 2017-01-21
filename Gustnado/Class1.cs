﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bearded.Monads;
using Gustnado.Endpoints;
using Gustnado.Extensions;
using Gustnado.Objects;
using Gustnado.RestSharp;
using Gustnado.Serialisation;
using Newtonsoft.Json;
using RestSharp;

namespace Gustnado
{
    public static class OptionsExtensions
    {
        public static IEnumerable<A> AsEnumerable<A>(this Option<A> option)
        {
            return option.Map(a => a.Yield()).ElseEmpty();
        }

        public static IEnumerable<KeyValuePair<string, string>> AsParameter(this Option<string> value, string key)
        {
            if (value.IsSome)
                yield return new KeyValuePair<string, string>(key, value.ForceValue());
        }

        public static Option<B> MaybeGetReadonlyValue<A, B>(this IReadOnlyDictionary<A, B> d, A key)
        {
            B b;
            return d.TryGetValue(key, out b) ? b : Option<B>.None;
        }
    }

    public static class ObjectExtensions
    {
        public static A Do<A>(this A a, Action<A> action)
        {
            action(a);
            return a;
        }

        public static B Map<A, B>(this A a, Func<A, B> map) => map(a);

        public static Option<A> GetCustomAttribute<A>(this Type type) where A : Attribute
        {
            return type.GetCustomAttributes()
                .Select(a => a.MaybeCast<A>())
                .ConcatOptions()
                .FirstOrNone();
        }
    }

    public static class ReflectionExtensions
    {
        public static IEnumerable<A> GetCustomAttributes<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes()
                .Select(a => a.MaybeCast<A>())
                .ConcatOptions();
        }

        public static Option<A> GetCustomAttribute<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes<A>().SingleOrNone();
        }

        public static object Invoke(this MethodBase m, object o) => m.Invoke(o, new object[0]);
    }
    
    //This can be instantiated as either authed, or not authed. It also takes care of reauthing.
    //todo should this be abstract?
    public class SoundCloudHttpClient
    {
        private readonly string clientId;
        private readonly string secret;
        private Option<string> oauth = Option<string>.None;

        public SoundCloudHttpClient(string clientId, string secret, IRestClient http)
        {
            this.clientId = clientId;
            this.secret = secret;
            Http = http;
        }

        public SoundCloudHttpClient Authenticate(string token)
        {
            oauth = token;
            return this;
        }

        public SoundCloudHttpClient Authenticate(string username, string password)
        {
            var request = SoundCloudApi.OAuth2.Post(new OAuthRequest
            {
                ClientId = clientId,
                ClientSecret = secret,
                GrantType = GrantType.Password,
                Username = username,
                Password = password,
            });

            oauth = Http.Execute<OAuthResponse>(request).Data.AccessToken;

            return this;
        }

        public IRestClient Http { get; }

        public Request Authenticate<Request>(Request r) where Request : IRestRequest
        {
            r.AddQueryParameter("oauth_token", oauth);
            return r;
        }

        public Request AddClientId<Request>(Request r) where Request : IRestRequest
        {
            r.AddQueryParameter("client_id", clientId);
            return r;
        }

        public T Execute<T>(SoundCloudRestRequest<T> request) => request.Execute(this);
    }

    public class GustnadoRestClient : RestClient
    {
        public GustnadoRestClient()
            : base(Constants.ApiEndpoint)
        {
            AddHandler("application/json", CustomSerializer.Default);
        }
    }

    public class SearchContext : IEnumerable<string>
    {
        private readonly IReadOnlyList<string> terms;

        public SearchContext(params string[] terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext(IEnumerable<string> terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext Add(string s)
        {
            return new SearchContext(new List<string>(terms) { s });
        }

        public SearchContext Add(int id) => Add(id.ToString());

        public IEnumerator<string> GetEnumerator() => terms.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class Constants
    {
        public static string ApiEndpoint = "https://api.soundcloud.com";
    }
}
