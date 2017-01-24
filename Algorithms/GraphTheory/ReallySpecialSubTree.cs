using System;
using System.Collections.Generic;

namespace Algorithms.GraphTheory
{
    /*see: https://www.hackerrank.com/challenges/kruskalmstrsub */
    public class ReallySpecialSubTree
    {
        #region Drivers
        public static void Driver()
        {
            Graph graph = new Graph(4);
            graph.connectNodeAndCreateEdge(1, 2, 5);
            graph.connectNodeAndCreateEdge(1, 3, 3);
            graph.connectNodeAndCreateEdge(4, 1, 6);
            graph.connectNodeAndCreateEdge(2, 4, 7);
            graph.connectNodeAndCreateEdge(3, 2, 4);
            graph.connectNodeAndCreateEdge(3, 4, 5);

            graph.GetMinSpanTree();
        }

        public static void HRDriver()
        {
            string[] input = Console.ReadLine().Split(' ');
            int gsize = int.Parse(input[0]);
            int l = int.Parse(input[1]);

            Graph g = new Graph(gsize);

            while (l > 0)
            {
                l--;
                string[] ed = Console.ReadLine().Split(' ');
                int e1 = int.Parse(ed[0]);
                int e2 = int.Parse(ed[1]);
                int wt = int.Parse(ed[2]);
                g.connectNodeAndCreateEdge(e1, e2, wt);
            }

            g.GetMinSpanTree();
        }

        #endregion

        class Node
        {
            public int data;
            public bool visited = false;
            public int no = 0;
            public int rank;
            public Node parent;

            public Node(int data)
            {
                this.data = data;
                this.rank = 0;
                this.parent = this;
            }
        }

        class Edge
        {
            public int e1; public int e2; public int weight;
            public Edge(int from, int to, int w)
            {
                this.e1 = from; this.e2 = to; this.weight = w;
            }
        }

        class Graph
        {
            public int size = 0;
            List<Node> nodes = new List<Node>();
            List<Edge> minSpanEdge = new List<Edge>();
            List<Edge> edgeList = new List<Edge>();

            public Graph(int size)
            {
                this.size = size;
                nodes = new List<Node>();

                // Add Default Nodes
                for (int i = 1; i <= this.size; i++)
                {
                    var n = new Node(i);
                    n.no = i;
                    nodes.Add(n);
                }
            }

            // used with Min Span Tree - s,t: nodes numbers, Edge is by nodes index
            public void connectNodeAndCreateEdge(int s, int t, int weight)
            {
                edgeList.Add(new Edge(s - 1, t - 1, weight));
            }

            // using Kruskal's Alogrithm - StandAlone
            public void GetMinSpanTree()
            {
                DisJointSet jointset = new DisJointSet();

                // sort edges
                edgeList.Sort(getAscEdgeComparer());

                // Create sets for each vertex
                for (int i = 0; i < nodes.Count; i++)
                {
                    jointset.makeset(nodes[i]);
                }

                // Construct Min Span Tree
                for (int i = 0; i < edgeList.Count; i++)
                {
                    Edge edge = edgeList[i];

                    if (jointset.union(nodes[edge.e1], nodes[edge.e2]))
                        // add to result
                        minSpanEdge.Add(edge);
                }

                //print result
                int mstSum = 0;
                foreach (var item in minSpanEdge)
                {
                    mstSum = mstSum + item.weight;
                    Console.WriteLine(nodes[item.e1].data + "---" + nodes[item.e2].data);
                }
                Console.WriteLine(mstSum);
            }

            private IComparer<Edge> getAscEdgeComparer()
            {
                return new SpecialMSTEdgeComparer();
            }

        }

        class SpecialMSTEdgeComparer : IComparer<Edge>
        {
            public int Compare(Edge x, Edge y)
            {
                if (x.weight > y.weight) { return 1; }
                else if (x.weight < y.weight) { return -1; }
                else
                {
                    int wx = x.e1 + x.weight + x.e2;
                    int wy = y.e1 + y.weight + y.e2;
                    if (wx >= wy)
                        return 1;
                    else
                        return -1;
                }
            }
        }

        class DisJointSet
        {
            Dictionary<int, Node> map = new Dictionary<int, Node>();

            public void makeset(int d)
            {
                map.Add(d, new Node(d));
            }

            public void makeset(Node node)
            {
                map.Add(node.data, node);
            }

            public bool union(Node n1, Node n2)
            {
                if (n1 == null || n2 == null) { return false; }

                var p1 = findSet(n1);
                var p2 = findSet(n2);

                if (p1.parent == p2.parent) { /*same set*/ return false; }

                if (p1.rank >= p2.rank)
                {
                    p1.rank = p1.rank == p2.rank ? p1.rank + 1 : p1.rank;
                    p2.parent = p1;
                }
                else
                {
                    p1.parent = p2;
                }
                return true;
            }

            public void union(int d1, int d2)
            {
                Node n1 = map[d1];
                Node n2 = map[d2];

                union(n1, n2);
            }

            private Node findSet(Node n1)
            {
                if (n1 == n1.parent)
                {
                    return n1;
                }
                n1.parent = findSet(n1.parent);
                return n1.parent;
            }

            internal Node findSet(int data)
            {
                return findSet(map[data]);
            }
        }
    }
}
