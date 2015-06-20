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
            var sequence = Enumerable.Range(0, 10);

            bool exists = sequence.Skip(9).Any();

            Console.WriteLine(exists);
        }
    }
}
