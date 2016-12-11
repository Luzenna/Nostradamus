using System.Windows;
using NostradamusEngine.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Evaluators;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine
{
    class Program
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(typeof(Program));
        [STAThread]
        static void Main(string[] args)
        {
            Log.Info("Starting Nostradamus");
            var board = new Board();

        var engine = new ChessEngine(new SimpleEvaluator(),board);
            engine.LoadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
            engine.Perft(8,Color.White);
            /*var window = new Window
            {
                Title = "Nostradamus WpfGui",
                Content = new WpfGui.Main(),
                Width = 1200,
                Height = 800
            };
            window.ShowDialog();
            */
            //SimpleGui gui = new SimpleGui();
            //while (gui.Update())
            //{

            //}
            //FENParser.LoadFEN(null,"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            //Console.WriteLine("Exited - any key to continue");
            //Console.ReadKey();
        }
    }
}
