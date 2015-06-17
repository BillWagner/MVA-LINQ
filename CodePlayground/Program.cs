using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            // This doesn't change (yet):
            foreach (var item in GeneratedStrings())
                Console.WriteLine(item);
        }

        // Core syntax for an enumerable:
        private static IEnumerable<string> GeneratedStrings()
        {
            var i = 0;
            while (i++ < 100)
                yield return i.ToString();
        }
    }


}
