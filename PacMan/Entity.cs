using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }

        public Entity(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }
    }
}
