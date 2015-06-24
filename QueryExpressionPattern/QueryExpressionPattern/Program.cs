using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryExpressionPattern
{
    public class Employee
    {
        public int Age
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = from n in numbers
                          select new { Number = n, Square = n * n };
            var squares2 = numbers.Select(n => new { Number = n, Square = n * n });

            ListingJoin();
        }

        static void ListingJoin()
        {
            var numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var labels = new string[] { "0", "1", "2", "3", "4", "5" };
            var query = from num in numbers
                        join label in labels on num.ToString() equals label
                        select new { num, label };

            var query2 = numbers.Join(labels, num => num.ToString(), label => label,
                (num, label) => new { num, label });

        }



        private static void WhereAndSelect(IEnumerable<Employee> employees)
        {
            var people = from e in employees
                         where e.Age > 30
                         orderby e.LastName, e.FirstName, e.Age
                         select e;
            var people2 = employees.Where(e => e.Age > 30).
                OrderBy(e => e.LastName).
                ThenBy(e => e.FirstName).
                ThenBy(e => e.Age);
        }

        private static void OrderByV2(IEnumerable<Employee> employees)
        {
            var people = from e in employees
                         where e.Age > 30
                         orderby e.LastName
                         orderby e.FirstName
                         orderby e.Age
                         select e;
        }

        private static void OrderByV3(IEnumerable<Employee> employees)
        {
            var people = from e in employees
                         where e.Age > 30
                         orderby e.LastName descending
                         orderby e.FirstName
                         orderby e.Age
                         select e;
        }



    }
}
