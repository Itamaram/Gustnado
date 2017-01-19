using Bearded.Monads;
using Newtonsoft.Json;
using RestSharp;

namespace Gustnado.Serialisation
{
    public class RequestWriter : JsonWriter
    {
        protected readonly IRestRequest Request;
        private readonly string format;
        public RequestWriter(IRestRequest request, Option<string> format)
        {
            Request = request;
            this.format = format.Else(() => "{0}");
        }

        protected virtual void Write(string key, object o) => Request.AddParameter(key, o);

        private string key;
        public override void WritePropertyName(string name, bool escape)
        {
            key = string.Format(format, name);
            base.WritePropertyName(name, escape);
        }

        private void Write(object obj) => obj.AsOption().WhenSome(value => Write(key, obj));

        public override void WriteValue(string value)
        {
            Write(value);
            base.WriteValue(value);
        }

        public override void WriteValue(int value)
        {
            Write(value);
            base.WriteValue(value);
        }

        public override void WriteValue(bool value)
        {
            Write(value);
            base.WriteValue(value);
        }

        public void WriteFile(string path)
        {
            Request.AddFile(key, path);
            base.WriteValue(path);
        }

        public override void Flush() { }
    }
}