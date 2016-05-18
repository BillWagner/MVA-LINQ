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

        public static bool Any<T>(this IEnumerable<T> sequence)
        {
            return sequence.GetEnumerator().MoveNext();
        }
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            int count = 0;
            foreach (var item in sequence)
                count++;
            return count;
        }

        public static bool Any<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate).GetEnumerator().MoveNext();
        }

        public static int Count<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            return sequence.Where(predicate).Count();
        }

        public static int Sum(this IEnumerable<int> sequence)
        {
            var sum = 0;
            foreach (var item in sequence)
                sum += item;
            return sum;
        }

        public static int Sum(this IEnumerable<int> sequence, int seed)
        {
            var sum = seed;
            foreach (var item in sequence)
                sum += item;
            return sum;
        }

        public static int Aggregate(this IEnumerable<int> sequence, Func<int, int, int> func)
        {
            var sum = 0;
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static int Aggregate(this IEnumerable<int> sequence, int seed, Func<int, int, int> func)
        {
            var sum = seed;
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static T Aggregate<T>(this IEnumerable<T> sequence, Func<T, T, T> func)
        {
            var sum = default(T);
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static T Aggregate<T>(this IEnumerable<T> sequence, T seed, Func<T, T, T> func)
        {
            var sum = seed;
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }
    }
    class Program
    {
        public static string delim = string.Empty;

        static void Main(string[] args)
        {
            var sum = SequenceFromConsole()
                .Aggregate("Comma Separated: [", (existingString, item) => {
                    var s = existingString + delim + item;
                    delim = ", ";
                    return s;
                });

            sum = string.Concat(sum, "]");

            Console.WriteLine(sum);

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
            string text = Console.ReadLine();
            while (text != "done")
            {
                yield return text;
                text = Console.ReadLine();
            }
        }
    }
}
