﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace Gustnado.RestSharp
{
    public class JsonDotNetDeserialiser : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }
}