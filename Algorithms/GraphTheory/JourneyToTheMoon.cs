using System;
using System.Collections.Generic;

namespace Algorithms.GraphTheory
{
    // https://www.hackerrank.com/challenges/journey-to-the-moon
    public class JourneyToTheMoon
    {
        public static void Driver()
        {
            Graph graph = new Graph(10);
            graph.connectNode(0, 2);
            graph.connectNode(2, 8);
            graph.connectNode(2, 6);
            graph.connectNode(6, 9);
            graph.connectNode(1, 8);
            graph.connectNode(1, 4);
            graph.connectNode(3, 5);

            graph.mydfs();
            long sum = graph.CalculateAstronouts();
            Console.WriteLine(sum);
        }

        class Graph
        {
            int size;
            long singles = 0;
            bool[] visited;
            List<Tuple<int, int>> tuple = new List<Tuple<int, int>>();
            Dictionary<int, bool[]> adjList = new Dictionary<int, bool[]>();

            public Graph(int size)
            {
                this.size = size;
                visited = new bool[size];
            }

            public void connectNode(int a, int b)
            {
                visited[a] = false;
                visited[b] = false;

                if (!adjList.ContainsKey(a))
                    adjList.Add(a, new bool[size]);

                adjList[a][b] = true;

                if (!adjList.ContainsKey(b))
                    adjList.Add(b, new bool[size]);

                adjList[b][a] = true;
            }

            public void mydfs()
            {
                for (int i = 0; i < visited.Length; i++)
                {
                    DFS(i);
                }
            }

            private void DFS(int root)
            {
                if (visited[root])
                    return;

                Stack<int> s = new Stack<int>();
                s.Push(root);
                visited[root] = true;

                int counter = 1;
                while (s.Count > 0)
                {
                    int child = -1;
                    if ((child = getAdjacentNode_List(s.Peek())) != -1)
                    {
                        counter++;
                        visited[child] = true;
                        s.Push(child);
                    }
                    else
                    {
                        s.Pop();
                    }
                }

                if (counter > 1)
                    tuple.Add(new Tuple<int, int>(root, counter));
                else
                    singles++;
            }

            private int getAdjacentNode_List(int index)
            {
                if (!adjList.ContainsKey(index))
                    return -1;

                bool[] myadjacents = adjList[index];

                for (int k = 0; k < myadjacents.Length; k++)
                {
                    if (k != index && visited[k] == false && myadjacents[k] == true)
                        return k;
                }
                return -1;
            }

            public long CalculateAstronouts()
            {
                long sum = 0;
                long sumoft = 0;
                for (int i = 0; i < tuple.Count - 1; i++)
                {
                    sumoft = sumoft + tuple[i].Item2;
                    for (int j = i + 1; j < tuple.Count; j++)
                    {
                        sum = tuple[i].Item2 * tuple[j].Item2 + sum;
                    }
                }
                sumoft = sumoft + tuple[tuple.Count - 1].Item2;
                sum = sum + (long)(sumoft * singles);
                if (singles > 0)
                    sum = sum + (singles * (singles - 1)) / 2;

                return sum;
            }
        }
    }
}
