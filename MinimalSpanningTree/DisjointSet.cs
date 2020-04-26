using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSpanningTree
{
    class DisjointSet
    {

        int[] parent;
        int[] rank;

        public DisjointSet(int n)
        {
            parent = new int[n];   
            rank = new int[n];

            for (int i = 0; i < parent.Length; i++)
                parent[i] = i;
        }
        public void MakeSet(int N)
        {
            for (int i = 0; i < N; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
               
        }


        public int Find(int k)
        {
            if (parent[k] != k)
                parent[k] = Find(parent[k]); 
            return parent[k];
        }

  
        public void Union(int a, int b)
        {

            int representativeX = Find(a); 
            int representativeY = Find(b); 

            if (rank[representativeX] == rank[representativeY])
            {
                rank[representativeY]++;
                parent[representativeX] = representativeY;
            }
            else if (rank[representativeX] > rank[representativeY])
            {
                parent[representativeY] = representativeX;
            }
            else
            {
                parent[representativeX] = representativeY;
            }

        }

    }
}
