using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.IO
{
    public static class FENParser
    {
        public static void LoadFEN(ChessEngine engine,IBoard board, string fen)
        {
            var fields = fen.Split(' ');
            GetPositions(board, fields[0]);
            engine.ToMove = fields[1] == "w" ? Color.White : Color.Black;
            DetermineCastling(board, fields[2]);
        }

        private static void DetermineCastling(IBoard board, string castlingData)
        {
            if (castlingData=="-")
            {
                board.GetKing(Color.Black).CanCastleKingSide=false;
                board.GetKing(Color.Black).CanCastleQueenSide = false;
                board.GetKing(Color.White).CanCastleKingSide = false;
                board.GetKing(Color.White).CanCastleQueenSide = false;
            }
            foreach (char c in castlingData)
            {
                switch (c)
                {
                    case 'K':
                        board.GetKing(Color.White).CanCastleKingSide = true;
                        break;
                    case 'k':
                        board.GetKing(Color.Black).CanCastleKingSide = true;
                        break;
                    case 'Q':
                        board.GetKing(Color.White).CanCastleQueenSide= true;
                        break;
                    case 'q':
                        board.GetKing(Color.Black).CanCastleQueenSide= true;
                        break;

                }
            }
        }

        private static void GetPositions(IBoard board, string posData)
        {
            var ranks = posData.Split('/');
            for (var r=7;r>=0;r--)
            {
                var f = 0;
                var cPos = 0;
                while (f<8)
                {
                    var num =0;
                    if (int.TryParse(ranks[r][cPos].ToString(),out num))
                    {
                        f+=num;
                        cPos++;
                    }
                    else
                    {
                        var currentSquare = new BareSquare(f, 7 - r);
                        switch (ranks[r][cPos])
                        {
                            case 'p':
                                board.SetPiece(currentSquare, new Pawn(Color.Black,board));
                                break;
                            case 'P':
                                board.SetPiece(currentSquare, new Pawn(Color.White, board));
                                break;
                            case 'q':
                                board.SetPiece(currentSquare, new Queen(Color.Black, board));
                                break;
                            case 'Q':
                                board.SetPiece(currentSquare, new Queen(Color.White, board));
                                break;
                            case 'k':
                                board.SetPiece(currentSquare, new King(Color.Black, board));
                                break;
                            case 'K':
                                board.SetPiece(currentSquare, new King(Color.White, board));
                                break;
                            case 'n':
                                board.SetPiece(currentSquare, new Knight(Color.Black, board));
                                break;
                            case 'N':
                                board.SetPiece(currentSquare, new Knight(Color.White, board));
                                break;
                            case 'r':
                                board.SetPiece(currentSquare, new Rook(Color.Black, board));
                                break;
                            case 'R':
                                board.SetPiece(currentSquare, new Rook(Color.White, board));
                                break;
                            case 'b':
                                board.SetPiece(currentSquare, new Bishop(Color.Black, board));
                                break;
                            case 'B':
                                board.SetPiece(currentSquare, new Bishop(Color.White, board));
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
