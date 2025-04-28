using System;
using Makedll;
namespace joe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            cal calculator = new cal(10, 5);
            int result = calculator.Add();
            Console.WriteLine(result);

            result = calculator.Sub();
            Console.WriteLine(result);
        }
    }
}
