using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PacMan
{
    class Program
    {
      static void Main(string[] args)
        {
            Console.Title = "PacMan - EmirCGN";
            Game game = new Game();
            game.Run();
        }
        
    }
}