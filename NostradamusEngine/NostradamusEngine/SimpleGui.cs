using NostradamusEngine.Board;
using NostradamusEngine.Rules;
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
            //game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            game.LoadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
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
                case "i":
                    {
                        var piece = game.Board[completeCommand[1]].Piece;
                        Console.SetCursorPosition(40, 17);
                        if (piece != null)
                            Console.Write("On {0} : {1} ( {2} )", completeCommand[1], piece.FullName, piece.IsWhite ? "White" : "Black");
                        else
                            Console.Write("No piece!");
                        break;
                    }
                case "m":
                    {
                        var piece = game.Board[completeCommand[1]].Piece;
                        if (piece != null)
                        {
                            Int32 i = 17;
                            foreach (Move move in piece.CalculateAllMoves())
                            {
                                Console.SetCursorPosition(40, i++);
                                Console.Write(move + " ");
                            }
                        }
                        break;
                    }
                case "d":
                    {
                        var piece = game.Board[completeCommand[1]].Piece;
                        var destination = game.Board[completeCommand[2]];
                        if (piece != null)
                        {
                            game.Move(new Move(piece, piece.Square, destination, destination.Piece));
                        }
                        break;
                    }
            }
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
                     
                    Console.SetCursorPosition(f * squareSize+x, (7-r)*squareSize+y);
                    Console.Write((x==0 && y==0)?r.ToString() : " ");
                }
            }

            if (game.Board[f, r].Piece!=null)
            {
                Console.SetCursorPosition(f * squareSize + 1, (7-r) * squareSize + 1);
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
