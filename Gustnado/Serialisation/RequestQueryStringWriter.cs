using Bearded.Monads;
using RestSharp;

namespace Gustnado.Serialisation
{
    public class RequestQueryStringWriter : RequestWriter
    {
        public RequestQueryStringWriter(IRestRequest request)
            : base(request, Option<string>.None) { }

        protected override void Write(string key, object o)
        {
            Request.AddQueryParameter(key, o.ToString());
        }
    }
}