using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.IO
{
    public static class FENParser
    {
        public static void LoadFEN(ChessEngine game, String fen)
        {
            String[] fields = fen.Split(' ');
            GetPositions(game, fields[0]);
            if (fields[1] == "w")
                game.IsWhiteToMove = true;
            else
                game.IsWhiteToMove = false;
            DetermineCastling(game, fields[2]);
        }

        private static void DetermineCastling(ChessEngine game, String castlingData)
        {
            if (castlingData=="-")
            {
                game.BlackCastling.Kingside=false;
                game.BlackCastling.Queenside=false;
                game.WhiteCastling.Queenside=false;
                game.WhiteCastling.Kingside=false;
            }
            foreach (char c in castlingData)
            {
                switch (c)
                {
                    case 'K':
                        game.WhiteCastling.Kingside=true;
                        break;
                    case 'k':
                        game.BlackCastling.Kingside=true;
                        break;
                    case 'Q':
                        game.WhiteCastling.Queenside=true;
                        break;
                    case 'q':
                        game.BlackCastling.Queenside=true;
                        break;

                }
            }
        }

        private static void GetPositions(ChessEngine game, String posData)
        {
            String[] ranks = posData.Split('/');
            for (var r=7;r>=0;r--)
            {
                var f = 0;
                var cPos = 0;
                while (f<8)
                {
                    var num =0;
                    if (Int32.TryParse(ranks[r][cPos].ToString(),out num))
                    {
                        f+=num;
                        cPos++;
                    }
                    else
                    {
                        switch (ranks[r][cPos])
                        {
                            case 'p':
                                game.Board[f, 7 - r].Piece = new Pawn(false, game.Board[f, 7 - r],game);
                                break;
                            case 'P':
                                game.Board[f, 7 - r].Piece = new Pawn(true, game.Board[f, 7 - r], game);
                                break;
                            case 'q':
                                game.Board[f, 7 - r].Piece = new Queen(false, game.Board[f, 7 - r], game);
                                break;
                            case 'Q':
                                game.Board[f, 7 - r].Piece = new Queen(true, game.Board[f, 7 - r], game);
                                break;
                            case 'k':
                                game.Board[f, 7 - r].Piece = new King(false, game.Board[f, 7 - r], game);
                                break;
                            case 'K':
                                game.Board[f, 7 - r].Piece = new King(true, game.Board[f, 7 - r], game);
                                break;
                            case 'n':
                                game.Board[f, 7 - r].Piece = new Knight(false, game.Board[f, 7 - r], game);
                                break;
                            case 'N':
                                game.Board[f, 7 - r].Piece = new Knight(true, game.Board[f, 7 - r], game);
                                break;
                            case 'r':
                                game.Board[f, 7 - r].Piece = new Rook(false, game.Board[f, 7 - r], game);
                                break;
                            case 'R':
                                game.Board[f, 7 - r].Piece = new Rook(true, game.Board[f, 7 - r], game);
                                break;
                            case 'b':
                                game.Board[f, 7 - r].Piece = new Bishop(false, game.Board[f, 7 - r], game);
                                break;
                            case 'B':
                                game.Board[f, 7 - r].Piece = new Bishop(true, game.Board[f, 7 - r], game);
                                break;
                        }
                        f++;
                        cPos++;
                    }
                }
            }
        }


    }
}
