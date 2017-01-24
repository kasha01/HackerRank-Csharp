using System;
using System.Numerics;

 /*see: https://www.hackerrank.com/challenges/fibonacci-modified */
namespace Algorithms.DynamicProgramming
{
    class FibonacciModified
    {
        public static void Driver()
        {
            Console.WriteLine("Enter Fibonnaci First Seed");
            string fSeed = Console.ReadLine();
            Console.WriteLine("Enter Fibonnaci Second Seed");
            string sSeed = Console.ReadLine();
            Console.WriteLine("Enter a number to calculate its Fibonnaci");
            string input = Console.ReadLine();
            Console.WriteLine("Your Fibonacci is: " + Fibonacci(int.Parse(fSeed), int.Parse(sSeed), int.Parse(input)));
            Console.ReadLine();
        }

        static string Fibonacci(int ff, int ss, int n)
        {
            BigInteger f = new BigInteger(ff);
            BigInteger s = new BigInteger(ss);
            BigInteger sum = new BigInteger(0);

            for (int i = 3; i <= n; i++)
            {
                sum = f + (s * s);
                f = s;
                s = sum;
            }
            return sum.ToString();
        }
    }
}