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
            // Old school way to use a sequence:
            foreach (var item in GeneratedStrings())
                Console.WriteLine(item);
        }

        // Old school way to generate a sequence:
        private static IEnumerable<string> GeneratedStrings()
        {
            var rval = new List<string>();
            int i = 0;
            while (i++ < 100)
                rval.Add(i.ToString());
            return rval;
        }
    }


}
