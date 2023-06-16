using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 1;
            while (n<=10)
            {
                Console.Write("{0}\t",n);
                n++;
            }
            Console.ReadKey();
        }
    }
}
