using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bearded.Monads;
using Gustnado.Objects;
using Gustnado.Requests.Users;

namespace Gustnado.Requests
{
    public static class Extensions
    {
        public static Task<T> Fetch<T>(this SoundCloudHttpClient client, SearchContext context)
        {
            return client.Fetch<T>(context, Enumerable.Empty<KeyValuePair<string, string>>());
        }
        public static Task<IEnumerable<T>> FetchMany<T>(this SoundCloudHttpClient client, SearchContext context)
        {
            return client.FetchMany<T>(context, Enumerable.Empty<KeyValuePair<string, string>>());
        }

        //todo this isn't pretty
        public static Task<IEnumerable<User>> Get(this UnauthedUsersRequest r) => r.Get(Option<string>.None);
    }
}