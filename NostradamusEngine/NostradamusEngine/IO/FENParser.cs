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
                game.ToMove = Color.White;
            else
                game.ToMove = Color.Black;
            DetermineCastling(game, fields[2]);
        }

        private static void DetermineCastling(ChessEngine game, String castlingData)
        {
            if (castlingData=="-")
            {
                game.GetKing(Color.Black).CanCastleKingSide=false;
                game.GetKing(Color.Black).CanCastleQueenSide = false;
                game.GetKing(Color.White).CanCastleKingSide = false;
                game.GetKing(Color.White).CanCastleQueenSide = false;
            }
            foreach (char c in castlingData)
            {
                switch (c)
                {
                    case 'K':
                        game.GetKing(Color.White).CanCastleKingSide = false;
                        break;
                    case 'k':
                        game.GetKing(Color.Black).CanCastleKingSide = false;
                        break;
                    case 'Q':
                        game.GetKing(Color.White).CanCastleQueenSide= false;
                        break;
                    case 'q':
                        game.GetKing(Color.Black).CanCastleQueenSide= false;
                        break;

                }
            }
        }

        private static void GetPositions(ChessEngine game, string posData)
        {
            var ranks = posData.Split('/');
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
                                game.Board[f, 7 - r].Piece = new Pawn(Color.Black, game.Board[f, 7 - r],game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);
                                break;
                            case 'P':
                                game.Board[f, 7 - r].Piece = new Pawn(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'q':
                                game.Board[f, 7 - r].Piece = new Queen(Color.Black, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'Q':
                                game.Board[f, 7 - r].Piece = new Queen(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'k':
                                game.Board[f, 7 - r].Piece = new King(Color.Black, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'K':
                                game.Board[f, 7 - r].Piece = new King(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'n':
                                game.Board[f, 7 - r].Piece = new Knight(Color.Black, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'N':
                                game.Board[f, 7 - r].Piece = new Knight(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'r':
                                game.Board[f, 7 - r].Piece = new Rook(Color.Black, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'R':
                                game.Board[f, 7 - r].Piece = new Rook(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'b':
                                game.Board[f, 7 - r].Piece = new Bishop(Color.Black, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

                                break;
                            case 'B':
                                game.Board[f, 7 - r].Piece = new Bishop(Color.White, game.Board[f, 7 - r], game);
                                game.AddPiece(game.Board[f, 7 - r].Piece);

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
