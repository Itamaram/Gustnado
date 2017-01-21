using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bearded.Monads;

namespace Gustnado.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<A> GetCustomAttributes<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes()
                .Select(a => OptionExtensions.MaybeCast<A>(a))
                .ConcatOptions();
        }

        public static Option<A> GetCustomAttribute<A>(this PropertyInfo property) where A : Attribute
        {
            return property.GetCustomAttributes<A>().SingleOrNone();
        }

        public static object Invoke(this MethodBase m, object o) => m.Invoke(o, new object[0]);
    }
}