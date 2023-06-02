using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PacMan
{
    class Ghost : Entity
    {
        private Random random;

        public Ghost(int x, int y) : base(x, y, 'G')
        {
            random = new Random();
        }

        public int GetRandomDirection()
        {
            return random.Next(4);
        }
    }
}
