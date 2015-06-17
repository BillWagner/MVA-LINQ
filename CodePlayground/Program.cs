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
            yield return "one";
            yield return "two";
            yield return "three";
        }
    }


}
