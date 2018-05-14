using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELMA.EleWise.Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Не указаны аргументы");
                return;
            }
            Console.WriteLine(Convert.ToInt64(args[0]) + Convert.ToInt64(args[1]));
            Console.ReadKey();
        }
    }
}
