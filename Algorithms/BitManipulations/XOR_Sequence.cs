using System;

/*see: https://www.hackerrank.com/challenges/xor-se */

namespace Algorithms.BitManipulations
{
    public class XOR_Sequence
    {
        public static void Driver()
        {
            long L = 0;
            long R = 0;

            #region HackerRank STDIN and Parsing
            /*
            int Q = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < Q; a0++)
            {
                string[] tokens_L = Console.ReadLine().Split(' ');
                long L = Convert.ToInt64(tokens_L[0]);
                long R = Convert.ToInt64(tokens_L[1]);
            }
            */
            #endregion

            #region Test Input
            L = 2;
            R = 4;
            #endregion
            
            long accumulated_result = 0;
            long result = 0;
            long current_index = L;

            // 1. Calculate Result of L(current index)
            result = GetStartSeed(current_index);
            accumulated_result = result;

            // 2. Calculate from: current index -> First index with Result equals 0
            result = XOR(ref current_index, R, result, ref accumulated_result);

            // 3. Calculate how many cycle from current index to R:
            //See how many 4's we have between current index(which is always 0 here) and R
            long cycle = (R - current_index) / 4;
            if (cycle >= 1)
            {
                current_index = (cycle * 4) + current_index;
                if (cycle % 2 != 0)
                {
                    //each XOR of a 4 cycle is 2, if I have an odd number of cycles their XOR would be 2
                    //then XOR 2 with accumulated result
                    accumulated_result = accumulated_result ^ 2;
                }
            }

            //4. Calculate from: current index -> R
            result = XOR(ref current_index, R, result, ref accumulated_result);

            Console.WriteLine(accumulated_result);
            Console.ReadLine();
        }

        static long GetStartSeed(long i)
        {
            long remainder = i % 4;
            long v = 0;
            switch (remainder)
            {
                case 0:
                    v = i;
                    break;
                case 1:
                    v = 1;
                    break;
                case 2:
                    v = i + 1;
                    break;
                case 3:
                    v = 0;
                    break;
            }
            return v;
        }

        /* Calculate A(x-1) XOR x: till index=R or I get a result of zero */
        static long XOR(ref long si, long R, long seed, ref long accu_seed)
        {
            if (si >= R) { return seed; }
            do
            {
                si++;
                seed = seed ^ si;
                accu_seed = accu_seed ^ seed;
            } while (si < R && seed != 0);
            return seed;
        }
    }
}
