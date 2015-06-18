using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePlayground
{
    public static class MyLinqMethods
    {
        public static IEnumerable<T> Where<T>(
            this IEnumerable<T> inputSequence, 
            Func<T, bool> predicate)
        {
            foreach (T item in inputSequence)
                if (predicate(item))
                    yield return item;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Generate items using a factory:
            var items = GenerateSequence(i => i.ToString());    
            foreach (var item in items.Where(item => item.Length < 2))
                Console.WriteLine(item);

            return;
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
