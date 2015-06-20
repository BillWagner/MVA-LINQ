using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExpressionPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = from n in numbers
                          select new { Number = n, Square = n * n };
            var squares2 = numbers.Select(n => new { Number = n, Square = n * n });

        }
    }
}
