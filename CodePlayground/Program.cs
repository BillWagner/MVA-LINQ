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

        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> inputSequence, 
            Func<TSource, TResult> transform)
        {
            foreach (TSource item in inputSequence)
                yield return transform(item);
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> inputSequence,
            Func<TSource, int, TResult> transform)
        {
            int index = 0;
            foreach (TSource item in inputSequence)
                yield return transform(item, index++);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var input = SequenceFromConsole();
            foreach (var item in input)
                Console.WriteLine($"\t{item}");

            return;
            // Generate items using a factory:
            var items = GenerateSequence(i => i.ToString());    
            foreach (var item in items.Where(item => item.Length < 2))
                Console.WriteLine(item);

            foreach (var item in items.Select((item, index) => 
                new { index, item }))
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

        private static IEnumerable<string> SequenceFromConsole()
        {
            string text = default(string);
            while (text != "done")
            {
                yield return text;
                text = Console.ReadLine();
            }
        }
    }
}
