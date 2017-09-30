using System;
using System.Threading;
using System.Threading.Tasks;

namespace SumPrimesNumbers
{
    class StartUp
    {
        private static string result;

        static void Main(string[] args)
        {
            Console.WriteLine("Calculating");

            Task.Run(() => CalculateSlowly());

            Console.WriteLine("Enter command:");

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "show")
                {
                    if (result ==null)
                    {
                        Console.WriteLine("Stil Calculating..Please Wait!");
                    }
                    else
                    {
                        Console.WriteLine($"Result is {result}");
                    }
                }
                if (line == "exit")
                {
                    break;
                }
            }
        }

        private static void CalculateSlowly()
        {
            Thread.Sleep(10000);
            result = "42";
        }
    }
}
