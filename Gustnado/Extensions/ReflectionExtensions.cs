using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bearded.Monads;

namespace Gustnado.Extensions
{
    public static class ReflectionExtensions
    {

        public static Option<A> GetCustomAttribute<A>(this Type type) where A : Attribute
        {
            return type.GetCustomAttributes()
                .Select(a => a.MaybeCast<A>())
                .ConcatOptions()
                .FirstOrNone();
        }

        public static IEnumerable<string> GetAllStringPropertiesValues<T>(this T t)
        {
            return typeof (T).GetProperties()
                .Where(p => p.PropertyType == typeof (string))
                .Select(p => p.GetMethod.Invoke(t, new object[0]))
                .OfType<string>();
        } 
    }
}