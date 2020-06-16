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

        public Graph(int n, int p)
        {
            N = n;
            Nodes = new bool[N, N];
            CreateConnections(p);
        }

        private void CreateConnections(int p)
        {
            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    int a = rnd.Next(100);
                    if (a > 0 && a < p)
                    {
                        Nodes[i, j] = true;
                        double cost = rnd.Next(1,20);
                        Edges.Add(new Edge(i, j, cost));
                    }
                    else Nodes[i, j] = false;
                }

            }

            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    Nodes[i, j] = Nodes[j, i];
                }

            }

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

        public void GridCoordinate(int width)
        {


            CoPoints = new int[N, 2];

            Random rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                int x = rnd.Next(1, 10);
                int y = rnd.Next(1, 10);
                bool was = false;
                x = x * (width / 10) - 5;
                y = y * (width / 10) - 5;
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
    }

}
