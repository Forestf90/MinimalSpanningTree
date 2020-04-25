using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSpanningTree
{
    class DisjointSet
    {

        Dictionary<int, int> parent = new Dictionary<int, int>();


        public void MakeSet(int N)
        {
            for (int i = 0; i < N; i++)
                parent.Add(i, i);
        }


        public int Find(int k)
        {
            if (parent[k] == k)
                return k;

            return Find(parent[k]);
        }

  
        public void Union(int a, int b)
        {
 
            int x = Find(a);
            int y = Find(b);

            parent.Add(x, y);
        }

    }
}
