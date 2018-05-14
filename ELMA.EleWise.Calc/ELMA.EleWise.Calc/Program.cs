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
            if (args.Length != 3)
            {
                Console.WriteLine("Не указаны аргументы");
                return;
            }
            CalcOper oper;
            if (!Enum.TryParse(args[0], out oper))
            {
                Console.WriteLine("Не указана операция");
                return;
            }
            switch (oper)
            {
                case CalcOper.Sum:
                    Console.WriteLine(Convert.ToInt64(args[1]) + Convert.ToInt64(args[2]));
                    break;
                case CalcOper.Mult:
                    Console.WriteLine(Convert.ToInt64(args[1]) * Convert.ToInt64(args[2]));
                    break;
            }
           
            Console.ReadKey();
        }
    }
}
