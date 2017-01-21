using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class AppsEndpoint
    {
        private static readonly SearchContext context = new SearchContext("apps");

        public RestRequestMany<App> Get() => RestRequestMany<App>.Get(context);
        
        public AppEndpoint this[int id] => new AppEndpoint(context, id);
    }

    public class AppEndpoint
    {
        private readonly SearchContext context;

        public AppEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<App> Get() => RestRequest<App>.Get(context); 
    }
}