using System;
using System.Collections.Generic;

namespace MinimalSpanningTree
{
    class Graph
    {
        public bool[,] Nodes;
        public int N;
        public bool Connected = false;
        public int[,] CoPoints;
        public List<Edge> Edges = new List<Edge>();

        public Graph(int n, int p, int minEdg, int maxEdg, bool conn)
        {
            N = n;
            Nodes = new bool[N, N];
            if (conn)
            {
                CreateConnected();
            }
            CreateConnections(p, minEdg, maxEdg);
        }

        private void CreateConnections(int p, int minEdg, int maxEdg)
        {
            Random rnd = new Random();

            List<int> nodesIndex = new List<int>();
            List<int> nodesIndex2 = new List<int>();
            for (int i = 0; i < N; i++)
            {
                nodesIndex.Add(i);
                nodesIndex2.Add(i);

            }
            nodesIndex.Sort((a, b) => rnd.Next(-1, 1));
            nodesIndex2.Sort((a, b) => rnd.Next(-1, 1));


            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (Nodes[nodesIndex[i], nodesIndex2[j]] || Nodes[nodesIndex2[j], nodesIndex[i]] 
                        || nodesIndex[i] == nodesIndex2[j]) continue;
                    if (Edges.Count >= maxEdg) break;
                    int a = rnd.Next(100);

                    if (a > 0 && a < p || Edges.Count < minEdg)
                    {
                        Nodes[nodesIndex[i], nodesIndex2[j]] = true;
                        Nodes[nodesIndex2[j], nodesIndex[i]] = true;
                        double cost = rnd.Next(1, 20);
                        Edges.Add(new Edge(nodesIndex[i], nodesIndex2[j], cost));
                    }

                }
                if (Edges.Count >= maxEdg) break;

            }

            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    if(Nodes[i,j] != true) Nodes[i, j] = false;

                }
            }

        }

        private void CreateConnected()
        {
            if (N <= 1) return;
            List<int> nodesIndex = new List<int>();
 
            for (int i=0; i<N; i++)
            {
                nodesIndex.Add(i);
            }
            
            var random = new Random();
            if (nodesIndex.Count % 2 == 1) nodesIndex.Add(random.Next(nodesIndex.Count));
            nodesIndex.Sort((a, b) => random.Next(-1, 1));

            while (nodesIndex.Count > 1)
            {
                nodesIndex = ConnectedRec(nodesIndex);
            }

        }

        private List<int> ConnectedRec(List<int> nodesIndex)
        {
            List<int> temp = new List<int>();
            Random rnd = new Random();
            if (nodesIndex.Count % 2 == 1) nodesIndex.Add(rnd.Next(N));
            for (int i = 0; i < nodesIndex.Count-1; i += 2)
            {
                Nodes[nodesIndex[i], nodesIndex[i + 1]] = true;
                Nodes[nodesIndex[i + 1], nodesIndex[i]] = true;
                temp.Add(nodesIndex[i]);
                double cost = rnd.Next(1, 20);
                Edges.Add(new Edge(nodesIndex[i], nodesIndex[i+1], cost));
            }
            return temp;
        }

        public void AddNode()
        {
            N = N + 1;
            bool[,] temp = new bool[N, N];

            for (int i = 0; i < N - 1; i++)
            {
                for (int j = 0; j < N - 1; j++)
                {
                    temp[i, j] = Nodes[i, j];
                }
            }

            for (int i = 0; i < N; i++)
            {
                temp[N - 1, i] = false;
                temp[i, N - 1] = false;
            }
            Nodes = temp;

        }

        public void RemoveNode(int removeIndex)
        {

            bool[,] temp = new bool[N - 1, N - 1];
            N = N - 1;
            int ii = 0, jj = 0;
            for (int i = 0; i < N; i++)
            {

                if (removeIndex == i) ii = 1;
                for (int j = 0; j < N; j++)
                {

                    if (removeIndex == j) jj = 1;
                    temp[i, j] = Nodes[i + ii, j + jj];
                }
                jj = 0;
            }

            Nodes = temp;
            Edges.RemoveAll(e => e.NodeOne == removeIndex || e.NodeTwo == removeIndex);
            foreach(Edge e in Edges)
            {
                if (e.NodeOne > removeIndex) e.NodeOne--;
                if (e.NodeTwo > removeIndex) e.NodeTwo--;
            }

        }

        public void RandomCoordinate(int width, int height)
        {
            CoPoints = new int[N, 2];

            Random rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                int x = rnd.Next(10, width - 15);
                int y = rnd.Next(20, height - 20);
                CoPoints[i, 0] = x;
                CoPoints[i, 1] = y;

            }

        }

        public void CircleCoordinate(int width, int height)
        {


            CoPoints = new int[N, 2];


            int centrumx = width / 2;
            int centrumy = height / 2;

            int x = 0;
            int y = 0;
            int diameter = (height / 3);
            int i = 0;
            for (double angle = 0.0f; angle <= (2.0f * Math.PI); angle += ((Math.PI * 2.0f) / N))
            {
                if (i == N) break;
                x = Convert.ToInt32(diameter * Math.Sin(angle)) + centrumx;
                y = Convert.ToInt32(diameter * Math.Cos(angle)) + centrumy;
                CoPoints[i, 0] = x;
                CoPoints[i, 1] = y;
                i++;

            }
        }

        public void GridCoordinate(int width, int height)
        {


            CoPoints = new int[N, 2];

            Random rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                int x = rnd.Next(1, 10);
                int y = rnd.Next(1, 10);
                bool was = false;
                x = x * (width / 10) - 5;
                y = y * (height / 10) - 5;
                for (int j = 0; j < i; j++)
                {

                    if (x == CoPoints[j, 0] && y == CoPoints[j, 1])
                    {
                        i = i - 1;
                        was = true;
                        break;
                    }

                }

                if (!was)
                {
                    CoPoints[i, 0] = x;
                    CoPoints[i, 1] = y;
                }

            }

        }

        public void ResizeGraph(double width, double height)
        {
            for(int i=0; i<N; i++)
            {
                CoPoints[i, 0] = Convert.ToInt32(width * CoPoints[i, 0]);
                CoPoints[i, 1] = Convert.ToInt32(height * CoPoints[i, 1]);
            }
        }

    }

}
