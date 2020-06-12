using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSpanningTree
{
    class Edge
    {
        public int NodeOne;
        public int NodeTwo;
        public double Cost;

        public Edge(int one, int two, double cost)
        {
            NodeOne = one;
            NodeTwo = two;
            Cost = cost;
        }

        public Edge()
        {

        }

        public String GetString()
        {
            return NodeOne + " -> " + NodeTwo + " cost: "+Cost;
        }
    }
}
