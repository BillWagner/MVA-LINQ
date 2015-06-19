using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePlayground
{
    public interface IOrderingImpl<T> : IEnumerable<T>
    {
        int CompareTo(T left, T right);
        IEnumerable<T> OriginalSource { get; }
    }
    public class MyOrderedEnumerable<T, TKey> : IOrderingImpl<T>, IEnumerable<T> where TKey : IComparable<TKey>
    {
        private Comparison<T> comparison;
        private IEnumerable<T> source;

        public MyOrderedEnumerable(IOrderingImpl<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            comparison = (a, b) =>
            {
                var originalComparison = source.CompareTo(a, b);
                if (originalComparison != 0)
                    return originalComparison;
                else
                    return comparer(a).CompareTo(comparer(b));
            };
        }

        public MyOrderedEnumerable(IEnumerable<T> source, Func<T, TKey> comparer)
        {
            this.source = source;
            comparison = (a, b) => comparer(a).CompareTo(comparer(b));
        }

        public IEnumerable<T> OriginalSource
        {
            get
            {
                return source;
            }
        }

        public int CompareTo(T left, T right)
        {
            return comparison(left, right);
        }

        public IEnumerator<T> GetEnumerator()
        {
            // very poor implementation, but works:
            var sorted = source.ToList();
            sorted.Sort(comparison);
            return sorted.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
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

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> sequence, Func<TAccumulate, TSource, TAccumulate> func)
        {
            var sum = default(TAccumulate);
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> sequence, 
            TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            var sum = seed;
            foreach (var item in sequence)
                sum = func(sum, item);
            return sum;
        }

        public static IOrderingImpl<T> MyOrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> comparer)
            where TKey: IComparable<TKey>
        {
            return new MyOrderedEnumerable<T, TKey>(source, comparer);
        }

        public static IOrderingImpl<T> MyThenBy<T, TKey>(this IOrderingImpl<T> source, Func<T, TKey> comparer)
            where TKey : IComparable<TKey>
        {
            return new MyOrderedEnumerable<T, TKey>(source, comparer);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = SequenceFromConsole()
                .MyOrderBy(s => s.Length)
                .MyThenBy(s => s);
            foreach (var item in sequence)
                Console.WriteLine($"\t{item}");
            return;
            var sum = SequenceFromConsole().Select(s => int.Parse(s))
                .Aggregate("Comma Separated", (existingString, item) => existingString + ", " + item);
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
