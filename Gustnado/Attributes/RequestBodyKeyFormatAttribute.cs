using System;

namespace Gustnado.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequestBodyKeyFormatAttribute : Attribute
    {
        public string Format { get; }
        public RequestBodyKeyFormatAttribute(string format)
        {
            Format = format;
        }
    }
}