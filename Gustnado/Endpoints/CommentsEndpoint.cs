using Gustnado.Objects;
using Gustnado.RestSharp;

namespace Gustnado.Endpoints
{
    public class CommentsEndpoint
    {
        private static readonly SearchContext context = new SearchContext("comments");

        public RestRequestMany<Comment> Get() => RestRequestMany<Comment>.Get(context);
        
        public CommentEndpoint this[int id] => new CommentEndpoint(context, id);
    }

    public class CommentEndpoint
    {
        private readonly SearchContext context;

        public CommentEndpoint(SearchContext context, int id)
        {
            this.context = context.Add(id);
        }

        public RestRequest<Comment> Get() => RestRequest<Comment>.Get(context);
    }
}