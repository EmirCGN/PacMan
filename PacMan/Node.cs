using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    class Node
    {
        public int X { get; }
        public int Y { get; }
        public int MovementCost { get; set; }
        public int HeuristicCost { get; set; }
        public int TotalCost => MovementCost + HeuristicCost;
        public Node Parent { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
            MovementCost = 0;
            HeuristicCost = 0;
            Parent = null;
        }
    }
}
