using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceConsiderations
{
    class Program
    {
        static void Main(string[] args)
        {

            var sequence = from n in Enumerable.Range(0, 20)
                           where n % 3 == 0
                           select n * n;

            var sequence2 = from item in sequence
                            let label = item.ToString()
                            orderby label descending
                            select new { label, item };

            foreach (var item in sequence2)
                Console.WriteLine(item);

            return;
            var sequence3 = Enumerable.Range(0, int.MaxValue);

            Console.WriteLine("Starting");
            int singleItem = sequence3.Last(item => item == 13);
            Console.WriteLine(singleItem);
        }
    }
}
