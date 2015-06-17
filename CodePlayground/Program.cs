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
            // Generate items using a factory:
            var items = GenerateSequence(i => i.ToString());    
            foreach (var item in items)
                Console.WriteLine(item);

            var moreItems = GenerateSequence(i => i);
            foreach (var item in moreItems)
                Console.WriteLine(item);
        }

        // Core syntax for an enumerable:
        private static IEnumerable<T> GenerateSequence<T>(Func<int, T> factory)
        {
            var i = 0;
            while (i++ < 100)
                yield return factory(i);
        }
    }


}
