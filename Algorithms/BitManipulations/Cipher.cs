using System;
using System.Collections.Generic;
using System.Text;

/*see https://www.hackerrank.com/challenges/cipher */

namespace Algorithms.BitManipulations
{
    public class Cipher
    {
        public static void CipherDriver()
        {
            string letter;
            int N;
            int K;

            #region HackerRank STDIN and Parsing
            //string[] input = Console.ReadLine().Split(' ');
            //letter = Console.ReadLine();
            //N = int.Parse(input[0]);
            //K = int.Parse(input[1]);
            #endregion

            #region Test Input
            letter = "1110100110";
            N = 7; K = 4;
            #endregion

            int kk = 0;
            decimal ones = 0;
            bool b;
            bool R;

            StringBuilder sb = new StringBuilder();
            Queue<bool> queue = new Queue<bool>();

            for (int i = 0; i < N; i++)
            {
                b = letter[i] == '1' ? true : false;

                if (ones % 2 != 0) { R = !b; } else { R = b; }
                sb.Append(R == true ? '1' : '0');
                queue.Enqueue(R);
                if (R == true) { ones++; }
                kk++;
                if (kk >= K && queue.Dequeue() == true) { ones--; }
            }

            // Writing to file - in case of big input, Console writing could get messy
            WriteToFile(sb.ToString());

            Console.Write(sb);
            Console.ReadLine();
        }

        static void WriteToFile(string s)
        {
            using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"C:\myfolder\WriteLines2.txt"))
            {

                file.WriteLine(s);
            }
        }
    }
}