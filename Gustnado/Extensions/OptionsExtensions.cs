using System.Collections.Generic;
using Bearded.Monads;

namespace Gustnado.Extensions
{
    public static class OptionsExtensions
    {
        public static Option<B> MaybeGetReadonlyValue<A, B>(this IReadOnlyDictionary<A, B> d, A key)
        {
            B b;
            return d.TryGetValue(key, out b) ? b : Option<B>.None;
        }
    }
}