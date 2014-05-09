using NostradamusEngine.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleGui gui = new SimpleGui();
            while (gui.Update())
            {

            }
            //FENParser.LoadFEN(null,"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            Console.WriteLine("Exited - any key to continue");
            Console.ReadKey();
        }
    }
}
