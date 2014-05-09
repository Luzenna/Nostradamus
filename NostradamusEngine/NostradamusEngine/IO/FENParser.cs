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
        public static void LoadFEN(NostradamusEngine game, String fen)
        {
            String[] fields = fen.Split(' ');
            GetPositions(game, fields[0]);
            if (fields[1] == "w")
                game.IsWhiteToMove = true;
            else
                game.IsWhiteToMove = false;
            DetermineCastling(game, fields[2]);
        }

        private static void DetermineCastling(NostradamusEngine game, String castlingData)
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

        private static void GetPositions(NostradamusEngine game, String posData)
        {
            String[] ranks = posData.Split('/');
            for (var r=7;r>=0;r--)
            {
                var f = 0;
                while (f<8)
                {
                    Int32 num =0;
                    if (Int32.TryParse(ranks[r][f].ToString(),out num))
                    {
                        f+=num;
                    }
                    else
                    {
                        switch (ranks[r][f])
                        {
                            case 'p':
                                game.Board[f, 7 - r].Piece = new Pawn(false, game.Board[f, 7 - r]);
                                break;
                            case 'P':
                                game.Board[f, 7 - r].Piece = new Pawn(true, game.Board[f, 7 - r]);
                                break;
                            case 'q':
                                game.Board[f, 7 - r].Piece = new Queen(false, game.Board[f, 7 - r]);
                                break;
                            case 'Q':
                                game.Board[f, 7 - r].Piece = new Queen(true, game.Board[f, 7 - r]);
                                break;
                            case 'k':
                                game.Board[f, 7 - r].Piece = new King(false, game.Board[f, 7 - r]);
                                break;
                            case 'K':
                                game.Board[f, 7 - r].Piece = new King(true, game.Board[f, 7 - r]);
                                break;
                            case 'n':
                                game.Board[f, 7 - r].Piece = new Knight(false, game.Board[f, 7 - r]);
                                break;
                            case 'N':
                                game.Board[f, 7 - r].Piece = new Knight(true, game.Board[f, 7 - r]);
                                break;
                            case 'r':
                                game.Board[f, 7 - r].Piece = new Rook(false, game.Board[f, 7 - r]);
                                break;
                            case 'R':
                                game.Board[f, 7 - r].Piece = new Rook(true, game.Board[f, 7 - r]);
                                break;
                            case 'b':
                                game.Board[f, 7 - r].Piece = new Bishop(false, game.Board[f, 7 - r]);
                                break;
                            case 'B':
                                game.Board[f, 7 - r].Piece = new Bishop(true, game.Board[f, 7 - r]);
                                break;
                        }
                        f++;
                    }
                }
            }
        }


    }
}
