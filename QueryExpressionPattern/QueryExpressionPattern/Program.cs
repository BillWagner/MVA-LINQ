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
            SelectManyExample4();
            return;
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var squares = from n in numbers
                          select new { Number = n, Square = n * n };
            var squares2 = numbers.Select(n => new { Number = n, Square = n * n });

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

        private static void GroupJoinV2(IEnumerable<Employee> employees)
        {
            var results = from e in employees
                          group e by e.Department into d
                          select new { Department = d.Key, Size = d.Count() };

            var results2 = from d in
                               from e in employees group e by e.Department
                           select new { Department = d.Key, Size = d.Count() };

            var results3 = employees.GroupBy(e => e.Department).
                Select(d => new { Department = d.Key, Size = d.Count() });
        }

        private static void GroupByWithCollection(IEnumerable<Employee> employees)
        {
            var results = from e in employees
                          group e by e.Department into d
                          select new { Department = d.Key, Employees = d.AsEnumerable() };
            var results2 = employees.GroupBy(e => e.Department).
                Select(d => new { Department = d.Key, Employees = d.AsEnumerable() });
        }

        private static void SelectManyExample1()
        {
            int[] odds = { 1, 3, 5, 7 };
            int[] evens = { 2, 4, 6, 8 };
            var values = from oddNumber in odds
                         from evenNumber in evens
                         select new { oddNumber, evenNumber, Sum = oddNumber + evenNumber };
        }

        private static void SelectManyExample2()
        {
            int[] odds = { 1, 3, 5, 7 };
            int[] evens = { 2, 4, 6, 8 };
            var values = odds.SelectMany(oddNumber => evens,
                (oddNumber, evenNumber) =>
                new { oddNumber, evenNumber, Sum = oddNumber + evenNumber });
        }

        private static void SelectManyExample3()
        {
            int[] odds = { 1, 3, 5, 7 };
            int[] evens = { 2, 4, 6, 8 };
            var values = from oddNumber in odds
                        from evenNumber in evens
                        where oddNumber > evenNumber
                        select new { oddNumber, evenNumber, Sum = oddNumber + evenNumber };
        }

        private static void SelectManyExample4()
        {

            int[] odds = { 1, 3, 5, 7 };
            int[] evens = { 2, 4, 6, 8 };
            var values = odds.SelectMany(oddNumber => evens,
                (oddNumber, evenNumber) =>
                new { oddNumber, evenNumber })
                .Where(pair => pair.oddNumber > pair.evenNumber).
                Select(pair => new {
                    pair.oddNumber,
                    pair.evenNumber,
                    Sum = pair.oddNumber + pair.evenNumber
                });


        }

    }
}
