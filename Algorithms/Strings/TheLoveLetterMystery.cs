using System;

/*see https://www.hackerrank.com/challenges/the-love-letter-mystery */
namespace Algorithms.Strings
{
    public class TheLoveLetterMystery
    {
        public static void Driver()
        {
            // Test input
            string input = "cba";
            int a = 0;
            int k = input.Length - 1;
            int counter = 0;

            while (a < k)
            {
                int aa = Convert.ToInt16(input[a]);
                int bb = Convert.ToInt16(input[k]);
                if(aa > bb) { counter = counter + reductLetter(aa, bb); }
                else if(aa < bb) { counter = counter + reductLetter(bb, aa); }

                a++;k--;
            }
            Console.WriteLine(counter);
            Console.ReadLine();
        }

        static int reductLetter(int toReduce, int toEqual)
        {
            int counter = 0;
            while (toReduce != toEqual)
            {
                toReduce--;
                counter++;
            }
            return counter;
        }
    }
}
