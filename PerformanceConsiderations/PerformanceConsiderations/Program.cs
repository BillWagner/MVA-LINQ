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
            var sequence = Enumerable.Range(0, int.MaxValue);

            Console.WriteLine("Starting");
            int singleItem = sequence.First(item => item == 13);
            Console.WriteLine(singleItem);
        }
    }
}
