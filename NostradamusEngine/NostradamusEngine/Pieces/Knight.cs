using System.Collections.Generic;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Knight : Piece
    {

        public Knight(Color color, IBoard board)
            : base(color, board)
        {

        }


        public override string FullName => "Knight";

        public override string ShortName => "N";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var allMoves = new List<ISquare>();
            allMoves.AddRange(CheckIfSquareIsCovered(2, 1));
            allMoves.AddRange(CheckIfSquareIsCovered(-2, 1));
            allMoves.AddRange(CheckIfSquareIsCovered(2, -1));
            allMoves.AddRange(CheckIfSquareIsCovered(-2, -1));
            allMoves.AddRange(CheckIfSquareIsCovered(1, 2));
            allMoves.AddRange(CheckIfSquareIsCovered(1, -2));
            allMoves.AddRange(CheckIfSquareIsCovered(-1, 2));
            allMoves.AddRange(CheckIfSquareIsCovered(-1, -2));
            return allMoves;
        }

        public override IEnumerable<NormalMove> CalculateAllMoves(int ply)
        {
            List<NormalMove> allMoves = new List<NormalMove>();
            allMoves.AddRange(CheckMove(2, 1, ply));
            allMoves.AddRange(CheckMove(-2, 1, ply));
            allMoves.AddRange(CheckMove(2, -1, ply));
            allMoves.AddRange(CheckMove(-2, -1, ply));
            allMoves.AddRange(CheckMove(1, 2, ply));
            allMoves.AddRange(CheckMove(1, -2, ply));
            allMoves.AddRange(CheckMove(-1, 2, ply));
            allMoves.AddRange(CheckMove(-1, -2, ply));
            return allMoves;
            
        }

        private IEnumerable<ISquare> CheckIfSquareIsCovered(int fileAdder, int rankAdder)
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
                yield return new NormalMove(this, Square, squareToCheck, null, ply);
            }
            else if (squareStatus == SquareStatus.Occupied)
            {
                var piece = Board.GetPieceOn(squareToCheck);
                if (piece.Color != Color)
                    yield return new NormalMove(this, Square, squareToCheck, piece, ply);
            }
        }

    }
}
