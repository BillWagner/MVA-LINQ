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
            var sequence = Enumerable.Range(0, 100);

            bool exists = sequence.Count() > 10;

            Console.WriteLine(exists);
        }
    }
}
