using System;
using System.Linq;
using System.Reflection;
using Bearded.Monads;

namespace Gustnado.Extensions
{
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
                .Select(a => OptionExtensions.MaybeCast<A>(a))
                .ConcatOptions()
                .FirstOrNone();
        }
    }
}