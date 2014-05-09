using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine
{
    public class SimpleGui
    {
        private NostradamusEngine game;
        private const Int32 squareSize = 3;

        public SimpleGui()
        {
            game = new NostradamusEngine();
            game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        public Boolean Update()
        {
            // Start on top
            for (var r=game.Board.Ranks-1;r>=0;r--)
            {
                for (var f=0;f<game.Board.Files;f++)
                {
                    DrawSquare(f, r);
                }
            }

            DrawBlack();
            DrawWhite();
            return GetCommand();
        }

        public Boolean GetCommand()
        {
            Console.SetCursorPosition(40, 16);
            Console.Write("{0} to move >", game.IsWhiteToMove ? "White" : "Black");
            String cmd = Console.ReadLine();
            return ParseCommand(cmd);
        }

        public Boolean ParseCommand(String cmd)
        {
            String[] completeCommand = cmd.Split(' ');

            switch (completeCommand[0])
            {
                case "x":
                    return false;
                case "l":
                    {
                        foreach (Move move in game.FindLegalMoves())
                        {

                        }
                    }
            }
                return false;
            return true;
        }

        public void DrawBlack()
        {
            Console.SetCursorPosition(40, 8);
            Console.WriteLine("Black Castling: {0}  {1}", game.BlackCastling.Kingside ? "O-O" : "", game.BlackCastling.Queenside ? "O-O-O" : "");
        }

        public void DrawWhite()
        {
            Console.SetCursorPosition(40, 12);
            Console.WriteLine("White Castling: {0}  {1}", game.WhiteCastling.Kingside ? "O-O" : "", game.WhiteCastling.Queenside ? "O-O-O" : "");
        }

        public void DrawSquare(Int32 f, Int32 r)
        {
            if (game.Board[f, r].IsWhite)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (var x = 0; x < squareSize; x++)
            {
                for (var y = 0; y < squareSize; y++)
                {
                    Console.SetCursorPosition(f * squareSize+x, r * squareSize+y);
                    Console.WriteLine(" ");
                }
            }

            if (game.Board[f, r].Piece!=null)
            {
                Console.SetCursorPosition(f * squareSize + 1, r * squareSize + 1);
                if (game.Board[f, r].Piece.IsWhite)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(game.Board[f, r].Piece.ShortName);
            }

        }





    }
}
