using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.BitManipulations
{
    /*see https://www.hackerrank.com/challenges/and-product */
    public class AND_Product
    {
        public static void Driver()
        {
            #region Test Input
            long A = 8;
            long B = 13;
            #endregion

            // 1.a Calculate the difference
            long d = B - A;

            // 1.b Find the zeros bits - Bits that would yield zero in the final result
            int zeroBitsRank = 0;
            while (d > 0)
            {
                d = d / 2;
                zeroBitsRank++;
            }
            // 2.a Convert A into a list of Binaries
            List<bool> listA = decimalToBin(A);

            // 2.b Convert B into a list of Binaries
            List<bool> listB = decimalToBin(B);

            // 3. Get AB AND Product starting from the Zero Bit Rank - AND Product of any bits before zero bit mark equals zero
            List<bool> resultantList = new List<bool>();
            for (int i = 0; i < listA.Count; i++)
            {
                if (i < zeroBitsRank)
                {
                    resultantList.Add(false);
                }
                else
                {
                    resultantList.Add(listA[i] & listB[i]);
                }
            }

            // 4. Convert resulting Binary List into a base-10 number
            double resultinDecimal = 0;
            for (int i = 0; i < resultantList.Count; i++)
            {
                int s = resultantList[i] ? 1 : 0;
                resultinDecimal = resultinDecimal + (Math.Pow(2, i) * s);
            }

            // 5. Print AB AND Product into Console.
            Console.WriteLine(resultinDecimal);
        }

        static List<bool> decimalToBin(long r)
        {
            List<bool> list = new List<bool>();
            do
            {
                if (r % 2 == 1) { list.Add(true); } else { list.Add(false); }
                r = r / 2;

            } while (r >= 1);
            return list;
        }
    }
}
