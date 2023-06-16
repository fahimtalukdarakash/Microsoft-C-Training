using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float rate = 2.7F;
            double basic = 9800.25;
            decimal expenses = 560.25M;
            Console.WriteLine("Rate : " + rate);
            Console.WriteLine("Basic : " + basic);
            Console.WriteLine("Expenses : " + expenses);
            Console.WriteLine($"Expenses : {expenses}, Basic : {basic}");
            Console.WriteLine("Rate : {0}, Basic: {1}, Expences: {2}", rate, basic, expenses);
            Console.ReadKey();
        }
    }
}
