using System;
using System.Threading;

namespace _1.EvenNumbersThread
{
    class Program
    {
        static void Main(string[] args)
        {
            var min = int.Parse(Console.ReadLine());
            var max = int.Parse(Console.ReadLine());

            Thread thread = new Thread(() => PrintEvenNumbers(min, max));

            thread.Start();
            thread.Join();
            Console.WriteLine("Finished");
        }

        public static void PrintEvenNumbers(int min , int max)
        {
            //Thread.Sleep(1000);
            Console.WriteLine("In Thread");
            for (int i = min; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);

                }
            }
        }
    }
}
