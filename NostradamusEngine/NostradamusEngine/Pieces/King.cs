using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine.Pieces
{
    public class King : Piece
    {
        public bool CanCastleQueenSide { get; set; }
        public bool CanCastleKingSide { get; set; }

        public King(Color color, IBoard board)
            : base(color, board)
        {

        }

        public override void Move(NormalMove m)
        {
            var castling = m as CastlingMove;
            if (castling !=null)
            {
                Castle(castling);
            }
            else
                base.Move(m);
        }

        public override void UndoMove(NormalMove m)
        {
            var castling = m as CastlingMove;
            if (castling != null)
            {
                UndoCastle(castling);
            }
            else 
                base.UndoMove(m);
        }

        private void UndoCastle(CastlingMove castling)
        {
            base.UndoMove(castling);
            Board.SetPiece(castling.CastlingRookFrom,this);
            Board.RemovePiece(castling.CastlingRookTo,this);
        }

        private void Castle(CastlingMove castling)
        {
            base.Move(castling);
            Board.SetPiece(castling.CastlingRookTo, this);
            Board.RemovePiece(castling.CastlingRookFrom, this);
        }

        public override string FullName => "King";

        public override string ShortName => "K";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var coveredSquares = new List<ISquare>();
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(0, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, 0));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, 0));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, 1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(0, 1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, 1));
            return coveredSquares;
        }

        public override IEnumerable<NormalMove> CalculateAllMoves(int ply)
        {
            var allMoves = new List<NormalMove>();
            allMoves.AddRange(CheckMove(-1, -1, ply));
            allMoves.AddRange(CheckMove(0, -1, ply));
            allMoves.AddRange(CheckMove(1, -1, ply));
            allMoves.AddRange(CheckMove(-1, 0, ply));
            allMoves.AddRange( CheckMove(1, 0, ply));
            allMoves.AddRange(CheckMove(-1, 1, ply));
            allMoves.AddRange(CheckMove(0, 1, ply));
            allMoves.AddRange(CheckMove(1, 1, ply));
            if (IsKingSideCastlePossible)
                allMoves.Add(new CastlingMove(this,Square,new BareSquare(6,Square.Rank), Board.GetPieceOn(new BareSquare(7,Square.Rank)),new BareSquare(7,Square.Rank),new BareSquare(5,Square.Rank),ply));
            if (IsQueenSideCastlePossible)
                allMoves.Add(new CastlingMove(this, Square, new BareSquare(2, Square.Rank), Board.GetPieceOn(new BareSquare(0, Square.Rank)), new BareSquare(0, Square.Rank), new BareSquare(3, Square.Rank), ply));
            return allMoves;
        }

        private bool IsQueenSideCastlePossible
        {
            get
            {
                if (!CanCastleQueenSide) return false;
                if (Board.GetSquareStatus(new BareSquare(1, Square.Rank)) != SquareStatus.Empty ||
                    Board.GetSquareStatus(new BareSquare(2, Square.Rank)) != SquareStatus.Empty ||
                    Board.GetSquareStatus(new BareSquare(3, Square.Rank)) != SquareStatus.Empty)
                {
                    return false;
                }
                if (Board.GetPieceOn(Square).Moves.Count > 0 ||
                    Board.GetPieceOn(new BareSquare(0, Square.Rank)).Moves.Count > 0)
                {
                    CanCastleQueenSide = false;
                    return false;
                }
                if (Board.PiecesCoverOneOrMore(ColorHelper.Reverse(Color),new [] { new BareSquare(2,Square.Rank), new BareSquare(3, Square.Rank), new BareSquare(4, Square.Rank) }))
                    return false;
                return true;
            }
        }


        private bool IsKingSideCastlePossible
        {
            get
            {
                if (!CanCastleKingSide) return false;
                if (Board.GetSquareStatus(new BareSquare(6, Square.Rank)) != SquareStatus.Empty ||
                    Board.GetSquareStatus(new BareSquare(5, Square.Rank)) != SquareStatus.Empty)
                {
                    return false;
                }
                if (Moves.Count > 0 ||
                    Board.GetPieceOn(new BareSquare(7, Square.Rank)).Moves.Count > 0)
                {
                    CanCastleKingSide = false;
                    return false;
                }
                if (Board.PiecesCoverOneOrMore(ColorHelper.Reverse(Color), new[] { new BareSquare(4, Square.Rank), new BareSquare(5, Square.Rank), new BareSquare(6, Square.Rank) }))
                    return false;
                return true;
            }
        }



        private IEnumerable<ISquare> CheckIfSquaredIsCovered(int fileAdder, int rankAdder)
        {
            var squareToCheck = new BareSquare(Square.File + fileAdder, Square.Rank + rankAdder);
            if (Board.GetSquareStatus(squareToCheck) == SquareStatus.Illegal)
                yield break;
            else yield return squareToCheck;
        }

        private IEnumerable<NormalMove> CheckMove(int fileAdder, int rankAdder, int ply)
        {
            var squareToCheck = new BareSquare(Square.File + fileAdder, Square.Rank + rankAdder);
            var squareStatus = Board.GetSquareStatus(squareToCheck);
            if (squareStatus == SquareStatus.Illegal)
                yield break;    
            if (squareStatus == SquareStatus.Empty)
            {
                yield return new NormalMove(this, Square, squareToCheck, null,ply);
            }
            else if (squareStatus==SquareStatus.Occupied)
            {
                var piece = Board.GetPieceOn(squareToCheck);
                if (piece.Color!=Color)
                    yield return new NormalMove(this, Square, squareToCheck, piece,ply);
            }
        }
    }
}
