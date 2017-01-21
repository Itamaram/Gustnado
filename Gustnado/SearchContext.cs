using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gustnado
{
    public class SearchContext : IEnumerable<string>
    {
        private readonly IReadOnlyList<string> terms;

        public SearchContext(params string[] terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext(IEnumerable<string> terms)
        {
            this.terms = terms.ToList();
        }

        public SearchContext Add(string s)
        {
            return new SearchContext(new List<string>(terms) { s });
        }

        public SearchContext Add(int id) => Add(id.ToString());

        public IEnumerator<string> GetEnumerator() => terms.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}